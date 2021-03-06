﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.IO;
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
        private Media media;
        private StackPanel play = new StackPanel();
        private StackPanel pause = new StackPanel();

        public MainWindow()
        {
            InitializeComponent();
            media = new Media(MyMediaPlayer, PositionSlider);
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

        private void Click_Exit(object sender, RoutedEventArgs args)
        {
            MyMediaPlayer.Close();
            MyWindow.Close();
        }

        private void Click_OpenLibrary(object sender, RoutedEventArgs args)
        {
            Library lib = new Library(MyMediaPlayer, media);
            lib.Show();
        }

        private void Click_Open(object sender, RoutedEventArgs args)
        {
            this.media.Set_Reading_Playlist(false);
            if (this.media.OpenFile(MyMediaPlayer) == true)
            {
                Play.Content = pause;
                PlayMenuItem.Header = "Pause";
            }
        }

        private void Click_Play_Pause(object sender, RoutedEventArgs args)
        {
            if (this.media.PlayPause(MyMediaPlayer) != false)
            {
                if (this.media.Get_Played() == true)
                {
                    Play.Content = this.pause;
                    PlayMenuItem.Header = "Pause";
                }
                else
                {
                    Play.Content = this.play;
                    PlayMenuItem.Header = "Play";
                }
            }
        }

        private void Opened_Media(object sender, RoutedEventArgs args)
        {
            if (MyMediaPlayer.NaturalDuration.HasTimeSpan)
            {
                TimeMedia.Content = MyMediaPlayer.NaturalDuration.TimeSpan.Hours.ToString() + ":"
                                    + MyMediaPlayer.NaturalDuration.TimeSpan.Minutes.ToString() + ":"
                                    + MyMediaPlayer.NaturalDuration.TimeSpan.Seconds.ToString();
                PositionSlider.Maximum = MyMediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                MyMediaPlayer.Width = (MyMediaPlayer.Height*MyMediaPlayer.NaturalVideoWidth)/
                                            MyMediaPlayer.NaturalVideoHeight;
                MyMediaPlayer.Height = (MyMediaPlayer.Width * MyMediaPlayer.NaturalVideoHeight) /
                                            MyMediaPlayer.NaturalVideoWidth;
                if (MyMediaPlayer.Width > 535 && MyMediaPlayer.Height > 95)
                {
                    MyWindow.Width = MyMediaPlayer.Width + 16;
                    MyWindow.Height = MyMediaPlayer.Height + 50;
                }
            }
        }

        private void Update_Time(object sender, RoutedEventArgs args)
        {
            PositionSlider.Value = 0;
            Play.Content = this.pause;
            PlayMenuItem.Header = "Pause";
            this.media.Update_Time(MyMediaPlayer);
            if (this.media.Get_Reading_Playlist())
            {
                FileInfo info = new FileInfo(this.media.Get_PlayList().getList().ElementAt(this.media.Get_PlayList().getCurrentMusic()));
                CurrentPlay.Content = info.Name;
            }
        }

        private void Click_Stop(object sender, RoutedEventArgs args)
        {
            Play.Content = this.play;
            PlayMenuItem.Header = "Play";
            this.media.Stop(MyMediaPlayer);
            PositionSlider.Value = 0;
        }

        private void Slide_Volume(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            this.media.Sound(MyMediaPlayer, Volume);
            Debug.Text = "Volume : " + MyMediaPlayer.SpeedRatio.ToString();
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
            if (!(MenuGrid.IsMouseOver || ButtonGrid.IsMouseOver || tree.IsMouseOver))
                this.Full_Screen(null, null);
        }

        private void resize_Media_Player(bool fullscreen)
        {
            bool _height;

            _height = false;
            if (MyMediaPlayer.NaturalVideoWidth*MyWindow.Height - MyMediaPlayer.NaturalVideoHeight*MyWindow.Width > 0)
                _height = true;
            if (fullscreen == true)
            {
                if (!_height)
                {
                    MyMediaPlayer.Height = MyWindow.Height;
                    MyMediaPlayer.Width = (MyMediaPlayer.Height*MyMediaPlayer.NaturalVideoWidth)/
                                            MyMediaPlayer.NaturalVideoHeight;
                }
                else
                {
                    MyMediaPlayer.Width = MyWindow.Width;
                    MyMediaPlayer.Height = (MyMediaPlayer.Width*MyMediaPlayer.NaturalVideoHeight)/
                                            MyMediaPlayer.NaturalVideoWidth;
                }
                tree.Height = MyWindow.Height;
                if (MyMediaPlayer.NaturalVideoWidth != 0 || MyMediaPlayer.NaturalVideoHeight != 0)
                {
                    MyMediaPlayer.Margin = new Thickness(0, (MyWindow.Height - MyMediaPlayer.Height)/2, 0, 0);
                    tree.Margin = new Thickness(MyWindow.Width - 157, 20, 0, 0);
                }
                else
                {
                    MyMediaPlayer.Margin = new Thickness(0, 0, 0, 0);
                    tree.Margin = new Thickness(MyWindow.Width - 157, 0, 0, 0);
                }
            }
            else
            {
            _height = false;
                if (MyMediaPlayer.NaturalVideoWidth * (MyWindow.Height - 50) - MyMediaPlayer.NaturalVideoHeight * (MyWindow.Width - 16) > 0)
                    _height = true;
                if (!_height)
                {
                    MyMediaPlayer.Height = MyWindow.Height;
                    MyMediaPlayer.Width = (((MyMediaPlayer.Height - 50) * MyMediaPlayer.NaturalVideoWidth) /
                                            MyMediaPlayer.NaturalVideoHeight);
                    MyMediaPlayer.Margin = new Thickness(0, 0, 0, 0);
                }
                else
                {
                    MyMediaPlayer.Width = MyWindow.Width - 16;
                    MyMediaPlayer.Height = ((MyMediaPlayer.Width - 4) * MyMediaPlayer.NaturalVideoHeight) /
                                            MyMediaPlayer.NaturalVideoWidth;

                    MyMediaPlayer.Margin = new Thickness(0, ((MyWindow.Height - MyMediaPlayer.Height) / 2) - 20, 0, 0);
                }
            }
            if (!this.media.Get_Hide())
                tree.Margin = new Thickness(MyWindow.Width - 157, 20, 0, 0);
            else
                tree.Margin = new Thickness(MyWindow.Width - 157, 0, 0, 0);
            Debug.Text += _height.ToString();
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

        private void PositionSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.media.Change_Time_Media(MyMediaPlayer, PositionSlider);
            TimeMedia.Content = MyMediaPlayer.Position;
        }

        private void Tree_Over(object sender, RoutedEventArgs args)
        {
            this.ShowPlaylist(sender, args);
            tree.Opacity = 100;
            if (this.Height - 60 - ButtonGrid.Height >= 0)
                tree.Height = this.Height - 60 - ButtonGrid.Height;
            else
                tree.Height = 0;
        }

        private void Tree_Leave(object sender, RoutedEventArgs args)
        {
            tree.Opacity = 0;
        }

        private void Playlist_Add(object sender, RoutedEventArgs args)
        {
            this.media.Add_PlayList(MyMediaPlayer);
            this.ShowPlaylist(sender, args);
        }

        private void ShowPlaylist(object sender, RoutedEventArgs args)
        {
            String[] source;
            String[] rawData;
            String[] tmp;
            int i = -1;

            rawData = this.media.Get_PlayList().getList().ToArray();
            source = new String[rawData.Count()];
            while (++i < rawData.Count())
            {
                tmp = rawData[i].Split('\\');
                source[i] = "" + (i + 1) + " - " + tmp.ElementAt(tmp.Count() - 1);
            }
            Playlist.ItemsSource = source;
        }

        private void Playlist_Read(object sender, RoutedEventArgs args)
        {
            if (this.media.Get_PlayList().getList().Count() != 0)
            {
                FileInfo info = new FileInfo(this.media.Get_PlayList().getList().ElementAt(this.media.Get_PlayList().getCurrentMusic()));
                CurrentPlay.Content = info.Name;
            }
            else
            {
                Playlist_Add(sender, args);
                if (this.media.Get_PlayList().getList().Count() != 0)
                {
                    FileInfo info = new FileInfo(this.media.Get_PlayList().getList().ElementAt(this.media.Get_PlayList().getCurrentMusic()));
                    CurrentPlay.Content = info.Name;
                }
            }
            if (this.media.Read_PlayList(MyMediaPlayer))
            {
                if (this.media.Get_Played() == true)
                {
                    Play.Content = this.pause;
                    PlayMenuItem.Header = "Pause";
                }
                else
                {
                    Play.Content = this.play;
                    PlayMenuItem.Header = "Play";
                }
            }
        }

        private void Playlist_Remove(object sender, RoutedEventArgs args)
        {
            if (this.media.Remove_PlaList(tree))
                this.ShowPlaylist(sender, args);
        }

        private void Playlist_Save(object sender, RoutedEventArgs args)
        {
            this.media.Save_PlayList(MyMediaPlayer);
        }

        private void Playlist_Load(object sender, RoutedEventArgs args)
        {
            this.media.Load_PlayList(MyMediaPlayer);
            this.ShowPlaylist(sender, args);
        }

        private void Playlist_Up(object sender, RoutedEventArgs args)
        {
            if (this.media.Up_PlaList(tree))
                this.ShowPlaylist(sender, args);
        }

        private void Playlist_Down(object sender, RoutedEventArgs args)
        {
            if (this.media.Down_PlaList(tree))
                this.ShowPlaylist(sender, args);
        }

        private void Click_Prev(object sender, RoutedEventArgs args)
        {
            String name;

            name = this.media.Prev_Click(this.MyMediaPlayer);
            if (name != null)
            {
                FileInfo info = new FileInfo(this.media.Get_PlayList().getList().ElementAt(this.media.Get_PlayList().getCurrentMusic()));
                CurrentPlay.Content = info.Name;
            }
        }

        private void Click_Next(object sender, RoutedEventArgs args)
        {
            String name;

            name = this.media.Next_Click(this.MyMediaPlayer);
            if (name != null)
                this.CurrentPlay.Content = name;
        }

        private void treeView_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
                foreach (string fileLoc in filePaths)
                    if (File.Exists(fileLoc))
                        this.media.Get_PlayList().addAMusic(fileLoc);
            }
            this.ShowPlaylist(sender, e);
        }

        private void HideAll_Checked(object sender, RoutedEventArgs e)
        {

        }


    }
}
