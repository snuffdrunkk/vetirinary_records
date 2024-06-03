using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Warehouse.Service;

namespace Warehouse.View.OutputDocuments
{

    public partial class DocumentsOutput : Window
    {

        private DataGrid dataGridFrzCheck;
        private DataGrid dataGridCarCheck;
        private DataGrid dataGridDriverCheck;

        OutputDocumentsService outputDocumentsService = new OutputDocumentsService();

        public DocumentsOutput(DataGrid dataGridFrzCheck, DataGrid dataGridCarCheck, DataGrid dataGridDriverCheck)
        {
            InitializeComponent();

            string imagePath = "D:\\ДИПЛОМ\\warehouse-main\\Warehouse\\Resources\\logo.jpg";
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();
            imageControl.Source = bitmap;

            this.dataGridFrzCheck = dataGridFrzCheck;
            this.dataGridCarCheck = dataGridCarCheck;
            this.dataGridDriverCheck = dataGridDriverCheck;
        }
        
        private void FreezerReport_Click(object sender, RoutedEventArgs e)
        {
  
        }

        private void CarReport_Click(object sender, RoutedEventArgs e)
        {
            CarOutputView carOutputView = new CarOutputView();
            carOutputView.ShowDialog();
        }
    }
}
