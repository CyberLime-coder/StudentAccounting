using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StudentAccounting.Models
{
    /// Модель дисциплины. Содержит свойства и валидацию.
    /// Реализует INotifyPropertyChanged и ICloneable.
    public class Discipline : INotifyPropertyChanged, ICloneable
    {
        private int _id;
        private string _name = "";
        private int _hours;
        private int _semester;
        private string _teacher = "";

        /// Уникальный идентификатор дисциплины.
        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }

        /// Название дисциплины. Не может быть пустым.
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        /// Количество часов (от 1 до 500).
        public int Hours
        {
            get => _hours;
            set { _hours = value; OnPropertyChanged(); }
        }

        /// Семестр (от 1 до 8).
        public int Semester
        {
            get => _semester;
            set { _semester = value; OnPropertyChanged(); }
        }

        /// ФИО преподавателя. Не может быть пустым.
        public string Teacher
        {
            get => _teacher;
            set { _teacher = value; OnPropertyChanged(); }
        }

        /// Проверяет корректность полей дисциплины.
        /// "error" Текст ошибки.
        /// true, если данные корректны.
        public bool IsValid(out string error)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                error = "Название дисциплины обязательно.";
                return false;
            }
            if (Hours <= 0 || Hours > 500)
            {
                error = "Часы должны быть в диапазоне 1–500.";
                return false;
            }
            if (Semester < 1 || Semester > 8)
            {
                error = "Семестр должен быть от 1 до 8.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Teacher))
            {
                error = "Преподаватель обязателен.";
                return false;
            }
            error = string.Empty;
            return true;
        }

        /// Клонирование объекта.
        public object Clone() => MemberwiseClone();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}