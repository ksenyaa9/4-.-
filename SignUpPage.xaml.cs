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
    /// Логика взаимодействия для SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {

        private Service _currentService = new Service();
        public SignUpPage(Service SelectedService)
        {
            InitializeComponent();
            if (SelectedService != null)
                this._currentService = SelectedService;

            DataContext = _currentService;


            var _currentClient = Gerasimova_AvtoservicEntities.GetContext().Client.ToList();

            ComboClient.ItemsSource = _currentClient;
        }

        private ClientService _currentClientService = new ClientService();
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            StringBuilder errors = new StringBuilder();

            if (ComboClient.SelectedItem == null)
                errors.AppendLine("Укажите ФИО клиента");

            if (StartDate.Text == "")
                errors.AppendLine("Укажите дату услуги");
            if (TBStart.Text == "")
                errors.AppendLine("Укажите время начала услуги");

            if (!proverka(TBStart.Text))
            {
                errors.AppendLine("Укажите правильный формат времени");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;

            }

            _currentClientService.ClientID = ComboClient.SelectedIndex + 1;
            _currentClientService.ServiceID = _currentService.ID;
            _currentClientService.StartTime = Convert.ToDateTime(StartDate.Text + " " + TBStart.Text);
            if (_currentClientService.ID == 0)
            {
                Gerasimova_AvtoservicEntities.GetContext().ClientService.Add(_currentClientService);
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


        //редачить
        private void TBStart_TextChanged(object sender, TextChangedEventArgs e)
        {
            string s = TBStart.Text;
            StringBuilder errors = new StringBuilder();


            if (s.Length < 5 || !s.Contains(':'))
            {
                TBEnd.Text = "";


                return;
            }
            else
            {
                string[] start = s.Split(new char[] { ':' });

                int startHour = Convert.ToInt32(start[0].ToString()) * 60;
                int startMin = Convert.ToInt32(start[1].ToString());

                int sum = startHour + startMin + _currentService.DurationInSeconds;

                int endHour = sum / 60;
                int endMin = sum % 60;
                endHour %= 24;

                // добавление 0 если меньше 10
                string endHourStr = endHour < 10 ? "0" + endHour.ToString() : endHour.ToString();
                string endMinStr = endMin < 10 ? "0" + endMin.ToString() : endMin.ToString();

                s = endHourStr.ToString() + ":" + endMinStr.ToString();

              TBEnd.Text = s;
                


            }

        }

        private bool proverka(string time)
        {
            string[] parts = time.Split(new char[] { ':' });
            if (parts.Length != 2) return false;

            if (!int.TryParse(parts[0], out int hour) || !int.TryParse(parts[1], out int minute))
                return false;

            if (hour < 0 || hour > 23 || minute < 0 || minute > 59)
                return false;

            return true;
        }

    }
}

    
