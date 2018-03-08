using MP3Player.Commands;
using MP3Player.Models;
using System.Windows.Input;
using System.Linq;
using System.Windows.Forms;

namespace MP3Player.ViewModels
{
    public class PlaylistViewModel
    {
        private Playlist playlist;
        public Playlist Playlist => playlist;

        public PlaylistViewModel()
        {
            playlist = new Playlist();
            GetSongsPaths = new MainCommand(x => CanOpenFileDialog(), x => OpenFileDialog());
            DeleteSongFromPlaylist = new MainCommand(x => CanDeleteOrClear(), x => DeleteFile());
            ClearSongsPaths = new MainCommand(x => CanDeleteOrClear(), x => ClearPlaylist());
        }

        public ICommand GetSongsPaths { get; private set; }
        public ICommand DeleteSongFromPlaylist { get; private set; }
        public ICommand ClearSongsPaths { get; private set; }

        public bool CanOpenFileDialog() => true;
        public bool CanDeleteOrClear() => Playlist != null ? Playlist.SongsList != null ? true : false : false;
        public void OpenFileDialog()
        {
            var fileDialog = new OpenFileDialog { Multiselect = true };

            if (fileDialog.ShowDialog() != null)
                foreach (var fileName in fileDialog.FileNames.Where(u => u.Split('.').Last().ToLower() == "mp3"))
                    Playlist.SongsList.Add(fileName);
        }
        public void DeleteFile() => Playlist.SongsList.Remove(Playlist.SelectedSong);
        public void ClearPlaylist() => Playlist.SongsList.ToList().All(y => Playlist.SongsList.Remove(y));
    }
}
