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
        HttpDownloader httpDownloader;
        int selectedIndex;
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
            var button = (Button)sender;
            var context = (CurrentTorrentFile)button.DataContext;
            int rowIndex = downloadList.Items.IndexOf(context);
            viewModel.Torrents.ElementAt(rowIndex).IsDownloading = true;
            
            

            //Беремо шлях робочого стола
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //Завантажуємо і вказуємо що вантажимо і куди
            httpDownloader = new HttpDownloader(enterURL.Text, System.IO.Path.Combine(path, System.IO.Path.GetFileName(enterURL.Text)));
            //змінюємо статус загрузки. У нашому випадку прогресбару            
            httpDownloader.ProgressChanged += HttpDownloader_ProgressChanged;
            //Підписуємося на подію, яка говорить, що скачування відбулося
            httpDownloader.DownloadCompleted += HttpDownloader_DownloadCompleted;

            //if (downloadList.SelectedItem != null)
            //{
                //selectedIndex = downloadList.SelectedIndex;
            httpDownloader.Start();
            //}

        }

        private void HttpDownloader_DownloadCompleted(object? sender, EventArgs e)
        {
            MessageBox.Show("Download Completed!");
        }

        private void HttpDownloader_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            //var torrentFiles = downloadList.Items;
            //foreach (ListViewItem torrentFile in torrentFiles)
            //{
            //    if(torrentFile.)
            //    if(torrentFile.AreAnyTouchesCapturedWithin)
            //    {
            //        var itemChildrens = torrentFile.FindLogicalChildren<ListViewItem>();
            //        foreach (var item in itemChildrens)
            //        {
            //            if(item.AreAnyTouchesCaptured)
            //            {
            //                CurrentTorrentFile currentFile = (CurrentTorrentFile)torrentFile;
            //                torrentFile.Size = Math.Round(httpDownloader.Info.Length / 1024d / 1024d, 2, MidpointRounding.AwayFromZero);
            //                torrentFile.DownloadProgress = e.Progress;
            //                torrentFile.Speed = $"{(e.SpeedInBytes / 1024d / 1024d).ToString("0.00")} MB/s";
            //            }
            //        }
            //    }
            //}


            //var sourceCollection = torrentFiles.SourceCollection;
            //sourceCollection;
            //ListViewItem litem = 

            //var torrentFiles = downloadList.Items;
            //foreach (CurrentTorrentFile torrentFile in torrentFiles)
            //{
            //    if(torrentFile.IsDownloading = true)
            //    {
            //        torrentFile.Size = Math.Round(httpDownloader.Info.Length / 1024d / 1024d, 2, MidpointRounding.AwayFromZero);
            //        torrentFile.DownloadProgress = e.Progress;
            //        torrentFile.Speed = $"{(e.SpeedInBytes / 1024d / 1024d).ToString("0.00")} MB/s";
            //    }
            //}

            //звертаємося до елементів списку по індексу, який зберіг раніше
            foreach (CurrentTorrentFile item in viewModel.Torrents)
            {
                if(item.IsDownloading)
                {
                    //var torrent = (CurrentTorrentFile)downloadList.Items[selectedIndex];
                    item.Size = Math.Round(httpDownloader.Info.Length / 1024d / 1024d, 2, MidpointRounding.AwayFromZero);
                    item.DownloadProgress = e.Progress;
                    item.Speed = $"{(e.SpeedInBytes / 1024d / 1024d).ToString("0.00")} MB/s";
                }
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
