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

namespace MyWindowsMediaPlayer
{
    /// <summary>
    /// MyWindowsMediaPlayer
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool played = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void InitAll()
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
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.AddExtension = true;
            ofd.DefaultExt = "*.*";
            ofd.Filter = "Media(*.*)|*.*";
            ofd.ShowDialog();
            if (ofd.FileName != "")
            {
                MyMediaPlayer.Source = new Uri(ofd.FileName);
                InitAll();
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

        private void Opened_Media(object sender, RoutedEventArgs args)
        {
            if (MyMediaPlayer.NaturalDuration.HasTimeSpan)
                TimeMedia.Content = MyMediaPlayer.NaturalDuration.TimeSpan.Hours.ToString() + ":"
                             + MyMediaPlayer.NaturalDuration.TimeSpan.Minutes.ToString() + ":"
                             + MyMediaPlayer.NaturalDuration.TimeSpan.Seconds.ToString();
        }

        private void Update_Time(object sender, RoutedEventArgs args)
        {
            MyMediaPlayer.Stop();
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
    }
}
