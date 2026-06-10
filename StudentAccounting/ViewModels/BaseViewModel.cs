using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StudentAccounting.ViewModels
{
    /// Базовый класс ViewModel, реализующий INotifyPropertyChanged.
    /// Все ViewModel наследуются от него, чтобы автоматически уведомлять представление об изменениях.
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// Вызывает событие PropertyChanged.

        /// "name" Имя свойства, которое изменилось (автоматически подставляется).
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}