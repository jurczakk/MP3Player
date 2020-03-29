namespace MP3Player.Interfaces.Helpers
{
    public interface IPlaylistHelpers
    {
        bool CanDeleteOrClear();
        void ClearPlaylist();
        void DeleteFile();
        void OpenFileDialog();
    }
}