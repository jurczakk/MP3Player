namespace MP3Player.Interfaces.Helpers
{
    public interface IPlaylistHelpers
    {
        bool CanDeleteOrClear();
        bool CanDelete();
        void ClearPlaylist();
        void DeleteFile();
        void OpenFileDialog();
    }
}