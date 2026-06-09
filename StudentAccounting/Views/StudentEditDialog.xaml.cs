using System.Windows;
using StudentAccounting.Models;
using StudentAccounting.ViewModels;

namespace StudentAccounting.Views
{
    /// Диалоговое окно для добавления или редактирования студента
    /// Принимает копию студента.
    /// Возвращает editedStudent при успешном сохранении.
    public partial class StudentEditDialog : Window
    {
        public Student EditedStudent { get; private set; }

        public StudentEditDialog(Student student)
        {
            InitializeComponent();
            var vm = new StudentEditViewModel(student);
            vm.CloseRequest += (result) =>
            {
                if (result == true)
                    EditedStudent = vm.Student;
                DialogResult = result;
                Close();
            };
            DataContext = vm;
        }
    }
}