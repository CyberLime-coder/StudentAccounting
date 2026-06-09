using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StudentAccounting.Models
{
    public class Discipline : INotifyPropertyChanged, ICloneable
    {
        private int _id;
        private string _name = string.Empty;
        private int _hours;
        private int _semester;
        private string _teacher = string.Empty;

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public int Hours
        {
            get => _hours;
            set { _hours = value; OnPropertyChanged(); }
        }

        public int Semester
        {
            get => _semester;
            set { _semester = value; OnPropertyChanged(); }
        }

        public string Teacher
        {
            get => _teacher;
            set { _teacher = value; OnPropertyChanged(); }
        }

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

        public object Clone() => MemberwiseClone();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}