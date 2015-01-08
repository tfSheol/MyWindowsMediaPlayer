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
        private bool played = false;
        private Media media = new Media();
        private StackPanel play = new StackPanel();
        private StackPanel pause = new StackPanel();

        public MainWindow()
        {
            InitializeComponent();
            Volume.Value = 50;
            Image _play = new Image();
            Image _pause = new Image();

            _play.Source = new BitmapImage(new Uri("./Ressource/play.jpg", UriKind.Relative));
            _pause.Source = new BitmapImage(new Uri("./Ressource/pause.png", UriKind.Relative));
            this.play.Orientation = Orientation.Horizontal;
            this.play.Children.Add(_play);
            this.pause.Orientation = Orientation.Horizontal;
            this.pause.Children.Add(_pause);
            this.resize_Media_Player(false);
        }

        private void Click_Open(object sender, RoutedEventArgs args)
        {
            if (this.media.OpenFile(MyMediaPlayer) == true)
                Play.Content = pause;
        }

        private void Click_Play_Pause(object sender, RoutedEventArgs args)
        {
            if (this.media.PlayPause(MyMediaPlayer) != false)
            {
                if (this.media.Get_Played() == true)
                    Play.Content = this.pause;
                else
                    Play.Content = this.play;
            }
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
            this.media.Update_Time(MyMediaPlayer);
        }

        private void Click_Stop(object sender, RoutedEventArgs args)
        {
            Play.Content = this.play;
            this.media.Stop(MyMediaPlayer);
        }

        private void Slide_Volume(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            this.media.Sound(MyMediaPlayer, Volume);
            Debug.Text = "Volume : " + MyMediaPlayer.SpeedRatio.ToString();
        }

        private void Click_SpeedLeft(object sender, RoutedEventArgs args)
        {
//            MyMediaPlayer.Position = MyMediaPlayer.Position - TimeSpan.FromSeconds(10);
//            MyMediaPlayer.SpeedRatio = MyMediaPlayer.SpeedRatio * 2;
            played = false;
            Debug.Text = "Speed : x" + MyMediaPlayer.SpeedRatio.ToString();
        }

        private void Click_SpeedRight(object sender, RoutedEventArgs args)
        {
            this.media.SpeedRight(MyMediaPlayer);
            Debug.Text = "Speed : x" + MyMediaPlayer.SpeedRatio.ToString();
        }

        private void Mute_CheckBox_Click(object sender, RoutedEventArgs e)
        {
            this.media.Mute(MyMediaPlayer);
        }

        private void Checked_Debug(object sender, RoutedEventArgs args)
        {
            MenuDebug.IsChecked = !MenuDebug.IsChecked;
            if (Debug.Visibility == Visibility.Hidden)
                Debug.Visibility = Visibility.Visible;
            else
                Debug.Visibility = Visibility.Hidden;
        }

        private void MenuGrid_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (this.media.Get_Hide() == true)
                MenuGrid.Opacity = 100;
        }

        private void MenuGrid_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (this.media.Get_Hide() == true)
                MenuGrid.Opacity = 0;
        }

        private void ButtonGrid_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (this.media.Get_Hide() == true)
                ButtonGrid.Opacity = 100;
        }

        private void ButtonGrid_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (this.media.Get_Hide() == true)
                ButtonGrid.Opacity = 0;
        }

        private void Full_Screen(object sender, RoutedEventArgs e)
        {
            if (this.media.Get_FullScreen() == false)
            {
                this.WindowStyle = WindowStyle.None;
                this.WindowState = WindowState.Maximized;
                MenuGrid.Opacity = 0;
                ButtonGrid.Opacity = 0;
                HideAll.IsChecked = true;
            }
            else
            {
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.WindowState = WindowState.Normal;
                MenuGrid.Opacity = 100;
                ButtonGrid.Opacity = 100;
                HideAll.IsChecked = false;
            }
            media.FullScreen();
            this.resize_Media_Player(this.media.Get_FullScreen());
        }

        private void MainWindow_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Full_Screen(null, null);
        }

        private void resize_Media_Player(bool fullscreen)
        {
            if (fullscreen == true)
            {
                MyMediaPlayer.Width = MyWindow.Width;
                MyMediaPlayer.Height = MyWindow.Height;
            }
            else
            {
                MyMediaPlayer.Height = MyWindow.Height - 40;
                if (this.media.Get_Hide() == true)
                {
                    MyMediaPlayer.Margin = new Thickness(0, 0, 0, 0);
                }
                else
                {
                    MyMediaPlayer.Margin = new Thickness(0, 20, 0, 0);
                    MyMediaPlayer.VerticalAlignment = VerticalAlignment.Top;
                }
                MyMediaPlayer.Width = MyWindow.Width - 15;
            }
        }

        private void HideAll_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.media.Hide_Click() == true)
            {
                MenuGrid.Opacity = 0;
                ButtonGrid.Opacity = 0;
            }
            else
            {
                MenuGrid.Opacity = 100;
                ButtonGrid.Opacity = 100;
            }
            this.resize_Media_Player(this.media.Get_FullScreen());
        }

        private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.resize_Media_Player(false);
        }
    }
}
