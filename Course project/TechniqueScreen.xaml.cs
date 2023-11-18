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
    /// Логика взаимодействия для TechniqueScreen.xaml
    /// </summary>
    public partial class TechniqueScreen : Window
    {
        private string connectionString = "server=127.0.0.1;port=3307;user=root;database=database_courseproject;";
        public TechniqueScreen()
        {
            InitializeComponent();
            dgTechique.ItemsSource = dt.ReadTechnique();
        }
        DatabaseClass dt = new DatabaseClass();

        private void LoadData()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string selectSql = "SELECT * FROM Technique";
                using (MySqlCommand selectCommand = new MySqlCommand(selectSql, connection))
                {
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(selectCommand);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    dgTechique.ItemsSource = dataTable.DefaultView;
                }
            }
        }
        private void btnAddTechnique_Click(object sender, RoutedEventArgs e)
        {
            AddTechniqueScreen addTechnique = new AddTechniqueScreen();
            addTechnique.Show();
        }

        private void btnDeleteTechnique_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = dgTechique.SelectedItem;

            if (selectedItem == null)
            {
                MessageBox.Show("Пожалуйста выберите какую технику удалить");
                return;
            }
            if (MessageBox.Show("Вы действительно хотите удалить технику?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Technique WHERE id=@id";
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
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void btnTransfer_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = dgTechique.SelectedItems;
            if (MessageBox.Show("Вы действительно хотите списать технику?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (DatabaseClass item in selectedItems)
                {
                    string model = item.model;
                    string type = item.type;
                    string owner = item.owner;

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = "INSERT INTO DecommissionedTechnique (model, type, OldOwner) SELECT @model, @type, @owner";
                    cmd.Parameters.AddWithValue("@model", model);
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters.AddWithValue("@owner", owner);

                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.ExecuteNonQuery();
                    }

                    MySqlCommand cmdDelete = new MySqlCommand();
                    cmdDelete.CommandText = "DELETE FROM Technique WHERE id = @id";
                    cmdDelete.Parameters.AddWithValue("@id", item.id);

                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        cmdDelete.Connection = conn;
                        cmdDelete.ExecuteNonQuery();
                    }
                }
                LoadData();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchParameter = tbSearch.Text.Trim();
            string query;
            if (!string.IsNullOrEmpty(searchParameter))
            {
                query = "SELECT * FROM Technique WHERE type LIKE @searchParameter OR owner LIKE @searchParameter OR Class LIKE @searchParameter";
            }
            else
            {
                query = "SELECT * FROM Technique";
            }
            MySqlConnection connection = new MySqlConnection("server=127.0.0.1;port=3307;database=database_courseproject;uid=root;");
            MySqlCommand command = new MySqlCommand(query, connection);
            if (!string.IsNullOrEmpty(searchParameter))
            {
                command.Parameters.AddWithValue("@searchParameter", "%" + searchParameter + "%");
            }
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dgTechique.ItemsSource = table.DefaultView;
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            var data = (List<DatabaseClass>)dgTechique.ItemsSource;

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "UPDATE Technique SET model = @model, type = @type, owner = @owner, class = @Class WHERE id = @id";
            cmd.Parameters.Add("@id", MySqlDbType.Int32);
            cmd.Parameters.Add("@model", MySqlDbType.VarChar);
            cmd.Parameters.Add("@type", MySqlDbType.VarChar);
            cmd.Parameters.Add("@owner", MySqlDbType.VarChar);
            cmd.Parameters.Add("@Class", MySqlDbType.Int32);

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                cmd.Connection = conn;
                foreach (DatabaseClass item in data)
                {
                    cmd.Parameters["@id"].Value = item.id;
                    cmd.Parameters["@model"].Value = item.model;
                    cmd.Parameters["@type"].Value = item.type;
                    cmd.Parameters["@owner"].Value = item.owner;
                    cmd.Parameters["@Class"].Value = item.Class;
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();
        }
    }
}
