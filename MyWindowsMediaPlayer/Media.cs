using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace MyWindowsMediaPlayer
{
    class Media
    {
        private string _pathFile;

        private string openOneFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.AddExtension = true;
            ofd.DefaultExt = "*.*";
            ofd.Filter = "Media(*.*)|*.*";
            ofd.ShowDialog();
            return ofd.FileName;
        }

        public bool isPathFile()
        {
            if (_pathFile != "")
                return true;
            else
                return false;
        }

        public void setPathFile()
        {
            _pathFile = openOneFile();
        }

        public void setPathFile(String music)
        {
            _pathFile = music;
        }

        public string getPathFile()
        {
            return _pathFile;
        }
    }
}
