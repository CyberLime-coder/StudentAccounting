using System;
using System.Windows.Input;
using StudentAccounting.Models;
using StudentAccounting.Commands;

namespace StudentAccounting.ViewModels
{
    public class DisciplineEditViewModel : BaseViewModel
    {
        private Discipline _discipline;
        private string _errorMessage = string.Empty;
        private bool _isSaveEnabled;

        public DisciplineEditViewModel(Discipline discipline)
        {
            _discipline = discipline?.Clone() as Discipline ?? new Discipline();
            SaveCommand = new RelayCommand(_ => Save(), _ => IsSaveEnabled);
            CancelCommand = new RelayCommand(_ => Cancel());
            Validate();
        }

        public Discipline Discipline
        {
            get => _discipline;
            set { _discipline = value; OnPropertyChanged(); Validate(); }
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

        private void Validate()
        {
            bool isValid = _discipline.IsValid(out string error);
            ErrorMessage = error;
            IsSaveEnabled = isValid;
        }

        private void Save() => CloseRequest?.Invoke(true);
        private void Cancel() => CloseRequest?.Invoke(false);
    }
}