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
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Click_Open(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.AddExtension = true;
            ofd.DefaultExt = "*.*";
            ofd.Filter = "Media(*.*)|*.*";
            ofd.ShowDialog();
            MyMediaPlayer.Source = new Uri(ofd.FileName);
            MyMediaPlayer.SpeedRatio = 1;
            MyMediaPlayer.Play();
            //Status.Text = MyMediaPlayer.Clock;
            //MyMediaPlayer.MediaOpened += new RoutedEventHandler(Click_Open);
        }

        private void Click_Play(object sender, RoutedEventArgs e)
        {
            MyMediaPlayer.SpeedRatio = 1;
            MyMediaPlayer.Play();
        }

        private void Click_Pause(object sender, RoutedEventArgs e)
        {
            MyMediaPlayer.Pause();
        }

        private void Click_Stop(object sender, RoutedEventArgs e)
        {
            MyMediaPlayer.Stop();
        }

        private void Slide_Volume(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MyMediaPlayer.Volume = (double)Volume.Value;
            Debug.Text = "Volume : " + (100 * MyMediaPlayer.SpeedRatio).ToString();
        }

        private void Click_SpeedLeft(object sender, RoutedEventArgs e)
        {
            MyMediaPlayer.SpeedRatio = -MyMediaPlayer.SpeedRatio * 2;
            Debug.Text = "Speed : x" + MyMediaPlayer.SpeedRatio.ToString();
        }

        private void Click_SpeedRight(object sender, RoutedEventArgs e)
        {
            MyMediaPlayer.SpeedRatio = MyMediaPlayer.SpeedRatio * 2;
            Debug.Text = "Speed : x" + MyMediaPlayer.SpeedRatio.ToString();
        }
    }
}
