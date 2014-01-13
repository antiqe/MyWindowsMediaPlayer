using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyWindowsMediaPlayer
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static int currentState = -1;

        public MainWindow()
        {
            InitializeComponent();
            MediaPlayer.Volume = 0.0;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void buttonOpen_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff|"
                + "All Medias Types|*.avi;*.mp3;*.mkv;*.mp4;";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                MediaPlayer.Source = new Uri(dlg.FileName);
                MediaPlayer.LoadedBehavior = MediaState.Manual;
                MediaPlayer.Height = 452;
                MediaPlayer.SpeedRatio = 1.0;
                MediaPlayer.Play();
                gridLibrary.Visibility = Visibility.Hidden;
                barLibrary.Visibility = Visibility.Hidden;
                treeLibrary.Visibility = Visibility.Hidden;
                MediaPlayer.Visibility = Visibility.Visible;
                buttonUnfoldPlaylist.Visibility = Visibility.Visible;
                currentState = 0;
            }
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
           WindowMedia.Close();
        }

        private void buttonReduce_Click(object sender, RoutedEventArgs e)
        {
            WindowMedia.WindowState = WindowState.Minimized;
        }

        private void buttonLibrary_Click(object sender, RoutedEventArgs e)
        {
            gridLibrary.Visibility = Visibility.Visible;
            MediaPlayer.Visibility = Visibility.Hidden;
            barLibrary.Visibility = Visibility.Visible;
            treeLibrary.Visibility = Visibility.Visible;
            buttonUnfoldPlaylist.Visibility = Visibility.Hidden;
            bgPlaylist.Visibility = Visibility.Hidden;
            buttonFoldPlaylist.Visibility = Visibility.Hidden;
            dataPlaylist.Visibility = Visibility.Hidden;

        }

        private void buttonUnfoldPlaylist_Click(object sender, RoutedEventArgs e)
        {
            bgPlaylist.Visibility = Visibility.Visible;
            buttonFoldPlaylist.Visibility = Visibility.Visible;
            dataPlaylist.Visibility = Visibility.Visible;
        }

        private void buttonFoldPlaylist_Click(object sender, RoutedEventArgs e)
        {
            bgPlaylist.Visibility = Visibility.Hidden;
            buttonFoldPlaylist.Visibility = Visibility.Hidden;
            dataPlaylist.Visibility = Visibility.Hidden;
        }

        private void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            String name = ((TreeViewItem)((TreeView)sender).SelectedItem).Name;
            switch (name)
            {
                case "itemPlaylist":
                break;
            }
        }

        private void buttonPlayPause_Click(object sender, RoutedEventArgs e)
        {
            if (MediaPlayer.SpeedRatio != 1.0)
                MediaPlayer.SpeedRatio = 1.0;
            else if (currentState == 0)
            {
                MediaPlayer.Pause();
                currentState = 1;
            }
            else if (currentState == 1)
            {
                MediaPlayer.Play();
                currentState = 0;
                MediaPlayer.SpeedRatio = 1.0;
            }
        }

        private void buttonStop_Click(object sender, RoutedEventArgs e)
        {
            if (currentState >= 0)
            {
                MediaPlayer.Stop();
                MediaPlayer.Visibility = Visibility.Hidden;
                currentState = -1;
            }
        }

        private void buttonRewind_Click(object sender, RoutedEventArgs e)
        {
            if (currentState == 0)
                MediaPlayer.SpeedRatio /= 2.0;
        }

        private void buttonFastForward_Click(object sender, RoutedEventArgs e)
        {
            if (currentState == 0)
                MediaPlayer.SpeedRatio *= 2.0;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MediaPlayer.Volume = e.NewValue;
        }
    }
}
