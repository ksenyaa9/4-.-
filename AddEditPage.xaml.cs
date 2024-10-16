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

namespace _1_лаба
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {

        private Service _currentServise = new Service();
        public AddEditPage( Service SelectedService)
        {
            InitializeComponent();

            if (SelectedService != null)
                _currentServise = SelectedService;

            DataContext = _currentServise;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentServise.Title))
            {
                errors.AppendLine("Укажите навзание услуги");
            }
            if (_currentServise.Cost == 0 || _currentServise.Cost<0)
            {
                errors.AppendLine("Укажите стоимость услуги");
            }

            if (string.IsNullOrWhiteSpace(_currentServise.DurationInSeconds))
            {
                errors.AppendLine("Укажите длительность услуги");
            }

            


            if ((_currentServise.Discount <0 || _currentServise.Discount>100) || Disc.Text.Length < 1)
            {
                errors.AppendLine("Укажите скидку");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            
            if(_currentServise.ID == 0)
            {
                Gerasimova_AvtoservicEntities.GetContext().Service.Add(_currentServise);
            }

            try
            {
                Gerasimova_AvtoservicEntities.GetContext().SaveChanges();
                MessageBox.Show("информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }



        }
    }
}
