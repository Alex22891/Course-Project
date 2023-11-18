using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для DecommissionedEquipmentScreen.xaml
    /// </summary>
    public partial class DecommissionedEquipmentScreen : Window
    {
        private string connectionString = "server=127.0.0.1;port=3307;user=root;database=database_courseproject;";
        public DecommissionedEquipmentScreen()
        {
            InitializeComponent();
            dgDecommissionedTechique.ItemsSource = dt.ReadDecommissionedTechnique();
        }

        DatabaseClass dt = new DatabaseClass();

        private void LoadData()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string selectSql = "SELECT * FROM DecommissionedTechnique";
                using (MySqlCommand selectCommand = new MySqlCommand(selectSql, connection))
                {
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(selectCommand);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    dgDecommissionedTechique.ItemsSource = dataTable.DefaultView;
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = dgDecommissionedTechique.SelectedItem;

            if (selectedItem == null)
            {
                MessageBox.Show("Пожалуйста выберите какую технику удалить");
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM DecommissionedTechnique WHERE id=@id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", (int)selectedItem.GetType().GetProperty("id").GetValue(selectedItem));
                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Техника успешно удалена!");
                }
                else
                {
                    MessageBox.Show("Ошибка удаления техника");
                }
            }
            LoadData();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
