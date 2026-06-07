using System.Linq;
using System.Windows;

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
            var user = Core.Context.Users.FirstOrDefault(u => u.Login == login &&
           u.Password == password);
            if (user == null)
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
            var newUser = new Users
            {
                Login = login,
                Password = password,
            };
            Core.Context.Users.Add(newUser);
            Core.Context.SaveChanges();
            MessageBox.Show("Вы успешно зарегистрированы!");
        }
    }
}