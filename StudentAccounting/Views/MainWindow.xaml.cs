using System.Windows;
using StudentAccounting.Services;
using StudentAccounting.ViewModels;

namespace StudentAccounting.Views
{
    /// Главное окно приложения. Содержит в себе вкладки Студенты и Дисциплины.
    /// Инициализирует DataContext с помощью MainViewModel.
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Получаем сервис данных из ресурсов приложения
            if (Application.Current.Properties["DataService"] is IDataService dataService)
            {
                DataContext = new MainViewModel(dataService);
            }
            else
            {
                MessageBox.Show("Ошибка инициализации сервиса данных.", "Критическая ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }
    }
}