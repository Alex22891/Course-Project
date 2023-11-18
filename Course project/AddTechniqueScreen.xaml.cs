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
    /// Логика взаимодействия для AddTechniqueScreen.xaml
    /// </summary>
    public partial class AddTechniqueScreen : Window
    {
        public AddTechniqueScreen()
        {
            InitializeComponent();
        }

        DatabaseClass dt = new DatabaseClass();

        private void btn_AddTehnique_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(tb_model.Text) || String.IsNullOrEmpty(tb_invn.Text) || String.IsNullOrEmpty(tb_type.Text) || String.IsNullOrEmpty(tb_date.Text) || String.IsNullOrEmpty(tb_owner.Text) || String.IsNullOrEmpty(tb_class.Text))
            {
                MessageBox.Show("Заполните все поля");
            }
            else if (dt.AddTehnique(tb_model.Text, tb_invn.Text, tb_type.Text, tb_date.Text, tb_owner.Text, tb_class.Text) > 0)
            {
                MessageBox.Show("Данные успешно добавлены!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Произошла ошибка");
            }
        }
    }
}
