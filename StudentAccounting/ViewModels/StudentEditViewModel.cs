using System;
using System.Text.RegularExpressions;
using System.Windows.Input;
using StudentAccounting.Models;
using StudentAccounting.Commands;

namespace StudentAccounting.ViewModels
{
    /// ViewModel для диалога редактирования студента.
    /// Содержит логику валидации, преобразование ввода среднего балла (замена запятой на точку),
    /// команды "Сохранить" и "Отмена".

    public class StudentEditViewModel : BaseViewModel
    {
        private Student _student;
        private string _errorMessage = "";
        private bool _isSaveEnabled;
        private string _averageScoreText = "";

        public StudentEditViewModel(Student student)
        {
            _student = student?.Clone() as Student ?? new Student();
            if (_student.AverageScore > 0)
                _averageScoreText = _student.AverageScore.ToString(System.Globalization.CultureInfo.InvariantCulture);
            SaveCommand = new RelayCommand(_ => Save(), _ => IsSaveEnabled);
            CancelCommand = new RelayCommand(_ => Cancel());
            Validate();
        }

        /// Редактируемый студент.
        public Student Student
        {
            get => _student;
            set { _student = value; OnPropertyChanged(); Validate(); }
        }

        /// Текст ошибки валидации.
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        /// Доступность кнопки "Сохранить".
        public bool IsSaveEnabled
        {
            get => _isSaveEnabled;
            set { _isSaveEnabled = value; OnPropertyChanged(); }
        }

        /// Текстовое представление среднего балла. Позволяет вводить точку или запятую.
        /// При изменении вызывает валидацию.
        public string AverageScoreText
        {
            get => _averageScoreText;
            set
            {
                _averageScoreText = value;
                OnPropertyChanged();
                Validate();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        /// Событие для закрытия окна.
        public event Action<bool?> CloseRequest;

        /// Проверяет все поля и обновляет состояние кнопки.
        private void Validate()
        {
            // 1. ФИО
            if (string.IsNullOrWhiteSpace(Student.FullName))
            {
                ErrorMessage = "ФИО не может быть пустым.";
                IsSaveEnabled = false;
                return;
            }
            // 2. Группа
            if (!Regex.IsMatch(Student.Group, @"^[А-Я]{4}-\d{2}-\d{2}$"))
            {
                ErrorMessage = "Группа должна иметь формат ББББ-ЧЧ-ЧЧ (например ИСП-21-01).";
                IsSaveEnabled = false;
                return;
            }
            // 3. Номер зачётной книжки
            if (!Regex.IsMatch(Student.RecordBookNumber, @"^\d{8}$"))
            {
                ErrorMessage = "Номер зачётной книжки – 8 цифр.";
                IsSaveEnabled = false;
                return;
            }
            // 4. Средний балл (поддержка точки и запятой)
            double score;
            string scoreText = AverageScoreText.Replace(',', '.');
            if (!double.TryParse(scoreText, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out score))
            {
                ErrorMessage = "Введите число (например 4.5 или 4,5).";
                IsSaveEnabled = false;
                return;
            }
            if (score < 2.0 || score > 5.0)
            {
                ErrorMessage = "Средний балл должен быть в диапазоне 2.0 – 5.0.";
                IsSaveEnabled = false;
                return;
            }
            // 5. Форма обучения
            if (string.IsNullOrEmpty(Student.EducationForm))
            {
                ErrorMessage = "Форма обучения обязательна.";
                IsSaveEnabled = false;
                return;
            }

            // Все проверки пройдены
            Student.AverageScore = Math.Round(score, 1);
            ErrorMessage = "";
            IsSaveEnabled = true;
        }

        private void Save() => CloseRequest?.Invoke(true);
        private void Cancel() => CloseRequest?.Invoke(false);
    }
}