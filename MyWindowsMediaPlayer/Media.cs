using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Win32;

using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;

namespace MyWindowsMediaPlayer
{
    class Media
    {
        private MediaOpenFile openFile = new MediaOpenFile();
        private bool _played = false;
        private bool _fullScreen = false;
        private bool _hide = false;

        public void InitAll(MediaElement mediaPlayer)
        {
            mediaPlayer.SpeedRatio = 1;
            _played = true;
        }

        public bool OpenFile(MediaElement mediaPlayer)
        {
            this.openFile.setPathFile();
            if (this.openFile.isPathFile())
            {
                mediaPlayer.Source = new Uri(this.openFile.getPathFile());
                this.InitAll(mediaPlayer);
                this._played = true;
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
                mediaPlayer.Pause();
            else
                mediaPlayer.Play();
            _played = !_played;
            return true;
        }

        public void Update_Time(MediaElement mediaPlayer)
        {
            mediaPlayer.Stop();
            mediaPlayer.Play();
        }

        public void Stop(MediaElement mediaPlayer)
        {
            this._played = false;
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
    }
}
