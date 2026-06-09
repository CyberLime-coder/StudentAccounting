using System.Windows;
using StudentAccounting.Models;
using StudentAccounting.ViewModels;

namespace StudentAccounting.Views
{
    /// Диалоговое окно для добавления или редактирования дисциплины
    /// Принимает копию дисциплины.
    /// Возвращает EditedDiscipline при успешном сохранении.
    public partial class DisciplineEditDialog : Window
    {
        /// Отредактированная дисциплина (null, если пользователь отменил).
        public Discipline EditedDiscipline { get; private set; }

        public DisciplineEditDialog(Discipline discipline)
        {
            InitializeComponent();
            // Создание ViewModel, передающая клон исходной дисциплины
            var vm = new DisciplineEditViewModel(discipline?.Clone() as Discipline);
            // Подписываемся на событие закрытия от ViewModel
            vm.CloseRequest += (result) =>
            {
                if (result == true)
                    EditedDiscipline = vm.Discipline;   // Сохранение отредактированной дисциплины
                DialogResult = result;                  // Устанавливание результата диалога
                Close();                                // Закрытие окна
            };
            DataContext = vm;
        }
    }
}