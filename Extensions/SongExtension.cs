using MP3Player.Enums;
using MP3Player.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace MP3Player.Extensions
{
    public static class SongExtension
    {
        /// <summary>
        /// Create songsList with IDs and get songPath to the next song,
        /// if we choose PlayType NEXT we will play next song
        /// else if we choose PlayType BACK we will play earlier song
        /// </summary>
        /// <param name="_songsList"></param>
        /// <param name="_playType"></param>
        /// <returns></returns>
        public static string GetNewSongPath(ObservableCollection<string> _songsList, PlayType _playType, Song _song)
        {
            var songsListWithIDs = _songsList.Select((x, i) => new { Index = i, Value = x });
            var currentlyID = songsListWithIDs.First(x => x.Value == _song.SongPath).Index;
            var ID = NewSongID(currentlyID, _songsList.Count, _playType);
            return songsListWithIDs.First(x => x.Index == ID).Value;
        }

        /// <summary>
        /// Helper method to get new ID (which help us to get new SongPath in GetNewSongPath() method)
        /// </summary>
        /// <param name="_currentlyID"></param>
        /// <param name="_totalCount"></param>
        /// <param name="_playType"></param>
        /// <returns></returns>
        public static int NewSongID(int _currentlyID, int _totalCount, PlayType _playType)
        {
            if (_playType == PlayType.Next)
                return _currentlyID == _totalCount - 1 ? 0 : _currentlyID + 1;
            return _currentlyID == 0 ? _totalCount - 1 : _currentlyID - 1;
        }
    }
}


