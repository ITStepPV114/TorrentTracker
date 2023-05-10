﻿using PropertyChanged;
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
        public string Name { get; set; }
        public double Size { get; set; }
        public double Speed { get; set; }
        public double DownloadProgress { get; set; }
    }
}