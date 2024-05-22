using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Warehouse.Storage;

namespace Warehouse.View.AddPage
{

    public partial class CarAdd : Window
    {
        DataGrid data;
        Database database = new Database();
        CarStorage carStorage = new CarStorage();

        public CarAdd(DataGrid data)
        {
            InitializeComponent();
            this.data = data;

            string imagePath = "D:\\ДИПЛОМ\\warehouse-main\\Warehouse\\Resources\\logo.jpg";
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();
            imageControl.Source = bitmap;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string number = CarNumberBox.Text;
            string mark = CarMarkComboBox.Text;
            string scrutiny = CarScrutinyComboBox.Text;

            if (ValidationCar(number, mark, scrutiny))
            {
                carStorage.CreateCar(number, mark, scrutiny);
                carStorage.ReadCar(data);
                this.Close();
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public bool ValidationCar(string number, string mark, string scrutiny)
        {
            if (!ValidationCarNumber(number))
                return false;

            if (!ValidationCarMark(mark))
                return false;

            if (!ValidationCarScrutiny(scrutiny))
                return false;

            return true;
        }

        private bool ValidationCarNumber(string number)
        {
            if (!carStorage.CountCarNum(number))
            {
                MessageBox.Show("Этот номер автомобиля уже существует в базе данных.");
                return false;
            }

            if (string.IsNullOrEmpty(number))
            {
                MessageBox.Show("Пожалуйста, введите номер автомобиля.");
                return false;
            }

            string pattern = @"^\d{4}[A-Z]{2}-[1-7]$";
            if (!Regex.IsMatch(number, pattern))
            {
                MessageBox.Show("Номер автомобиля должен соответствовать формату: 4 цифры, 2 английские буквы в верхнем регистре, тире, затем цифра от 1 до 7.");
                return false;
            }
            return true;
        }

        private bool ValidationCarMark(string mark)
        {
            if (string.IsNullOrEmpty(mark))
            {
                MessageBox.Show("Выберите марку авто!");
                return false;
            }

            return true;
        }

        private bool ValidationCarScrutiny(string scrutiny)
        {
            if (string.IsNullOrEmpty(scrutiny))
            {
                MessageBox.Show("Пожалуйста, выберите тип осмотра автомобиля.");
                return false;
            }

            return true;
        }
    }
}
