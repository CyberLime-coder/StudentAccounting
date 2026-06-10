using System.Collections.ObjectModel;
using StudentAccounting.Models;

namespace StudentAccounting.Services
{
    /// Интерфейс сервиса для работы с данными (студенты и дисциплины).
    /// Позволяет абстрагировать ViewModel от конкретной реализации хранения.
    public interface IDataService
    {
        /// Возвращает список студентов.
        ObservableCollection<Student> GetStudents();
        /// Возвращает список дисциплин.
        ObservableCollection<Discipline> GetDisciplines();
        /// Добавляет нового студента.
        void AddStudent(Student student);
        /// Обновляет существующего студента.
        void UpdateStudent(Student student);
        /// Удаляет студента по ID.
        void DeleteStudent(int id);
        /// Добавляет новую дисциплину.
        void AddDiscipline(Discipline discipline);
        /// Обновляет дисциплину.
        void UpdateDiscipline(Discipline discipline);
        /// Удаляет дисциплину по ID.
        void DeleteDiscipline(int id);
        /// Сохраняет все изменения (запись в файл).
        void SaveChanges();
    }
}