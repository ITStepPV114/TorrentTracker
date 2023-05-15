using AltoHttp;
using System;
using System.Collections.Generic;
using System.IO;
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
using Xceed.Wpf.AvalonDock.Controls;

namespace TorrentTrackerApp
{
    public partial class MainWindow : Window
    {
        ViewModel viewModel = new ViewModel();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext= viewModel;
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            if (downloadList.SelectedItem != null)
            {
                downloadList.Items.Remove(downloadList.SelectedItem);
            }
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            if (enterURL.Text != "")
            {
                CurrentTorrentFile file = new CurrentTorrentFile();
                var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                file.httpDownloader = new HttpDownloader(enterURL.Text, System.IO.Path.Combine(path, System.IO.Path.GetFileName(enterURL.Text)));
                file.httpDownloader.ProgressChanged += file.HttpDownloader_ProgressChanged;
                file.httpDownloader.DownloadCompleted+= file.HttpDownloader_DownloadCompleted;
                file.Name = enterURL.Text.Split(new char[] {'/'}).Last();
                downloadList.Items.Add(file);
            }
        }

        private void start_button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var context = (CurrentTorrentFile)button.DataContext;
            int rowIndex = downloadList.Items.IndexOf(context);
            var currentFile = (CurrentTorrentFile)downloadList.Items[rowIndex];
            currentFile.IsDownloading = true;
            currentFile.httpDownloader.Start();
        }

        private void pause_button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var context = (CurrentTorrentFile)button.DataContext;
            int rowIndex = downloadList.Items.IndexOf(context);
            var currentFile = (CurrentTorrentFile)downloadList.Items[rowIndex];
            currentFile.httpDownloader.Pause();
        }

        private void сontinue_button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var context = (CurrentTorrentFile)button.DataContext;
            int rowIndex = downloadList.Items.IndexOf(context);
            var currentFile = (CurrentTorrentFile)downloadList.Items[rowIndex];
            currentFile.httpDownloader.Resume();
        }
    }
}
