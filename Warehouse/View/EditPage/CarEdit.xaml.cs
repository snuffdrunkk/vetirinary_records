using System;
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

/*            ValidationFileds validation = new ValidationFileds();
            if (validation.ValidationProductTypeTitle(title))
            {*/
                carStore.UpdateCar(id, number, mark, scrutiny);
                carStore.ReadCar(grid);

                this.Close();
/*            }*/
        }
    }
}
