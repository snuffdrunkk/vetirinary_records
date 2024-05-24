﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Warehouse.DTO;
using Warehouse.Storage;

namespace Warehouse.View.AddPage
{
    public partial class CarCheckAdd : Window
    {
        DataGrid data;
        CarCheckStorage carCheckStorage = new CarCheckStorage();
        CarStorage carStorage = new CarStorage();

        public CarCheckAdd(DataGrid data)
        {
            InitializeComponent();
            this.data = data;
            carStorage.ReadCarNumberToComboBox(CarComboBox);

            string imagePath = "D:\\ДИПЛОМ\\warehouse-main\\Warehouse\\Resources\\logo.jpg";
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();
            imageControl.Source = bitmap;

            Date.SelectedDate = DateTime.Today;
            Date.IsEnabled = false;
            ArrivalDate.SelectedDate = DateTime.Today;
            ArrivalDate.IsEnabled = false;
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxDTO dto = (ComboBoxDTO)CarComboBox.SelectedItem;
            string carCheckDate = Date.SelectedDate.Value.ToString("yyyy-MM-dd");
            string arrivalDate = ArrivalDate.SelectedDate.Value.ToString("yyyy-MM-dd");
            string admissionCar = CarAdmissionBox.Text;
            string carTemperature = CarTemperatureBox.Text;

            if (ValidationCarCheck(arrivalDate, admissionCar, carTemperature, dto)) 
            {
                carCheckStorage.CreateCarCheck(carCheckDate, arrivalDate, admissionCar, carTemperature, dto);
                carCheckStorage.ReadCarCheck(data);

                this.Close();
            }
        }

        internal bool ValidationCarCheck(string arrivalDate, string admissionCar, string carTemperature, ComboBoxDTO dto)
        {
            if (!ValidationArrivalDate(arrivalDate))
                return false;

            if (!ValidationCarAdmission(admissionCar))
                return false;

            if (!ValidationCarTemperature(carTemperature))
                return false;

            if (!ValidationCar(dto))
                return false;

            return true;
        }

        private bool ValidationArrivalDate(string arrivalDate)
        {
            if (string.IsNullOrEmpty(arrivalDate))
            {
                MessageBox.Show("Пожалуйста, выберите дату прибытия автомобиля.");
                return false;
            }

            if (DateTime.Parse(arrivalDate) != DateTime.Today)
            {
                MessageBox.Show("Дата приезда машины должна быть сегодняшней.");
                return false;
            }

            return true;
        }

        private bool ValidationCarAdmission(string admissionCar)
        {
            if (string.IsNullOrEmpty(admissionCar))
            {
                MessageBox.Show("Пожалуйста, введите допуск осмотра автомобиля.");
                return false;
            }

            return true;
        }

        private bool ValidationCarTemperature(string carTemperature)
        {
            if (string.IsNullOrEmpty(carTemperature))
            {
                MessageBox.Show("Пожалуйста, введите температуру автомобиля.");
                return false;
            }

            if (!decimal.TryParse(carTemperature, out decimal temperature) || temperature < -18m || temperature > 4m)
            {
                MessageBox.Show("Температура автомобиля должна быть числом от -18°C до 4°C.");
                return false;
            }

            return true;
        }

        internal bool ValidationCar(ComboBoxDTO dto)
        {
            if (dto == null)
            {
                MessageBox.Show("Пожалуйста, выберите машину.");
                return false;
            }

            return true;
        }
    }
}
