using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Warehouse.Storage;

namespace Warehouse.View.EditPage
{
    public partial class EditCar : Window
    {
        long id;

        DataGrid grid;
        CarStorage carStore = new CarStorage();

        public EditCar(long id, string number, string mark, string scrutiny, DataGrid grid)
        {
            InitializeComponent();
            this.id = id;
            this.grid = grid;
            CarNumberBox.Text = number;
            CarMarkBox.Text = mark;
            CarScrutinyComboBox.Text = scrutiny;

            string imagePath = "D:\\ДИПЛОМ\\warehouse-main\\Warehouse\\Resources\\logo.jpg";

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();

            imageControl.Source = bitmap;
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string number = CarNumberBox.Text;
            string mark = CarMarkBox.Text;
            string scrutiny = CarScrutinyComboBox.Text;

            if (ValidationCar(number, mark, scrutiny))
            {
                carStore.UpdateCar(id, number, mark, scrutiny);
                carStore.ReadCar(grid);

                this.Close();
             }
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
