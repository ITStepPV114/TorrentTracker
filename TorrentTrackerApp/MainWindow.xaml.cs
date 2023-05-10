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
                
                file.Name = enterURL.Text.Split(new char[] {'/'}).Last();
                
                downloadList.Items.Add(file);
            }
        }

        private void start_button_Click(object sender, RoutedEventArgs e)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            httpDownloader = new HttpDownloader(enterURL.Text, System.IO.Path.Combine(path, System.IO.Path.GetFileName(enterURL.Text)));
            httpDownloader.ProgressChanged += HttpDownloader_ProgressChanged;
            httpDownloader.DownloadCompleted += HttpDownloader_DownloadCompleted;
            httpDownloader.Start();
        }

        private void HttpDownloader_DownloadCompleted(object? sender, EventArgs e)
        {
            MessageBox.Show("Download Completed!");
        }

        private void HttpDownloader_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            FileInfo fileInfo = new FileInfo(httpDownloader.FullFileName);
            if(downloadList.SelectedItem!=null)
            {
                var torrentFile = viewModel.Torrents.ElementAt(downloadList.SelectedIndex);
                torrentFile.Size = fileInfo.Length / 1024d / 1024d;
                torrentFile.DownloadProgress = e.Progress;
                torrentFile.Speed = $"{(e.SpeedInBytes / 1024d / 1024d).ToString("0.00")} MB/s";
            }
        }

        private void pause_button_Click(object sender, RoutedEventArgs e)
        {
            httpDownloader.Pause();
        }

        private void сontinue_button_Click(object sender, RoutedEventArgs e)
        {
            httpDownloader.Resume();
        }
    }
}
