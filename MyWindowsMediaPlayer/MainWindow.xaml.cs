using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;

namespace MyWindowsMediaPlayer
{
    /// <summary>
    /// MyWindowsMediaPlayer
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool played = false;
        private bool playlistReading = false;
        private Media media = new Media();
        private Playlist playlist = new Playlist();
        private SaveData saver = new SaveData();

        public MainWindow()
        {
            InitializeComponent();
        }

        public void initAll()
        {
            Volume.Value = 50;
            MyMediaPlayer.SpeedRatio = 1;
            MyMediaPlayer.Volume = 50;
            played = true;
            if (played)
                Play.Content = "Pause";
        }

        private void Click_Open(object sender, RoutedEventArgs args)
        {
            playlistReading = false;
            media.setPathFile();
            if (media.isPathFile())
            {
                setCurrentMedia(media.getPathFile());
                MyMediaPlayer.Play();
            }
        }

        private void Click_Play(object sender, RoutedEventArgs args)
        {
            MyMediaPlayer.SpeedRatio = 1;
            if (!played)
            {
                MyMediaPlayer.Play();
                Play.Content = "Pause";
            }
            else
            {
                MyMediaPlayer.Pause();
                Play.Content = "Play";
            }
            played = !played;
        }

        private void Tree_Over(object sender, RoutedEventArgs args)
        {
            tree.Opacity = 100;
            tree.Height = this.Height - 40 - bunttongrid.Height;
        }

        private void Tree_Leave(object sender, RoutedEventArgs args)
        {
            tree.Opacity = 0;
        }

        private void Opened_Media(object sender, RoutedEventArgs args)
        {
            playlistReading = false;
            if (MyMediaPlayer.NaturalDuration.HasTimeSpan)
                TimeMedia.Content = MyMediaPlayer.NaturalDuration.TimeSpan.Hours.ToString() + ":"
                             + MyMediaPlayer.NaturalDuration.TimeSpan.Minutes.ToString() + ":"
                             + MyMediaPlayer.NaturalDuration.TimeSpan.Seconds.ToString();
        }

        private void Update_Time(object sender, RoutedEventArgs args)
        {
            MyMediaPlayer.Stop();
            if (playlistReading)
            {
                if (playlist.countLeft() == 0)
                    playlist.setCurrentMusic(-1);
                setCurrentMedia(playlist.getNextMusic());
            }
            MyMediaPlayer.Play();
        }

        private void Click_Stop(object sender, RoutedEventArgs args)
        {
            Play.Content = "Play";
            played = false;
            MyMediaPlayer.Stop();
            MyMediaPlayer.Close();
        }

        private void Slide_Volume(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            MyMediaPlayer.Volume = (double)Volume.Value;
            Debug.Text = "Volume : " + MyMediaPlayer.SpeedRatio.ToString();
        }

        private void Click_SpeedLeft(object sender, RoutedEventArgs args)
        {
            //MyMediaPlayer.Position = MyMediaPlayer.Position - TimeSpan.FromSeconds(10);
            //MyMediaPlayer.SpeedRatio = MyMediaPlayer.SpeedRatio * 2;
            played = false;
            Debug.Text = "Speed : x" + MyMediaPlayer.SpeedRatio.ToString();
        }

        private void Click_SpeedRight(object sender, RoutedEventArgs args)
        {
            MyMediaPlayer.SpeedRatio = MyMediaPlayer.SpeedRatio * 2;
            played = false;
            Debug.Text = "Speed : x" + MyMediaPlayer.SpeedRatio.ToString();
        }

        private void Click_Muted(object sender, RoutedEventArgs args)
        {
            MyMediaPlayer.IsMuted = !MyMediaPlayer.IsMuted;
        }

        private void Checked_Debug(object sender, RoutedEventArgs args)
        {
            Menu_Debug.IsChecked = !Menu_Debug.IsChecked;
            if (Debug.Visibility == Visibility.Hidden)
                Debug.Visibility = Visibility.Visible;
            else
                Debug.Visibility = Visibility.Hidden;
        }

        private void Playlist_Add(object sender, RoutedEventArgs args)
        {
            playlistReading = true;
            playlist.addAMusic();
            ShowPlaylist(sender, args);
            if (playlist.countLeft() == 0)
                playlist.setCurrentMusic(-1);
            setCurrentMedia(playlist.getNextMusic());
        }

        private void ShowPlaylist(object sender, RoutedEventArgs args)
        {
            Playlist.ItemsSource = playlist.getList().ToArray();
        }

        private void Playlist_Read(object sender, RoutedEventArgs args)
        {
            playlistReading = true;
            playlist.setCurrentMusic(-1);
            setCurrentMedia(playlist.getNextMusic());
            MyMediaPlayer.Play();
            if (playlist.countLeft() == 0)
                playlist.setCurrentMusic(-1);
            setCurrentMedia(playlist.getNextMusic());
        }
        
        private void Playlist_Remove(object sender, RoutedEventArgs args)
        {
            if (-1 != playlist.findIndexOf(tree.SelectedItem.ToString()))
            {
                playlist.delMusicFromPlaylist(playlist.findIndexOf(tree.SelectedItem.ToString()));
                ShowPlaylist(sender, args);
            }
        }

        private void Playlist_Save(object sender, RoutedEventArgs args)
        {
            SaveData.Save(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\my_playlist.xml", this.playlist);
            if (playlist.countLeft() == 0)
                playlist.setCurrentMusic(-1);
            setCurrentMedia(playlist.getNextMusic());
        }

        private void Playlist_Load(object sender, RoutedEventArgs args)
        {
            playlistReading = true;
            playlist = SaveData.Load(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\my_playlist.xml");
            ShowPlaylist(sender, args);
            if (playlist.countLeft() == 0)
                playlist.setCurrentMusic(-1);
            setCurrentMedia(playlist.getNextMusic());
        }

        private void setCurrentMedia(String path)
        {
            if (path != null && File.Exists(path))
            {
                currentPlay.Text = path;
                media.setPathFile(path);
                MyMediaPlayer.Source = new Uri(path);
                initAll();
            }
        }
    }
}
