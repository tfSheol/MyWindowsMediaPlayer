using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Threading;
using Microsoft.Win32;

using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;
using System.IO;

namespace MyWindowsMediaPlayer
{
    class Media
    {
        private MediaOpenFile openFile = new MediaOpenFile();
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private Playlist playlist = new Playlist();

        private bool playlistReading = false;
        private bool _played = false;
        private bool _fullScreen = false;
        private bool _hide = false;

        public Media(MediaElement mediaPlayer, Slider positionSlider)
        {
            this.dispatcherTimer.Tick += new EventHandler((sender, e) => Timer_Tick(sender, e, mediaPlayer, positionSlider));
            this.dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        }

        private void Timer_Tick(object sender, EventArgs e, MediaElement mediaPlayer, Slider slider)
        {
            slider.Value = mediaPlayer.Position.TotalSeconds;
        }

        public void InitAll(MediaElement mediaPlayer)
        {
            mediaPlayer.SpeedRatio = 1;
            this.dispatcherTimer.Start();
        }

        public bool OpenFile(MediaElement mediaPlayer)
        {
            this.openFile.setPathFile();
            if (this.openFile.isPathFile())
            {
                mediaPlayer.Source = new Uri(this.openFile.getPathFile());
                this.InitAll(mediaPlayer);
                this._played = true;
                this.dispatcherTimer.Start();
                mediaPlayer.Play();
                return true;
            }
            return false;
        }

        public bool PlayPause(MediaElement mediaPlayer) 
        {
            if (this.openFile.isPathFile() == false)
                return false;
            if (mediaPlayer.SpeedRatio != 1)
            {
                mediaPlayer.SpeedRatio = 1;
                return false;
            }
            if (this._played)
            {
                this.dispatcherTimer.Stop();
                mediaPlayer.Pause();
            }
            else
            {
                this.dispatcherTimer.Start();
                mediaPlayer.Play();
            }
            _played = !_played;
            return true;
        }

        public void Update_Time(MediaElement mediaPlayer)
        {
            this.dispatcherTimer.Stop();
            mediaPlayer.Stop();
            if (this.playlistReading)
            {
                if (playlist.countLeft() == 0)
                    playlist.setCurrentMusic(-1);
                setCurrentMedia(playlist.getNextMusic(), mediaPlayer);
            }
            this.dispatcherTimer.Start();
            mediaPlayer.Play();
        }

        public void Set_Reading_Playlist(bool reading)
        {
            this.playlistReading = reading;
        }

        public bool Get_Reading_Playlist()
        {
            return this.playlistReading;
        }

        public void Stop(MediaElement mediaPlayer)
        {
            this._played = false;
            this.dispatcherTimer.Stop();
            mediaPlayer.Stop();
            mediaPlayer.Close();
        }

        public void Sound(MediaElement mediaPlayer, Slider volume)
        {
            mediaPlayer.Volume = (double)volume.Value / 100;
        }

        public void SpeedRight(MediaElement mediaPlayer)
        {
            if (this._played == true)
            {
                mediaPlayer.SpeedRatio = mediaPlayer.SpeedRatio*2;
                _played = false;
            }
        }

        public void Mute(MediaElement mediaPlayer)
        {
            mediaPlayer.IsMuted = !mediaPlayer.IsMuted;
        }

        public bool Get_Played()
        {
            return this._played;
        }

        public bool Get_FullScreen()
        {
            return this._fullScreen;
        }

        public bool Get_Hide()
        {
            return this._hide;
        }

        public void FullScreen()
        {
            this._fullScreen = !this._fullScreen;
            this._hide = this._fullScreen;
        }

        public bool Hide_Click()
        {
            this._hide = !this._hide;
            return _hide;
        }

        public void Change_Time_Media(MediaElement mediaPlayer, Slider positionSlider)
        {
            mediaPlayer.Pause();
            mediaPlayer.Position = TimeSpan.FromSeconds(positionSlider.Value);
            if (this._played)
                mediaPlayer.Play();
        }

        /***************************************************/

        private bool setCurrentMedia(String path, MediaElement mediaPlayer)
        {
            if (path != null && File.Exists(path))
            {
                this.openFile.setPathFile(path);
                mediaPlayer.Source = new Uri(path);
                return true;
            }
            return false;
        }

