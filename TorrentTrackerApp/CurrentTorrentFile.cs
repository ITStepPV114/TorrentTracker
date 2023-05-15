using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorrentTrackerApp
{
    [AddINotifyPropertyChangedInterface]
    public class CurrentTorrentFile
    {
        private int _id = 0;
        public int Id { get; set; }
        public string Name { get; set; }
        public double Size { get; set; }
        public string Speed { get; set; }
        public double DownloadProgress { get; set; }
        public bool IsDownloading { get; set; }
        public CurrentTorrentFile()
        {
            _id++;
            Id = _id;
        }
    }
    
}