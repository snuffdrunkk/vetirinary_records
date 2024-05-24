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

            if (ValidationFreezers(name, description, volume)) 
            {
                freezerStore.UpdateFreezer(id, name, description, volume);
                freezerStore.ReadFreezer(grid);

                this.Close();
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public bool ValidationFreezers(string name, string description, string volume)
        {
            if (!ValidationFreezerName(name))
                return false;

            if (!ValidationFreezerDescription(description))
                return false;

            if (!ValidationFreezerVolume(volume))
                return false;

            return true;
        }

        private bool ValidationFreezerName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Пожалуйста, введите название морозильной камеры.");
                return false;
            }

            if (name.Length > 50)
            {
                MessageBox.Show("Название морозильной камеры не должно превышать 50 символов.");
                return false;
            }

            return true;
        }

        private bool ValidationFreezerDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Пожалуйста, введите описание морозильной камеры.");
                return false;
            }

            if (description.Length > 50)
            {
                MessageBox.Show("Описание морозильной камеры не должно превышать 50 символов.");
                return false;
            }

            return true;
        }

        private bool ValidationFreezerVolume(string volume)
        {
            if (string.IsNullOrEmpty(volume))
            {
                MessageBox.Show("Пожалуйста, введите объем морозильной камеры.");
                return false;
            }

            if (!int.TryParse(volume, out int volumeValue) || volumeValue < 1000 || volumeValue > 5000)
            {
                MessageBox.Show("Объем морозильной камеры должен быть числом от 1000 до 5000 литров.");
                return false;
            }

            return true;
        }
    }
}
