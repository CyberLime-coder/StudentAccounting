using System;
using System.Windows.Input;
using StudentAccounting.Models;
using StudentAccounting.Commands;

namespace StudentAccounting.ViewModels
{
    /// ViewModel для диалога редактирования дисциплины.
    /// Управляет полями, валидацией, командами "Сохранить" и "Отмена".
    public class DisciplineEditViewModel : BaseViewModel
    {
        private Discipline _discipline;
        private string _errorMessage = "";
        private bool _isSaveEnabled;

        public DisciplineEditViewModel(Discipline discipline)
        {
            _discipline = discipline?.Clone() as Discipline ?? new Discipline();
            SaveCommand = new RelayCommand(_ => Save(), _ => IsSaveEnabled);
            CancelCommand = new RelayCommand(_ => Cancel());
            Validate();
        }

        /// Редактируемая дисциплина.
        public Discipline Discipline
        {
            get => _discipline;
            set { _discipline = value; OnPropertyChanged(); Validate(); }
        }

        /// Текст последней ошибки валидации.
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        /// Доступна ли кнопка "Сохранить".
        public bool IsSaveEnabled
        {
            get => _isSaveEnabled;
            set { _isSaveEnabled = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        /// Событие для закрытия окна с результатом (true – сохранить, false – отмена).
        public event Action<bool?> CloseRequest;

        /// Проверяет корректность полей дисциплины и обновляет IsSaveEnabled и ErrorMessage.
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