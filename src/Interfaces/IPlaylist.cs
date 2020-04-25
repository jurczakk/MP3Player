using System;
using System.Collections.Generic;

namespace MP3Player.Interfaces
{
    public interface IPlaylist
    {
        Tuple<int, string> SelectedSong { get; set; }
        IList<Tuple<int, string>> SongsList { get; set; }
    }
}