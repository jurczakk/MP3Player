using System.Collections.Generic;

namespace MP3Player.Interfaces
{
    public interface IPlaylist
    {
        ISongData SelectedSong { get; set; }
        IList<ISongData> SongsList { get; set; }
    }
}