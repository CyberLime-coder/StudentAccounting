using System.Windows;

/// Окно авторизации. Проверяет введённые логин и пароль.
/// При успехе открывает главное окно и закрывается

namespace StudentAccounting.Views
{
    /// Окно авторизации. Проверяет введённые логин и пароль.
    /// При успехе открывает главное окно и закрывается.
    public partial class AuthWindow : Window
    {
        public AuthWindow() => InitializeComponent();

        // Обработчик кнопки Вход
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            // учётная запись как указанная в нашем ТЗ и логин и пароль к ней
            if (LoginBox.Text == "dean" && PasswordBox.Password == "univer123")
            {
                var mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Ошибка авторизации. Проверьте логин и пароль",
                                                "Ошибка",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Error);
                LoginBox.Clear();
                PasswordBox.Clear();
            }
        }

        // Обработчик кнопки Выход
        private void Exit_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
    }
}