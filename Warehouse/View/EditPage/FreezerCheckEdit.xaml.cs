﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Warehouse.DTO;
using Warehouse.Storage;
using Warehouse.View.AddPage;

namespace Warehouse.View.EditPage
{
    public partial class FreezerCheckEdit : Window
    {
        DataGrid grid;
        Database database = new Database();
        FreezerCheckStorage freezerCheckStorage = new FreezerCheckStorage();

        int id;

        public FreezerCheckEdit(int id, string freezerName, string freezerCheckDate, string washingMethod, string detergent, string detergentQuantity, string disinfectionMethod, string disinfectant, string disinfectantQuantity, DataGrid grid)
        {
            InitializeComponent();

            string imagePath = "D:\\ДИПЛОМ\\warehouse-main\\Warehouse\\Resources\\logo.jpg";
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();
            imageControl.Source = bitmap;

            this.grid = grid;
            this.id = id;
            FreezerComboBox.Items.Add(freezerName);
            Date.Text = freezerCheckDate;
            WashingMethodBox.Text = washingMethod;
            DetergentBox.Text = detergent;
            DetergentQuantityBox.Text = detergentQuantity;
            DisinfectionMethodBox.Text = disinfectionMethod;
            DisinfectantBox.Text = disinfectant;
            DisinfectantQuantityBox.Text = disinfectantQuantity;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string washingMethod = WashingMethodBox.Text;
            string detergent = DetergentBox.Text;
            string detergentQuantity = DetergentQuantityBox.Text;
            string disinfectionMethod = DisinfectionMethodBox.Text;
            string disinfectant = DisinfectantBox.Text;
            string disinfectantQuantity = DisinfectantQuantityBox.Text;

            freezerCheckStorage.UpdateFreezerCheck(id, washingMethod, detergent, detergentQuantity, disinfectionMethod, disinfectant, disinfectantQuantity);
            freezerCheckStorage.ReadFreezerCheck(grid);

            this.Close();
        }

        private void BackToSecondPage_Click(object sender, RoutedEventArgs e)
        {
            WashingMethodBox.Visibility = Visibility.Visible;
            DetergentBox.Visibility = Visibility.Visible;
            DetergentQuantityBox.Visibility = Visibility.Visible;

            DisinfectionMethodBox.Visibility = Visibility.Collapsed;
            DisinfectantBox.Visibility = Visibility.Collapsed;
            DisinfectantQuantityBox.Visibility = Visibility.Collapsed;

            BackToFirstPage.Visibility = Visibility.Visible;
            ToThirdPage.Visibility = Visibility.Visible;

            BackToSecondPage.Visibility = Visibility.Collapsed;
            Confirm.Visibility = Visibility.Collapsed;
        }

        private void ToThirdPage_Click(object sender, RoutedEventArgs e)
        {
            WashingMethodBox.Visibility = Visibility.Collapsed;
            DetergentBox.Visibility = Visibility.Collapsed;
            DetergentQuantityBox.Visibility = Visibility.Collapsed;

            DisinfectionMethodBox.Visibility = Visibility.Visible;
            DisinfectantBox.Visibility = Visibility.Visible;
            DisinfectantQuantityBox.Visibility = Visibility.Visible;

            BackToFirstPage.Visibility = Visibility.Collapsed;
            ToThirdPage.Visibility = Visibility.Collapsed;

            BackToSecondPage.Visibility = Visibility.Visible;
            Confirm.Visibility = Visibility.Visible;
        }

        private void BackToFirstPage_Click(object sender, RoutedEventArgs e)
        {
            FreezerComboBox.Visibility = Visibility.Visible;
            Date.Visibility = Visibility.Visible;

            WashingMethodBox.Visibility = Visibility.Collapsed;
            DetergentBox.Visibility = Visibility.Collapsed;
            DetergentQuantityBox.Visibility = Visibility.Collapsed;

            Return.Visibility = Visibility.Visible;
            ToSecondPage.Visibility = Visibility.Visible;

            BackToFirstPage.Visibility = Visibility.Collapsed;
            ToThirdPage.Visibility = Visibility.Collapsed;
        }

        private void ToSecondPage_Click(object sender, RoutedEventArgs e)
        {
            FreezerComboBox.Visibility = Visibility.Collapsed;
            Date.Visibility = Visibility.Collapsed;

            WashingMethodBox.Visibility = Visibility.Visible;
            DetergentBox.Visibility = Visibility.Visible;
            DetergentQuantityBox.Visibility = Visibility.Visible;

            Return.Visibility = Visibility.Collapsed;
            ToSecondPage.Visibility = Visibility.Collapsed;

            BackToFirstPage.Visibility = Visibility.Visible;
            ToThirdPage.Visibility = Visibility.Visible;
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
