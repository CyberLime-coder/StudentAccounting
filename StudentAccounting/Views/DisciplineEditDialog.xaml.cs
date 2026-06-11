using System.Windows;
using StudentAccounting.Models;
using StudentAccounting.ViewModels;

namespace StudentAccounting.Views
{
    // Окно для добавления/редактирования дисциплины
    public partial class DisciplineEditDialog : Window
    {
        // Свойство, в которое сохраняется отредактированная дисциплина
        // (если пользователь нажал "Сохранить")
        public Discipline EditedDiscipline { get; private set; }

        // Конструктор. Принимает дисциплину для редактирования (может быть null)
        public DisciplineEditDialog(Discipline discipline)
        {
            InitializeComponent(); // Обязательный вызов для загрузки XAML

            // Создаём ViewModel для этого диалога
            DisciplineEditViewModel vm = new DisciplineEditViewModel(discipline);

            // Подписываемся на событие запроса закрытия от ViewModel
            vm.CloseRequest += (result) =>
            {
                // Если результат true – пользователь подтвердил сохранение
                if (result == true)
                    EditedDiscipline = vm.Discipline; // Сохраняем отредактированную дисциплину

                // Устанавливаем результат диалога (true/false/null)
                DialogResult = result;

                // Закрываем окно
                Close();
            };

            // Устанавливаем DataContext окна, чтобы привязки в XAML работали с этой ViewModel
            DataContext = vm;
        }
    }
}