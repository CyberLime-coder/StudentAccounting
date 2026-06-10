using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using StudentAccounting.Models;
using StudentAccounting.Services;
using StudentAccounting.Commands;

namespace StudentAccounting.ViewModels
{
    /// ViewModel главного окна. Содержит коллекции студентов и дисциплин,
    /// выбранные элементы, команды для CRUD операций.

    public class MainViewModel : BaseViewModel
    {
        private readonly IDataService _dataService;
        private ObservableCollection<Student> _students;
        private ObservableCollection<Discipline> _disciplines;
        private Student _selectedStudent;
        private Discipline _selectedDiscipline;

        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _students = _dataService.GetStudents();
            _disciplines = _dataService.GetDisciplines();

            AddStudentCommand = new RelayCommand(_ => OpenStudentEditDialog(null));
            EditStudentCommand = new RelayCommand(_ => OpenStudentEditDialog(_selectedStudent), _ => _selectedStudent != null);
            DeleteStudentCommand = new RelayCommand(_ => DeleteStudent(), _ => _selectedStudent != null);
            RefreshStudentsCommand = new RelayCommand(_ => RefreshStudents());

            AddDisciplineCommand = new RelayCommand(_ => OpenDisciplineEditDialog(null));
            EditDisciplineCommand = new RelayCommand(_ => OpenDisciplineEditDialog(_selectedDiscipline), _ => _selectedDiscipline != null);
            DeleteDisciplineCommand = new RelayCommand(_ => DeleteDiscipline(), _ => _selectedDiscipline != null);
            RefreshDisciplinesCommand = new RelayCommand(_ => RefreshDisciplines());
        }

        public ObservableCollection<Student> Students
        {
            get => _students;
            set { _students = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Discipline> Disciplines
        {
            get => _disciplines;
            set { _disciplines = value; OnPropertyChanged(); }
        }

        public Student SelectedStudent
        {
            get => _selectedStudent;
            set { _selectedStudent = value; OnPropertyChanged(); }
        }

        public Discipline SelectedDiscipline
        {
            get => _selectedDiscipline;
            set { _selectedDiscipline = value; OnPropertyChanged(); }
        }

        // Команды для студентов
        public ICommand AddStudentCommand { get; }
        public ICommand EditStudentCommand { get; }
        public ICommand DeleteStudentCommand { get; }
        public ICommand RefreshStudentsCommand { get; }

        // Команды для дисциплин
        public ICommand AddDisciplineCommand { get; }
        public ICommand EditDisciplineCommand { get; }
        public ICommand DeleteDisciplineCommand { get; }
        public ICommand RefreshDisciplinesCommand { get; }

        /// Открывает диалог добавления/редактирования студента.
        private void OpenStudentEditDialog(Student student)
        {
            var dialog = new Views.StudentEditDialog(student?.Clone() as Student);
            if (dialog.ShowDialog() == true && dialog.EditedStudent != null)
            {
                if (student == null)
                    _dataService.AddStudent(dialog.EditedStudent);
                else
                    _dataService.UpdateStudent(dialog.EditedStudent);
                RefreshStudents();
            }
        }

        /// Открывает диалог добавления/редактирования дисциплины.
        private void OpenDisciplineEditDialog(Discipline discipline)
        {
            var dialog = new Views.DisciplineEditDialog(discipline?.Clone() as Discipline);
            if (dialog.ShowDialog() == true && dialog.EditedDiscipline != null)
            {
                if (discipline == null)
                    _dataService.AddDiscipline(dialog.EditedDiscipline);
                else
                    _dataService.UpdateDiscipline(dialog.EditedDiscipline);
                RefreshDisciplines();
            }
        }

        /// Удаляет выбранного студента после подтверждения.
        private void DeleteStudent()
        {
            if (SelectedStudent == null) return;
            var result = MessageBox.Show($"Удалить запись о студенте {SelectedStudent.FullName}?",
                                         "Подтверждение удаления",
                                         MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _dataService.DeleteStudent(SelectedStudent.Id);
                    RefreshStudents();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// Удаляет выбранную дисциплину после подтверждения.
        private void DeleteDiscipline()
        {
            if (SelectedDiscipline == null) return;
            var result = MessageBox.Show($"Удалить дисциплину {SelectedDiscipline.Name}?",
                                         "Подтверждение удаления",
                                         MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _dataService.DeleteDiscipline(SelectedDiscipline.Id);
                    RefreshDisciplines();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// Обновляет список студентов из сервиса.
        private void RefreshStudents()
        {
            Students = _dataService.GetStudents();
            OnPropertyChanged(nameof(Students));
        }

        /// Обновляет список дисциплин из сервиса.
        private void RefreshDisciplines()
        {
            Disciplines = _dataService.GetDisciplines();
            OnPropertyChanged(nameof(Disciplines));
        }
    }
}