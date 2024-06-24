using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Warehouse.View.OutputDocuments
{

    public partial class DocumentsOutput : Window
    {
        public DocumentsOutput()
        {
            InitializeComponent();

            string imagePath = "D:\\ДИПЛОМ\\warehouse-main\\Warehouse\\Resources\\logo.jpg";
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();
            imageControl.Source = bitmap;
        }
        
        private void FreezerReport_Click(object sender, RoutedEventArgs e)
        {
            FreezerOutputView freezerOutputView = new FreezerOutputView();
            freezerOutputView.ShowDialog();
        }

        private void CarReport_Click(object sender, RoutedEventArgs e)
        {
            CarOutputView carOutputView = new CarOutputView();
            carOutputView.ShowDialog();
        }

        private void DriverReport_Click(object sender, RoutedEventArgs e)
        {
            DriverOutputView driverOutputView = new DriverOutputView();
            driverOutputView.ShowDialog();
        }

        private void StorkeeperReport_Click(object sender, RoutedEventArgs e)
        {
            StorekeeperOutputView storekeeperOutputView = new StorekeeperOutputView();
            storekeeperOutputView.ShowDialog();
        }

        private void MedicalSertificate_Click(object sender, RoutedEventArgs e)
        {
            WaybillOutputView waybillOutputView = new WaybillOutputView();
            waybillOutputView.generateCarWordDocument();
        }
    }
}
