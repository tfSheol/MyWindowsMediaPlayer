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

        private void loadAllData()
        {
            try
            {
                string[] mediaFiles = Directory.GetFiles(@Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Music", "*.mp3", SearchOption.AllDirectories);
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
            return _itemMediaLib[index];
        }

        private void Select_Element(object sender, SelectionChangedEventArgs args)
        { 
            _libMedia.OpenFile(_libMediaPlayer, getItemPath(libList.SelectedIndex));
        }
    }
}
