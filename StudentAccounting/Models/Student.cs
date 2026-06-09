using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace StudentAccounting.Models
{
    public class Student : INotifyPropertyChanged, ICloneable
    {
        private int _id;
        private string _fullName = string.Empty;
        private string _group = string.Empty;
        private string _recordBookNumber = string.Empty;
        private double _averageScore;
        private string _educationForm = "Бюджет";

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }

        public string FullName
        {
            get => _fullName;
            set { _fullName = value; OnPropertyChanged(); }
        }

        public string Group
        {
            get => _group;
            set { _group = value; OnPropertyChanged(); }
        }

        public string RecordBookNumber
        {
            get => _recordBookNumber;
            set { _recordBookNumber = value; OnPropertyChanged(); }
        }

        public double AverageScore
        {
            get => _averageScore;
            set { _averageScore = value; OnPropertyChanged(); }
        }

        public string EducationForm
        {
            get => _educationForm;
            set { _educationForm = value; OnPropertyChanged(); }
        }

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

        public object Clone() => MemberwiseClone();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}