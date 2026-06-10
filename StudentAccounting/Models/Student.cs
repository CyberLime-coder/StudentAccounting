using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace StudentAccounting.Models
{
    /// Модель студента. Содержит свойства и валидацию.
    /// Реализует INotifyPropertyChanged для уведомлений об изменении свойств,
    /// и ICloneable для клонирования (редактирование через копию).
    public class Student : INotifyPropertyChanged, ICloneable
    {
        private int _id;
        private string _fullName = "";
        private string _group = "";
        private string _recordBookNumber = "";
        private double _averageScore;
        private string _educationForm = "Бюджет";

        /// Уникальный идентификатор студента.
        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }

        /// Полное имя студента (ФИО). Не может быть пустым.
        public string FullName
        {
            get => _fullName;
            set { _fullName = value; OnPropertyChanged(); }
        }

        /// Группа в формате ББББ-ЧЧ-ЧЧ, например ИСП-21-01.
        /// Заглавные русские буквы, дефисы, цифры.

        public string Group
        {
            get => _group;
            set { _group = value; OnPropertyChanged(); }
        }

        /// Номер зачётной книжки: ровно 8 цифр.
        public string RecordBookNumber
        {
            get => _recordBookNumber;
            set { _recordBookNumber = value; OnPropertyChanged(); }
        }

        /// Средний балл от 2.0 до 5.0 (один знак после запятой).
        public double AverageScore
        {
            get => _averageScore;
            set { _averageScore = value; OnPropertyChanged(); }
        }

        /// Форма обучения: "Бюджет" или "Контракт".
        public string EducationForm
        {
            get => _educationForm;
            set { _educationForm = value; OnPropertyChanged(); }
        }

        /// Проверяет корректность всех полей студента.

        /// "error" Текст ошибки, если валидация не пройдена.
        /// true, если все поля корректны, иначе false.
        public bool IsValid(out string error)
        {
            if (string.IsNullOrWhiteSpace(FullName))
            {
                error = "ФИО не может быть пустым.";
                return false;
            }
            if (!Regex.IsMatch(Group, @"^[А-Я]{4}-\d{2}-\d{2}$"))
            {
                error = "Группа должна иметь формат ББББ-ЧЧ-ЧЧ (например ИСП-21-01).";
                return false;
            }
            if (!Regex.IsMatch(RecordBookNumber, @"^\d{8}$"))
            {
                error = "Номер зачётной книжки – 8 цифр.";
                return false;
            }
            if (AverageScore < 2.0 || AverageScore > 5.0)
            {
                error = "Средний балл должен быть в диапазоне 2.0 – 5.0.";
                return false;
            }
            if (string.IsNullOrEmpty(EducationForm))
            {
                error = "Форма обучения обязательна.";
                return false;
            }
            error = string.Empty;
            return true;
        }

        /// Создаёт поверхностную копию объекта (для редактирования в диалоге).

        public object Clone() => MemberwiseClone();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}