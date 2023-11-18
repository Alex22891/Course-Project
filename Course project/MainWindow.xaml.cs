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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Course_project
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string Login { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            Profile prof = new Profile();
            prof.Name = Name;
            prof.Position = Position;
            prof.Login = Login;
            prof.Show();
            this.Close();
        }

        private void btnAccounting_Click(object sender, RoutedEventArgs e)
        {
            TechniqueScreen technique = new TechniqueScreen();
            this.Close();
            technique.Show();
        }

        private void btnDecommissioned_Click(object sender, RoutedEventArgs e)
        {
            DecommissionedEquipmentScreen decommissioned = new DecommissionedEquipmentScreen();
            this.Close();
            decommissioned.Show();
        }
    }
}
