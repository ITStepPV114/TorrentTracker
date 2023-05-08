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
}
