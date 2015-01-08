﻿using System;
using Microsoft.Win32;

namespace MyWindowsMediaPlayer
{
    public class MediaOpenFile
    {
        private string _pathFile = "";

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

        public string getPathFile()
        {
            return _pathFile;
        }
    }
}