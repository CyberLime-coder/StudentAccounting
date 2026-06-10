using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using StudentAccounting.Models;

namespace StudentAccounting.Services
{
    /// Реализация IDataService с хранением данных в JSON-файле.
    /// При первом запуске создаёт пустой файл, при последующих загружает из него.
    public class JsonDataService : IDataService
    {
        private readonly string _filePath = "university_data.json";
        private ObservableCollection<Student> _students;
        private ObservableCollection<Discipline> _disciplines;
        private int _nextStudentId;
        private int _nextDisciplineId;

        public JsonDataService()
        {
            _students = new ObservableCollection<Student>();
            _disciplines = new ObservableCollection<Discipline>();
            LoadData();
        }

        /// Загружает данные из JSON-файла или создаёт пустые коллекции.
        private void LoadData()
        {
            if (!File.Exists(_filePath))
            {
                _students.Clear();
                _disciplines.Clear();
                _nextStudentId = 1;
                _nextDisciplineId = 1;
                return;
            }

            try
            {
                string json = File.ReadAllText(_filePath);
                var data = JsonSerializer.Deserialize<DataContainer>(json);
                if (data != null)
                {
                    _students = new ObservableCollection<Student>(data.Students);
                    _disciplines = new ObservableCollection<Discipline>(data.Disciplines);
                    _nextStudentId = _students.Any() ? _students.Max(s => s.Id) + 1 : 1;
                    _nextDisciplineId = _disciplines.Any() ? _disciplines.Max(d => d.Id) + 1 : 1;
                }
            }
            catch (Exception)
            {
                // При ошибке чтения создаём пустые коллекции
                _students = new ObservableCollection<Student>();
                _disciplines = new ObservableCollection<Discipline>();
                _nextStudentId = 1;
                _nextDisciplineId = 1;
            }
        }

        /// Сохраняет текущее состояние в JSON-файл.
        private void SaveData()
        {
            var container = new DataContainer
            {
                Students = _students.ToList(),
                Disciplines = _disciplines.ToList()
            };
            string json = JsonSerializer.Serialize(container, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public ObservableCollection<Student> GetStudents() => _students;
        public ObservableCollection<Discipline> GetDisciplines() => _disciplines;

        public void AddStudent(Student student)
        {
            student.Id = _nextStudentId++;
            _students.Add(student);
            SaveData();
        }

        public void UpdateStudent(Student student)
        {
            var existing = _students.FirstOrDefault(s => s.Id == student.Id);
            if (existing != null)
            {
                existing.FullName = student.FullName;
                existing.Group = student.Group;
                existing.RecordBookNumber = student.RecordBookNumber;
                existing.AverageScore = student.AverageScore;
                existing.EducationForm = student.EducationForm;
                SaveData();
            }
        }

        public void DeleteStudent(int id)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                _students.Remove(student);
                SaveData();
            }
        }

        public void AddDiscipline(Discipline discipline)
        {
            discipline.Id = _nextDisciplineId++;
            _disciplines.Add(discipline);
            SaveData();
        }

        public void UpdateDiscipline(Discipline discipline)
        {
            var existing = _disciplines.FirstOrDefault(d => d.Id == discipline.Id);
            if (existing != null)
            {
                existing.Name = discipline.Name;
                existing.Hours = discipline.Hours;
                existing.Semester = discipline.Semester;
                existing.Teacher = discipline.Teacher;
                SaveData();
            }
        }

        public void DeleteDiscipline(int id)
        {
            var discipline = _disciplines.FirstOrDefault(d => d.Id == id);
            if (discipline != null)
            {
                _disciplines.Remove(discipline);
                SaveData();
            }
        }

        public void SaveChanges() => SaveData();

        /// Вспомогательный класс для сериализации/десериализации.
        private class DataContainer
        {
            public List<Student> Students { get; set; } = new List<Student>();
            public List<Discipline> Disciplines { get; set; } = new List<Discipline>();
        }
    }
}