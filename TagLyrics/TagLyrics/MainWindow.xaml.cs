using System.Collections.ObjectModel;
using System;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using System.Windows.Input;
using System.Windows.Controls;

namespace TagLyrics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //콘솔로그 출력
       TextBoxOutputter outputter;
        ObservableCollection<Music> musics = new ObservableCollection<Music>();
        LyricsFinder lyricsFinder = new LyricsFinder();
        public MainWindow()
        {
            InitializeComponent();
            outputter = new TextBoxOutputter(LogBox);
           Console.SetOut(outputter);
            MusicData.DataContext = musics;
        }

        

        public void GetTags(string filepath)
        {
            try
            { 
                var tfile = TagLib.File.Create(filepath);
                musics.Add(new Music(Path.GetFileNameWithoutExtension(filepath),tfile.Tag.Title, tfile.Tag.Album, tfile.Tag.Lyrics, filepath));
                Console.WriteLine("Add"+ tfile.Tag.Title);
            }
            catch (Exception ex)
            {
                MessageBox.Show("A handled exception just occurred: " + ex.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        public void OpenFiles()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Music files (*.flac)|*.flac|All files (*.*)|*.*",
            };
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    Console.WriteLine("Select" + filename);
                    GetTags(filename);
                }
            }
        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFiles();
        }

        private void FindLyricButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var music in musics)
            {
                music.NewLyrics = lyricsFinder.SearchbyNaver("rwd1111",music.Title,music.Name,"가사번역");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var music in musics)
            {
                music.Lyrics = music.NewLyrics;
                music.NewLyrics = "";
                music.IsChanged = false;
                var tfile = TagLib.File.Create(music.Location);
                tfile.Tag.Lyrics = music.Lyrics;
                tfile.Save();
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            //미변경시 알림 추가하기
            musics.Clear();
        }
        private void Row_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var selectedMusic = (Music)MusicData.SelectedItem;
            LyricBox.Text = selectedMusic.Lyrics;
        }
    }
}
