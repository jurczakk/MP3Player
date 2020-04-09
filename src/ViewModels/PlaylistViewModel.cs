using MP3Player.Commands;
using MP3Player.Interfaces;
using System.Windows.Input;
using Microsoft.Win32;
using System.Linq;
using System;
using System.Windows;

namespace MP3Player.ViewModels
{
    public class PlaylistViewModel : IPlaylistViewModel
    {
        public IPlaylist Playlist { get; set; }
        public ICommand GetSongs { get; set; }
        public ICommand DeleteSong { get; set; }
        public ICommand Clear { get; set; }

        public PlaylistViewModel() { }
        public PlaylistViewModel(IPlaylist playlist)
        {
            Playlist = playlist;

            GetSongs =
                new MainCommand(
                    x => true,
                    x => OpenFileDialog());

            DeleteSong =
                new MainCommand(
                    x => CanClearOrDelete(),
                    x => Delete());

            Clear =
                new MainCommand(
                    x => CanClearOrDelete(),
                    x => Playlist.SongsList.ToList().All(x => Playlist.SongsList.Remove(x)));
        }

        private bool CanClearOrDelete()
        {
            if (Playlist?.SongsList == null)
                return false;
            return true;
        }

        private void Delete()
        {
            Playlist.SongsList.Remove(Playlist.SelectedSong);
            // Reset Ids
            //Playlist.SongsList.
        }

        private void OpenFileDialog()
        {
            var fileDialog = new OpenFileDialog { Multiselect = true };
            if (fileDialog.ShowDialog() != null)
                foreach (var filename in fileDialog.FileNames.Where(x => x.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase)))
                    Playlist.SongsList.Add(new Tuple<int, string>(Playlist.SongsList.Count + 1, filename));
            MessageBox.Show(Playlist.SongsList.Count.ToString());
        }
    }
}