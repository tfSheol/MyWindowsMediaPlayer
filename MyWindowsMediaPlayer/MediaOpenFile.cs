using System;
using Microsoft.Win32;

namespace MyWindowsMediaPlayer
{
    public class MediaOpenFile
    {
        private string _pathFile = "";

        private string openOneFile(string url)
        {
            if (url == null)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.AddExtension = true;
                ofd.DefaultExt = "*.*";
                ofd.Filter = "Media(*.*)|*.*";
                ofd.ShowDialog();
                return ofd.FileName;
            }
            else
                return (url);
        }

        public bool isPathFile()
        {
            if (_pathFile != "")
                return true;
            else
                return false;
        }

        public void setPathFile(string url = null)
        {
            _pathFile = openOneFile(url);
        }

        public string getPathFile()
        {
            return _pathFile;
        }
    }
}