using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CarBuilding.View
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
            LoginBox.Focus();
        }
        private void Login(object sender, RoutedEventArgs e)
        {
            string login = LoginBox.Text.Trim();
            string password = LoginPasswordBox.Password;
            if (string.IsNullOrEmpty(login))
            {
                MessageBox.Show("Введите логин");
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите пароль");
                return;
            }
            var user = Core.Context.Users.FirstOrDefault(u => u.Login == login);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                MessageBox.Show("Неверный логин или пароль");
                return;
            }
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void Register(object sender, RoutedEventArgs e)
        {
            string login = RegLoginBox.Text.Trim();
            string password = RegPasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;
            // Проверка заполнения полей
            if (string.IsNullOrEmpty(login))
            {
                MessageBox.Show("Введите логин");
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите пароль");
                return;
            }
            if (password != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }
            if (Core.Context.Users.Any(u => u.Login == login))
            {
                MessageBox.Show("Пользователь с таким логином уже существует");
                return;
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var newUser = new Users
            {
                Login = login,
                Password = hashedPassword,
            };
            Core.Context.Users.Add(newUser);
            Core.Context.SaveChanges();
            MessageBox.Show("Вы успешно зарегистрированы!");
        }
        private void MoveToNextElement_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);

                UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;

                if (elementWithFocus != null)
                {
                    elementWithFocus.MoveFocus(request);
                }
            }
        }
    }
}