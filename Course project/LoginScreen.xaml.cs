using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Course_project
{
    /// <summary>
    /// Логика взаимодействия для LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public string name { get; set; }
        public string Position { get; set; }
        public string Login { get; set; }
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void btn_authorization_Click(object sender, RoutedEventArgs e)
        {
            string enteredLogin = tb_login.Text;
            string enteredPassword = pass.Password;
            if (successfulLogin(enteredLogin, enteredPassword))
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Name = Name;
                mainWindow.Position = Position;
                mainWindow.Login = Login;
                mainWindow.Show();
                this.Close();
            }
        }

        private bool successfulLogin(string enteredLogin, string enteredPassword)
        {
            string connectionString = "server=localhost;port=3307;user=root;database=database_courseproject;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM users WHERE login = @login AND password = @password";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@login", enteredLogin);
                command.Parameters.AddWithValue("@password", enteredPassword);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        MessageBox.Show("Вы успешно зашли в свой аккаунт!");
                        Name = reader["name"].ToString();
                        Position = reader["position"].ToString();
                        Login = reader["login"].ToString();
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль");
                        return false;
                    }
                }
            }
        }

        private void tb_login_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb_login.Text))
            {
                btn_authorization.IsEnabled = false;
            }
            else
            {
                btn_authorization.IsEnabled = true;
            }
        }
    }
}
