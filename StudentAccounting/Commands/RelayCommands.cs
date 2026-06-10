using System;
using System.Windows.Input;

namespace StudentAccounting.Commands
{
    /// Базовая реализация интерфейса ICommand для использования в MVVM.
    /// Позволяет привязывать методы ViewModel к командам в представлении.
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        /// Создаёт новую команду.

        /// "execute" Метод, вызываемый при выполнении команды.
        /// "canExecute" Метод, определяющий, доступна ли команда.
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }
        /// Определяет, может ли команда выполняться в текущем состоянии.
        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);


        /// Выполняет команду.

        public void Execute(object parameter) => _execute(parameter);

        /// Событие, возникающее при изменении возможности выполнения команды.
        /// Использует CommandManager.RequerySuggested для автоматического обновления.
        
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}