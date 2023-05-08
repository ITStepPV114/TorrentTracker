using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorrentTrackerApp
{
    [AddINotifyPropertyChangedInterface]
    public class ViewModel
    {
        ObservableCollection<CurrentTorrentFile> torrents;
        public ViewModel() 
        {
            torrents= new ObservableCollection<CurrentTorrentFile>();
        }
        public void Add(CurrentTorrentFile file)
        {
            torrents.Add(file);
        }
    }
    [AddINotifyPropertyChangedInterface]
    public class CurrentTorrentFile
    {
        public string Name { get; set; }
        public double Size { get; set; }
        public double Speed { get; set; }
        public double DownloadProgress { get; set; }
    }
}
