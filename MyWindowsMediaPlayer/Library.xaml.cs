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
using System.Windows.Shapes;
using System.IO;

class MyMedia
{
    public string setTitle {get; set;}
    public string setArtist {get; set;}
    public string setAlbum {get; set;}
}

namespace MyWindowsMediaPlayer
{
    /// <summary>
    /// Logique d'interaction pour Library.xaml
    /// </summary>
    public partial class Library : Window
    {
        private string[] _itemMediaLib;
        private Media _libMedia;
        private MediaElement _libMediaPlayer;

        public Library(MediaElement mediaPlayer, Media medial)
        {
            InitializeComponent();
            _libMedia = medial;
            _libMediaPlayer = mediaPlayer;
            loadAllData();
        }

        private void loadAllData(string option = "Music")
        {
            try
            {
                string[] mediaFiles = Directory.GetFiles(@Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\" + option, "*.*", SearchOption.AllDirectories);
                _itemMediaLib = mediaFiles;
                foreach (string name in mediaFiles)
                {
                    FileInfo info = new FileInfo(name);
                    libList.Items.Add(new MyMedia() { setTitle = info.Name, setArtist = "", setAlbum = "" });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        private string getItemPath(int index)
        {
            try
            {
                return _itemMediaLib[index];
            }
            catch (Exception e)
            {
                return "";
            }
        }

        private void Select_Element(object sender, SelectionChangedEventArgs args)
        { 
            _libMedia.OpenFile(_libMediaPlayer, getItemPath(libList.SelectedIndex));
        }

        private void Click_Musics(object sender, RoutedEventArgs args)
        {
            libList.Items.Clear();
            _itemMediaLib = null;
            loadAllData("Music");
        }

        private void Click_Movies(object sender, RoutedEventArgs args)
        {
            libList.Items.Clear();
            _itemMediaLib = null;
            loadAllData("Videos");
        }

        private void Click_Pictures(object sender, RoutedEventArgs args)
        {
            libList.Items.Clear();
            _itemMediaLib = null;
            loadAllData("Pictures");
        }
    }
}
