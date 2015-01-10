using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Win32;

namespace MyWindowsMediaPlayer
{
    [DataContract]
    public class Playlist
    {
        [DataMember]
        private bool Start = true;
        [DataMember]
        private List<String> Musics;
        [DataMember]
        private int CurrentMusic;

        public Playlist()
        {
            CurrentMusic = 0;
            Musics = new List<string>();
        }

        public void Save(string path)
 	    {
            var serializer = new DataContractSerializer(this.GetType());
 		    using(var stream = new FileStream(path, FileMode.Create))
 		    {
 			    serializer.WriteObject(stream, this);
 		    }
 	    }

        public static Playlist Load(string path)
        {
            var serializer = new DataContractSerializer(typeof(Playlist));
            if (File.Exists(path))
            {
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    return serializer.ReadObject(stream) as Playlist;
                }
            }
            return null;
        }

        public void addAMusic(String path)
        {
            if (File.Exists(path))
            {
                Musics.Add(path);
                this.setCurrentMusic(-1);
            }
        }

        public void delMusicFromPlaylist(int id)
        {
            if (CurrentMusic + 1 == Musics.Count())
                --CurrentMusic;
            if (id < Musics.Count())
                Musics.RemoveAt(id);
        }

        public void setCurrentMusic(int id)
        {
            if (id > -1)
                CurrentMusic = id;
            else
            {
                Start = true;
                CurrentMusic = 0;
            }
        }

        public int getCurrentMusic()
        {
            return (CurrentMusic);
        }

        public String getNextMusic()
        {
            if (CurrentMusic < Musics.Count() && CurrentMusic >= 0)
            {
                if (Start)
                    Start = false;
                else
                    ++CurrentMusic;
                return (Musics.ElementAt(CurrentMusic));
            }
            return null;
        }

        public List<String> getList()
        {
            return (Musics);
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
            {
                Musics.Add(path);
                this.setCurrentMusic(-1);
            }
        }

        public int findIndexOf(String str)
        {
            return int.Parse(str.Split('-').ElementAt(0)) - 1;
        }

        public int countLeft()
        {
            if (!Start)
                return 0;
            return (Musics.Count() - CurrentMusic);
        }
    }
}
