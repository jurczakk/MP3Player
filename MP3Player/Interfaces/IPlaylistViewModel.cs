using System.Windows.Input;

namespace MP3Player.Interfaces
{
    public interface IPlaylistViewModel
    {
        ICommand ClearSongsPaths { get; }
        ICommand DeleteSongFromPlaylist { get; }
        ICommand GetSongsPaths { get; }
        IPlaylist Playlist { get; }

        bool CanDeleteOrClear();
        void ClearPlaylist();
        void DeleteFile();
        void OpenFileDialog();
    }
}