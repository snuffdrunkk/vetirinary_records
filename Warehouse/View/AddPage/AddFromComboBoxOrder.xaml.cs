﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Warehouse.DTO;
using Warehouse.Service;
using Warehouse.Storage;

namespace Warehouse.View.AddPage
{
    public partial class AddFromComboBoxOrder : Window
    {
        double amount = 0;

        DataGrid grid;
        TextBox TextBox;
        Database database = new Database();

        private bool isPosted;

        public AddFromComboBoxOrder(DataGrid grid, TextBox textBox, bool isPosted)
        {
            InitializeComponent();

            this.grid = grid;
            this.TextBox = textBox;
            this.isPosted = isPosted;

            if (isPosted)
            {
                database.ComboBoxToTable($"SELECT product_id, CONCAT(title,' - ', product_type.type_name, ' - ', description, ' - ', presence) AS product_info " +
                    $"FROM product, product_type " +
                    $"WHERE product.product_type_id = product_type.product_type_id " +
                    $"AND suitability = N'Пригоден' ", ProductComboBox);
            }
            else
            {
                database.ComboBoxToTable($"SELECT product_id, CONCAT(title,' - ', product_type.type_name, ' - ', description) AS product_info " +
                    $"FROM product, product_type " +
                    $"WHERE product.product_type_id = product_type.product_type_id " +
                    $"AND suitability ='' ", ProductComboBox);
            }

            string imagePath = "D:\\ДИПЛОМ\\warehouse-main\\Warehouse\\Resources\\logo.jpg";

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();

            imageControl.Source = bitmap;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxDTO dto = (ComboBoxDTO) ProductComboBox.SelectedItem;
            string quantity = ProductQuantity.Text;

            ValidationFileds validation = new ValidationFileds();

            if (validation.ValidationAddFromComboBoxOrder(quantity, dto))
            {
                if (!ComboBoxOrder.dicrtionaryWithName.ContainsKey(dto.name))
                { 
                    ComboBoxOrder.dicrtionaryWithId1.Add(dto.id, validation.CastQuantityToInt(quantity));
                    ComboBoxOrder.dicrtionaryWithName.Add(dto.name, validation.CastQuantityToInt(quantity));

                    addToComboBoxOrder(ComboBoxOrder.dicrtionaryWithName);

                    AddAmount();


                    this.Close();
                } else
                {
                    MessageBox.Show("Данный товар уже выбран!");
                }
            }
        }

        private void addToComboBoxOrder(Dictionary<string, int> dictionary)
        {
            List<ComboBoxOrder> combo = new List<ComboBoxOrder>();

            foreach (var item in dictionary)
            {
                combo.Add(new ComboBoxOrder { Title = item.Key, Quantity = item.Value });
            }

            grid.ItemsSource = combo;
        }

        private void AddAmount()
        {
            List<long> ids = new List<long>();

            foreach (DictionaryEntry item in ComboBoxOrder.dicrtionaryWithId1)
            {
                ids.Add((long)item.Key);
            }

            int i = 0;

            foreach (var item in grid.Items)
            {
                int quantity = ((ComboBoxOrder)item).Quantity;

                int cost = database.ReadProductCostById(ids[i]);
                double subtotal = quantity * cost;
                amount += subtotal;

                i++;
            }

            TextBox.Text = amount.ToString();
        }
    }
}
