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
using static System.Net.WebRequestMethods;

namespace TorrentTrackerApp
{
    public partial class MainWindow : Window
    {
        ViewModel viewModel = new ViewModel();
        HttpDownloader httpDownloader;        
        int Idbutton;
        CurrentTorrentFile selectedCurrentFile;

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
            Button btn = sender as Button;
            //selectedCurrentFile = viewModel.Torrents.FirstOrDefault((CurrentTorrentFile)btn.DataContext);
            selectedCurrentFile = downloadList.SelectedItems[0] as CurrentTorrentFile;
            downloadList.SelectedItem = selectedCurrentFile;
            selectedCurrentFile
            //var obj2 = downloadList.GetValue(obj.Id);
            //viewModel.Torrents.ElementAt()
            //Torrents
            MessageBox.Show($"{selectedCurrentFile.Name}");
            //Беремо шлях робочого стола
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //Завантажуємо і вказуємо що вантажимо і куди
            httpDownloader = new HttpDownloader(enterURL.Text, System.IO.Path.Combine(path, System.IO.Path.GetFileName(enterURL.Text)));
            //змінюємо статус загрузки. У нашому випадку прогресбару            
            httpDownloader.ProgressChanged += HttpDownloader_ProgressChanged;
            //Підписуємося на подію, яка говорить, що скачування відбулося
            httpDownloader.DownloadCompleted += HttpDownloader_DownloadCompleted;

            httpDownloader.Start();

        }

        private void HttpDownloader_DownloadCompleted(object? sender, EventArgs e)
        {
            MessageBox.Show("Download Completed!");
        }

        private void HttpDownloader_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            //FileInfo fileInfo = new FileInfo(httpDownloader.FullFileName);

            //звертаємося до елементів списку по індексу, який зберіг раніше             

            //var torrent = (CurrentTorrentFile)downloadList.Items;
            var torrentFile = (HttpDownloader)sender;
            torrentFile.
            var torrent = (CurrentTorrentFile)viewModel.Torrents;
            //MessageBox.Show(sender.ToString());
            torrent.Size = Math.Round(httpDownloader.Info.Length / 1024d / 1024d, 2, MidpointRounding.AwayFromZero);
            torrent.DownloadProgress = e.Progress;
            torrent.Speed = $"{(e.SpeedInBytes / 1024d / 1024d).ToString("0.00")} MB/s";
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
