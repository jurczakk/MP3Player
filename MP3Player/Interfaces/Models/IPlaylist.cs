using System.Collections.Generic;

namespace MP3Player.Interfaces.Models
{
    public interface IPlaylist
    {
        string SelectedSong { get; set; }
        IList<string> SongsList { get; set; }
    }
}