using AltoHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TorrentTrackerApp
{
    public partial class MainWindow : Window
    {
        ViewModel viewModel = new ViewModel();
        HttpDownloader httpDownloader;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext= viewModel;
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            if (enterURL.Text != "")
            {
                CurrentTorrentFile file = new CurrentTorrentFile();
                
                file.Name = enterURL.Text.Split(new char[] { '/' }).Last();
                //viewModel.Add(file);
                downloadList.Items.Add(file);
            }
        }

        private void start_button_Click(object sender, RoutedEventArgs e)
        {
            httpDownloader = new HttpDownloader(enterURL.Text, $@"C:\Users\dev\Desktop\{System.IO.Path.GetFileName(enterURL.Text)}");
            httpDownloader.Start();
        }

        private void pause_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void сontinue_button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
