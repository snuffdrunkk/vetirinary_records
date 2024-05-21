using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Warehouse.Storage;

namespace Warehouse.View.EditPage
{
    public partial class FreezerEdit : Window
    {
        long id;

        DataGrid grid;
        FreezerStorage freezerStore = new FreezerStorage();
        public FreezerEdit(long id, string name, string description, string volume, DataGrid grid)
        {
            InitializeComponent();
            this.id = id;
            this.grid = grid;
            FreezerNameBox.Text = name;
            FreezerDescriptionBox.Text = description;
            FreezerVolumeBox.Text = volume;

            string imagePath = "D:\\ДИПЛОМ\\warehouse-main\\Warehouse\\Resources\\logo.jpg";

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();

            imageControl.Source = bitmap;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string name = FreezerNameBox.Text;
            string description = FreezerDescriptionBox.Text;
            string volume = FreezerVolumeBox.Text;

            /*            ValidationFileds validation = new ValidationFileds();
                        if (validation.ValidationProductTypeTitle(title))
                        {*/
            freezerStore.UpdateFreezer(id, name, description, volume);
            freezerStore.ReadFreezer(grid);

            this.Close();
            /*            }*/
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
