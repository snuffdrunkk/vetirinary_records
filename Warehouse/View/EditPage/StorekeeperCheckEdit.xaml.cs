using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Warehouse.Storage;

namespace Warehouse.View.EditPage
{
    public partial class StorekeeperCheckEdit : Window
    {
        long id;

        DataGrid grid;
        StorekeeperCheckStorage storekeeperCheckStorage = new StorekeeperCheckStorage();
        public StorekeeperCheckEdit(long id, string storekeeperSurnmae, string storekeeperCheckDate, string startDate, string endDate, string admissionStorekeeper, DataGrid grid)
        {
            InitializeComponent();

            string imagePath = "D:\\ДИПЛОМ\\warehouse-main\\Warehouse\\Resources\\logo.jpg";
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();
            imageControl.Source = bitmap;

            this.id = id;
            this.grid = grid;
            StorekeeperComboBox.Items.Add(storekeeperSurnmae);
            StorekeeperComboBox.SelectedIndex = 0;
            Date.SelectedDate = DateTime.Today;
            Date.IsEnabled = false;
            StartDate.Text = startDate;
            EndDate.Text = endDate;
            StorekeeperAdmissionBox.Text = admissionStorekeeper;
            StorekeeperAdmissionBox.SelectedIndex = 0;

            if (DateTime.Today < EndDate.SelectedDate)
            {
                StorekeeperAdmissionBox.SelectedIndex = 1;
                StorekeeperAdmissionBox.IsEnabled = false;
            }
            else
            {
                StorekeeperAdmissionBox.SelectedIndex = 0;
                StorekeeperAdmissionBox.IsEnabled = false;
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string storekeeperCheckDate = Date.SelectedDate.Value.ToString("yyyy-MM-dd"); ;
            string startDate = StartDate.SelectedDate.Value.ToString("yyyy-MM-dd"); ;
            string endDate = EndDate.SelectedDate.Value.ToString("yyyy-MM-dd"); ;
            string admissionStorekeeper = StorekeeperAdmissionBox.Text;

            if (ValidationdStorekeeperCheck(startDate, endDate, admissionStorekeeper))
            {
                storekeeperCheckStorage.UpdateStorekeeperCheck(id, storekeeperCheckDate, startDate, endDate, admissionStorekeeper);
                storekeeperCheckStorage.ReadStorekeeperCheck(grid);

                this.Close();
            }
        }

        internal bool ValidationdStorekeeperCheck(string startDate, string endDate, string admissionStorekeeper)
        {

            if (!ValidationStartDate(startDate))
                return false;

            if (!ValidationEndDate(endDate))
                return false;

            if (!ValidateDateRange(startDate, endDate))
                return false;

            if (!ValidateStorekeeperAdmission(endDate))
                return false;

            if (!ValidationStorekeeperAdmission(admissionStorekeeper))
                return false;

            return true;
        }

        private bool ValidateDateRange(string startDate, string endDate)
        {
            if (!DateTime.TryParse(startDate, out DateTime startDateTime) ||
                !DateTime.TryParse(endDate, out DateTime endDateTime))
            {
                MessageBox.Show("Неверный формат даты.");
                return false;
            }

            if (endDateTime.Year - startDateTime.Year != 1)
            {
                MessageBox.Show("Диапазон между датами должен быть равен 1 году.");
                return false;
            }

            return true;
        }

        private bool ValidateStorekeeperAdmission(string endDate)
        {
            if (!DateTime.TryParse(endDate, out DateTime endDateTime))
            {
                MessageBox.Show("Неверный формат даты.");
                return false;
            }

            if (DateTime.Today < endDateTime)
            {
                StorekeeperAdmissionBox.SelectedIndex = 1; 
                StorekeeperAdmissionBox.IsEnabled = false; 
                return true;
            }
            else
            {
                StorekeeperAdmissionBox.SelectedIndex = 0; 
                StorekeeperAdmissionBox.IsEnabled = false; 
                return true;
            }
        }

        private bool ValidationStartDate(string startDate)
        {
            if (string.IsNullOrEmpty(startDate))
            {
                MessageBox.Show("Пожалуйста, выберите дату начала действия справки.");
                return false;
            }

            return true;
        }

        private bool ValidationEndDate(string endlDate)
        {
            if (string.IsNullOrEmpty(endlDate))
            {
                MessageBox.Show("Пожалуйста, выберите дату окончания действия справки.");
                return false;
            }

            return true;
        }

        private bool ValidationStorekeeperAdmission(string admissionStorekeeper)
        {
            if (string.IsNullOrEmpty(admissionStorekeeper))
            {
                MessageBox.Show("Пожалуйста, введите допуск кладовщика.");
                return false;
            }

            return true;
        }
    }
}
