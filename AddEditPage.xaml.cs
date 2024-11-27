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

        private int edit = 0;
        public AddEditPage( Service SelectedService)
        {
            InitializeComponent();

            if (SelectedService != null)
            {
                _currentServise = SelectedService;
                edit = 1;
            }

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


            if ( _currentServise.DurationInSeconds < 0 && _currentServise.DurationInSeconds>240)
            {
                errors.AppendLine("Длительность не может быть больше 240 минут или меньше 0 ");
            }
           


            
            /*if (string.IsNullOrWhiteSpace(_currentServise.DurationInSeconds))
            {
                errors.AppendLine("Укажите длительность услуги");
            }
            */
            //уфыасу


            if ((_currentServise.DiscountInt <0 || _currentServise.DiscountInt>100) || Disc.Text.Length < 1)
            {
                errors.AppendLine("Укажите скидку");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            //проверка существует ли такая услуга
            
            var allServices = Gerasimova_AvtoservicEntities.GetContext().Service.ToList();
            allServices = allServices.Where(p => p.Title == _currentServise.Title).ToList();

            if (allServices.Count == 0 || edit == 1)
            {
                if (_currentServise.ID == 0)
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
            else
            {
                
                    MessageBox.Show("Уже существует такая услуга");
                
                
            }


        }
    }
}
