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
            CarMarkComboBox.Text = mark;
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
            string mark = CarMarkComboBox.Text;
            string scrutiny = CarScrutinyComboBox.Text;

            if (ValidationCar(mark, scrutiny))
            {
                carStore.UpdateCar(id, mark, scrutiny);
                carStore.ReadCar(grid);
                this.Close();
            }
        }

        public bool ValidationCar(string mark, string scrutiny)
        {
            if (!ValidationCarMark(mark))
                return false;

            if (!ValidationCarScrutiny(scrutiny))
                return false;

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
