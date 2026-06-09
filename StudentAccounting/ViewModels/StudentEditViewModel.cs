using System;
using System.Text.RegularExpressions;
using System.Windows.Input;
using StudentAccounting.Models;
using StudentAccounting.Commands;

namespace StudentAccounting.ViewModels
{
    public class StudentEditViewModel : BaseViewModel
    {
        private Student _student;
        private string _errorMessage = string.Empty;
        private bool _isSaveEnabled;
        private string _averageScoreText = "";

        public StudentEditViewModel(Student student)
        {
            _student = student?.Clone() as Student ?? new Student();
            // Инициализируем текстовое поле среднего балла
            if (_student.AverageScore > 0)
                _averageScoreText = _student.AverageScore.ToString(System.Globalization.CultureInfo.InvariantCulture);
            SaveCommand = new RelayCommand(_ => Save(), _ => IsSaveEnabled);
            CancelCommand = new RelayCommand(_ => Cancel());
            Validate();
        }

        public Student Student
        {
            get => _student;
            set { _student = value; OnPropertyChanged(); Validate(); }
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

        public string AverageScoreText
        {
            get => _averageScoreText;
            set
            {
                _averageScoreText = value;
                OnPropertyChanged();
                Validate(); // при каждом изменении текста проверка
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public event Action<bool?> CloseRequest;

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
            // 4. Средний балл
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

            // проверки пройдены
            Student.AverageScore = Math.Round(score, 1);
            ErrorMessage = "";
            IsSaveEnabled = true;
        }

        private void Save() => CloseRequest?.Invoke(true);
        private void Cancel() => CloseRequest?.Invoke(false);
    }
}