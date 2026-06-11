using System;
using System.Windows.Input;
using StudentAccounting.Models;
using StudentAccounting.Commands;

namespace StudentAccounting.ViewModels
{
    public class DisciplineEditViewModel : BaseViewModel
    {
        // Редактируемая дисциплина (клон)
        private Discipline _discipline;

        // Текст ошибки валидации
        private string _errorMessage = "";

        // Доступность кнопки "Сохранить"
        private bool _isSaveEnabled;

        // Текстовые поля для привязки ввода
        private string _nameText = "";
        private string _hoursText = "";
        private string _semesterText = "";
        private string _teacherText = "";

        // Конструктор
        public DisciplineEditViewModel(Discipline discipline)
        {
            _discipline = discipline?.Clone() as Discipline ?? new Discipline();
            NameText = _discipline.Name;
            HoursText = _discipline.Hours.ToString();
            SemesterText = _discipline.Semester.ToString();
            TeacherText = _discipline.Teacher;
            SaveCommand = new RelayCommand(_ => Save(), _ => IsSaveEnabled);
            CancelCommand = new RelayCommand(_ => Cancel());
            Validate();
        }

        // Свойства для привязки
        public string NameText
        {
            get => _nameText;
            set { _nameText = value; OnPropertyChanged(); Validate(); }
        }

        public string HoursText
        {
            get => _hoursText;
            set { _hoursText = value; OnPropertyChanged(); Validate(); }
        }

        public string SemesterText
        {
            get => _semesterText;
            set { _semesterText = value; OnPropertyChanged(); Validate(); }
        }

        public string TeacherText
        {
            get => _teacherText;
            set { _teacherText = value; OnPropertyChanged(); Validate(); }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public bool IsSaveEnabled
        {
            get => _isSaveEnabled;
            set { _isSaveEnabled = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public event Action<bool?> CloseRequest;

        public Discipline Discipline => _discipline;

        // Проверка всех полей
        private void Validate()
        {
            // Название
            if (string.IsNullOrWhiteSpace(NameText))
            {
                ErrorMessage = "Название дисциплины обязательно.";
                IsSaveEnabled = false;
                return;
            }
            // Часы
            if (!int.TryParse(HoursText, out int hours) || hours < 1 || hours > 500)
            {
                ErrorMessage = "Часы должны быть целым числом от 1 до 500.";
                IsSaveEnabled = false;
                return;
            }
            // Семестр
            if (!int.TryParse(SemesterText, out int semester) || semester < 1 || semester > 8)
            {
                ErrorMessage = "Семестр должен быть целым числом от 1 до 8.";
                IsSaveEnabled = false;
                return;
            }
            // Преподаватель
            if (string.IsNullOrWhiteSpace(TeacherText))
            {
                ErrorMessage = "Преподаватель обязателен.";
                IsSaveEnabled = false;
                return;
            }
            // Все хорошо
            _discipline.Name = NameText;
            _discipline.Hours = hours;
            _discipline.Semester = semester;
            _discipline.Teacher = TeacherText;
            ErrorMessage = "";
            IsSaveEnabled = true;
        }

        private void Save() => CloseRequest?.Invoke(true);
        private void Cancel() => CloseRequest?.Invoke(false);
    }
}