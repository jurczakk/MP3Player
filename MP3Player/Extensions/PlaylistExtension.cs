using Microsoft.Win32;
using MP3Player.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MP3Player.Extensions
{
    public static class PlaylistExtension
    {
        public static void OpenFileDialog(this IPlaylist playlist)
        {
            var fileDialog = new OpenFileDialog { Multiselect = true };
            if (fileDialog.ShowDialog() != null)
                foreach (var filename in fileDialog.FileNames.Where(x => x.ToLower().EndsWith(".mp3")))
                    playlist.SongsList.Add(filename);
        }

        public static bool CanDeleteOrClear(this IPlaylist playlist)
        {
            if (playlist?.SongsList == null)
                return false;
            return true;
        }

        public static void DeleteFile(this IPlaylist playlist)
        {
            playlist.SongsList.Remove(playlist.SelectedSong);
        }

        public static void ClearPlaylist(this IPlaylist playlist)
        {
            new List<string>(playlist.SongsList).All(x => playlist.SongsList.Remove(x));
            //playlist.SongsList.ToList().All(x => Playlist.SongsList.Remove(x));
        }
    }
}
