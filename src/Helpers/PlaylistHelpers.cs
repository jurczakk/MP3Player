using Microsoft.Win32;
using MP3Player.Interfaces.Helpers;
using MP3Player.Interfaces.Models;
using System.Collections.Generic;
using System.Linq;

namespace MP3Player.Helpers
{
    public class PlaylistHelpers : IPlaylistHelpers
    {
        private IPlaylist Playlist;
        public PlaylistHelpers() { }
        public PlaylistHelpers(IPlaylist playlist)
        {
            Playlist = playlist;
        }

        public void OpenFileDialog()
        {
            var fileDialog = new OpenFileDialog { Multiselect = true };
            if (fileDialog.ShowDialog() != null)
                foreach (var filename in fileDialog.FileNames.Where(x => x.ToLower().EndsWith(".mp3")))
                    Playlist.SongsList.Add(filename);
        }

        public bool CanDeleteOrClear()
        {
            if (Playlist?.SongsList == null)
                return false;
            return true;
        }

        public void DeleteFile()
        {
            Playlist.SongsList.Remove(Playlist.SelectedSong);
        }

        public void ClearPlaylist()
        {
            new List<string>(Playlist.SongsList).All(x => Playlist.SongsList.Remove(x));
        }
    }
}
