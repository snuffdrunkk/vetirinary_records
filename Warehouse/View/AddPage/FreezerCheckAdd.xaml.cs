using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Warehouse.DTO;
using Warehouse.Storage;

namespace Warehouse.View.AddPage
{

    public partial class FreezerCheckAdd : Window
    {
        DataGrid data;
        Database database = new Database();
        FreezerCheckStorage freezerCheckStorage = new FreezerCheckStorage();
        FreezerStorage freezerStorage = new FreezerStorage();
        public FreezerCheckAdd(DataGrid data)
        {
            InitializeComponent();
            WashingMethodBox.SelectionChanged += WashingMethodBox_SelectedIndexChanged;
            DisinfectionMethodBox.SelectionChanged += DisinfectionMethodBox_SelectionChanged;

            this.data = data;
            freezerStorage.ReadFreezerNameToComboBox(FreezerComboBox);

            string imagePath = "D:\\ДИПЛОМ\\warehouse-main\\Warehouse\\Resources\\logo.jpg";
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();
            imageControl.Source = bitmap;

            Date.SelectedDate = DateTime.Today;
            Date.IsEnabled = false;

        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxDTO dto = (ComboBoxDTO)FreezerComboBox.SelectedItem;
            string freezerCheckDate = Date.SelectedDate.Value.ToString("yyyy-MM-dd");
            string washingMethod = WashingMethodBox.Text;
            string detergent = DetergentBox.Text;
            string detergentQuantity = DetergentQuantityBox.Text;
            string disinfectionMethod = DisinfectionMethodBox.Text;
            string disinfectant = DisinfectantBox.Text;
            string disinfectantQuantity = DisinfectantQuantityBox.Text;

            if (ValidationCarCheckAdd(dto, washingMethod, detergent, detergentQuantity, disinfectionMethod, disinfectant, disinfectantQuantity)) 
            {
                freezerCheckStorage.CreateFreezerCheck(freezerCheckDate, washingMethod, detergent, detergentQuantity, disinfectionMethod, disinfectant, disinfectantQuantity, dto);
                freezerCheckStorage.ReadFreezerCheck(data);

                this.Close();
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool ValidationCarCheckAdd(ComboBoxDTO dto, string washingMethod, string detergent, string detergentQuantity, string disinfectionMethod, string disinfectant, string disinfectantQuantity)
        {
            if (!ValidationFreezer(dto))
                return false;

            if (!ValidationWashingMethod(washingMethod))
                return false;

            if (!ValidationDetergent(detergent))
                return false;

            if (!ValidationDetergentQuantity(detergentQuantity))
                return false;

            if (!ValidationDisinfectionMethod(disinfectionMethod))
                return false;

            if (!ValidationDisinfectant(disinfectant))
                return false;

            if (!ValidationDisinfectantQuantity(disinfectantQuantity))
                return false;

            return true;
        }

        private bool ValidationFreezer(ComboBoxDTO dto)
        {
            if (dto == null)
            {
                MessageBox.Show("Пожалуйста, выберите морозильную камеру.");
                return false;
            }

            return true;
        }

        private bool ValidationWashingMethod(string washingMethod)
        {
            if (string.IsNullOrEmpty(washingMethod))
            {
                MessageBox.Show("Пожалуйста, введите метод мойки.");
                return false;
            }

            return true;
        }

        private bool ValidationDetergent(string detergent)
        {
            if (string.IsNullOrEmpty(detergent))
            {
                MessageBox.Show("Пожалуйста, введите моющее средство.");
                return false;
            }

            return true;
        }

        private bool ValidationDetergentQuantity(string detergentQuantity)
        {
            if (string.IsNullOrEmpty(detergentQuantity))
            {
                MessageBox.Show("Пожалуйста, введите количество моющего средства.");
                return false;
            }

            int quantity;
            if (!int.TryParse(detergentQuantity, out quantity))
            {
                MessageBox.Show("Количество моющего средства должно быть числом.");
                return false;
            }

            if (quantity < 500 || quantity > 1000)
            {
                MessageBox.Show("Количество моющего средства должно быть в диапазоне от 500 до 1000.");
                return false;
            }


            return true;
        }

        private bool ValidationDisinfectionMethod(string disinfectionMethod)
        {
            if (string.IsNullOrEmpty(disinfectionMethod))
            {
                MessageBox.Show("Пожалуйста, введите метод дезинфекции.");
                return false;
            }

            return true;
        }

        private bool ValidationDisinfectant(string disinfectant)
        {
            if (string.IsNullOrEmpty(disinfectant))
            {
                MessageBox.Show("Пожалуйста, введите дезинфицирующее средство.");
                return false;
            }

            return true;
        }

        private bool ValidationDisinfectantQuantity(string disinfectantQuantity)
        {
            if (string.IsNullOrEmpty(disinfectantQuantity))
            {
                MessageBox.Show("Пожалуйста, введите количество дезинфицирующего средства.");
                return false;
            }

            int quantity;
            if (!int.TryParse(disinfectantQuantity, out quantity))
            {
                MessageBox.Show("Количество дезинфицирующего средства должно быть числом.");
                return false;
            }

            if (quantity < 500 || quantity > 1000)
            {
                MessageBox.Show("Количество дезинфицирующего средства должно быть в диапазоне от 500 до 1000.");
                return false;
            }

            return true;
        }

        private void WashingMethodBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (WashingMethodBox.SelectedIndex)
            {
                case 0:
                    DetergentBox.Text = "Вода";
                    break;
                case 1: 
                    DetergentBox.Text = "Горячая вода";
                    break;
                case 2: 
                    DetergentBox.Text = "Специальное средство";
                    break;
                case 3: 
                    DetergentBox.Text = "Горячая вода";
                    break;
                case 4: 
                    DetergentBox.Text = "Уксус";
                    break;
            }
        }

        private void DisinfectionMethodBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (DisinfectionMethodBox.SelectedIndex)
            {
                case 0:
                    DisinfectantBox.Text = "Хлорный раствор";
                    break;
                case 1:
                    DisinfectantBox.Text = "Спирт";
                    break;
                case 2:
                    DisinfectantBox.Text = "Перекись водорода";
                    break;
                case 3:
                    DisinfectantBox.Text = "Антибактериальное ср-во";
                    break;
                case 4:
                    DisinfectantBox.Text = "Уксусный раствор";
                    break;
            }
        }
    }
}
