using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Win32;

namespace MyWindowsMediaPlayer
{
    public class Playlist
    {
        private bool _start = true;
        private List<String> _musics;
        private int _currentMusic;

        public Playlist()
        {
            _currentMusic = 0;
            _musics = new List<string>();
        }

        public void addAMusic(String path)
        {
            if (File.Exists(path))
                _musics.Add(path);
        }

        public void delMusicFromPlaylist(int id)
        {
            if (_currentMusic + 1 == _musics.Count())
                --_currentMusic;
            if (id < _musics.Count())
                _musics.RemoveAt(id);
        }

        public void setCurrentMusic(int id)
        {
            if (id > -1)
                _currentMusic = id;
            else
            {
                _start = true;
                _currentMusic = 0;
            }
        }

        public int getCurrentMusic()
        {
            return (_currentMusic);
        }

        public String getNextMusic()
        {
            if (_currentMusic + 1 < _musics.Count() && _currentMusic + 1 >= 0)
            {
                if (_start)
                    _start = false;
                else
                    ++_currentMusic;
                return (_musics.ElementAt(_currentMusic));
            }
            return null;
        }

        public List<String> getList()
        {
            return (_musics);
        }

        private string openOneFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.AddExtension = true;
            ofd.DefaultExt = "*.*";
            ofd.Filter = "Media(*.*)|*.*";
            ofd.ShowDialog();
            return ofd.FileName;
        }

        public void addAMusic()
        {
            String path;

            path = openOneFile();
            if (path != null && File.Exists(path))
                _musics.Add(path);
        }

        public int findIndexOf(String str)
        {
            int i = -1;

            while (_musics.Count() > ++i)
            {
                if (str.Equals(_musics.ElementAt(i)))
                    return (i);
            }
            return (-1);
        }

        public int countLeft()
        {
            return (_musics.Count() - (_currentMusic + 1));
        }
    }
}