        public void Add_PlayList(MediaElement mediaPlayer)
        {
            this.playlistReading = true;
            this.playlist.addAMusic();
            if (this.playlist.countLeft() == 0)
                this.playlist.setCurrentMusic(-1);
            this.setCurrentMedia(playlist.getNextMusic(), mediaPlayer);
        }

        public Playlist Get_PlayList() 
        {
            return this.playlist;
        }

        public bool Read_PlayList(MediaElement mediaPlayer)
        {
            if (this.playlist.getList().Count() == 0)
                return false;
            this.playlistReading = true;
            this.playlist.setCurrentMusic(-1);
            if (this.setCurrentMedia(this.playlist.getNextMusic(), mediaPlayer))
            {
                this.InitAll(mediaPlayer);
                this._played = true;
                this.dispatcherTimer.Start();
                mediaPlayer.Play();
                return true;
            }
            return false;
        }

        public bool Remove_PlaList(TreeView tree)
        {
            if (tree.SelectedItem != null && - 1 != this.playlist.findIndexOf(tree.SelectedItem.ToString()))
            {
                this.playlist.delMusicFromPlaylist(playlist.findIndexOf(tree.SelectedItem.ToString()));
                return true;
            }
            return false;
        }

        public void Save_PlayList(MediaElement mediaPlayer)
        {
            SaveData.Save(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\my_playlist.xml", this.playlist);
            if (playlist.countLeft() == 0)
                playlist.setCurrentMusic(-1);
            this.setCurrentMedia(playlist.getNextMusic(), mediaPlayer);

        }

        public void Load_PlayList(MediaElement mediaPlayer)
        {
            this.playlistReading = true;
            this.playlist = SaveData.Load(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\my_playlist.xml");
            if (this.playlist.countLeft() == 0)
                this.playlist.setCurrentMusic(-1);
            this.setCurrentMedia(playlist.getNextMusic(), mediaPlayer);
        }

        public String[] Get_Musics()
        {
            int i = -1;
            String[] list = null;
            /*
            using (MediaLibrary library = new MediaLibrary())
            {
                list = new String[library.Songs.Count];
                while (++i < library.Songs.Count)
                {
                    Console.WriteLine(library.Songs.ToArray()[i].Name);
                    list[i] = library.Songs.ToArray()[i].Name;
                }
            }*/
            return (list);
        }

        public String[] Get_Photos()
        {/*
            int i = -1;
            String[] list = new String[this.library.Songs.Count];

            while (++i < this.library.Pictures.Count)
            {
                list[i] = this.library.Pictures.ToArray()[i].Name;
            }
            return (list);*/
            return (null);
        }

        public String Next_Click(MediaElement mediaPlayer)
        {
            if (this.playlistReading)
            {
                if (this.playlist.countLeft() != 0)
                    this.playlist.setCurrentMusic(this.playlist.getCurrentMusic() + 1);
                else
                    this.playlist.setCurrentMusic(-1);
                if (this.setCurrentMedia(this.playlist.getList().ElementAt(this.playlist.getCurrentMusic()), mediaPlayer))
                {
                    this.InitAll(mediaPlayer);
                    this._played = true;
                    this.dispatcherTimer.Start();
                    mediaPlayer.Play();
                    return this.playlist.getList().ElementAt(this.playlist.getCurrentMusic());
                }
            }
            return null;
        }

        public String Prev_Click(MediaElement mediaPlayer)
        {
            if (this.playlistReading)
            {
                if (this.playlist.getCurrentMusic() != 0)
                    this.playlist.setCurrentMusic(this.playlist.getCurrentMusic() - 1);
                else
                    this.playlist.setCurrentMusic(this.playlist.getList().Count - 1);
                if (this.setCurrentMedia(this.playlist.getList().ElementAt(this.playlist.getCurrentMusic()), mediaPlayer))
                {
                    this.InitAll(mediaPlayer);
                    this._played = true;
                    this.dispatcherTimer.Start();
                    mediaPlayer.Play();
                    return this.playlist.getList().ElementAt(this.playlist.getCurrentMusic());
                }
            }
            return null;
        }
    }
}
