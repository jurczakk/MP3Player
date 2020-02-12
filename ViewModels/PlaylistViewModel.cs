using MP3Player.Commands;
using MP3Player.Models;
using System.Windows.Input;
using System.Linq;
using System.Windows.Forms;

namespace MP3Player.ViewModels
{
    public class PlaylistViewModel
    {
        public Playlist Playlist { get; }

        public PlaylistViewModel()
        {
            Playlist = new Playlist();
            GetSongsPaths = new MainCommand(x => true, x => OpenFileDialog());
            DeleteSongFromPlaylist = new MainCommand(x => CanDeleteOrClear(), x => DeleteFile());
            ClearSongsPaths = new MainCommand(x => CanDeleteOrClear(), x => ClearPlaylist());
        }

        public ICommand GetSongsPaths { get; private set; }
        public ICommand DeleteSongFromPlaylist { get; private set; }
        public ICommand ClearSongsPaths { get; private set; }

        public void OpenFileDialog()
        {
            var fileDialog = new OpenFileDialog { Multiselect = true };
            if (fileDialog.ShowDialog() != null)
                foreach (var filename in fileDialog.FileNames.Where(x => x.ToLower().EndsWith(".mp3")))
                    Playlist.SongsList.Add(filename);
        }

        public bool CanDeleteOrClear() => 
            Playlist?.SongsList == null ? false : true;

        public void DeleteFile() =>
            Playlist.SongsList.Remove(Playlist.SelectedSong);

        public void ClearPlaylist() =>
            Playlist.SongsList.ToList().All(x => Playlist.SongsList.Remove(x));
    }
}