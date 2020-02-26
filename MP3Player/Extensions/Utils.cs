using MP3Player.Enums;
using MP3Player.Abstracts;
using System.Collections.Generic;
using System.Linq;

namespace MP3Player.Extensions
{
    public static class Utils
    {
       public static string GetNewSongPath(IList<string> songsList, PlayType playType, ISong song)
        {
            var songsListWithIDs = songsList.Select((x, i) => new { Index = i, Value = x });
            var currentlyID = songsListWithIDs.First(x => x.Value == song.Path).Index;
            var ID = currentlyID == 0 ? songsList.Count - 1 : currentlyID - 1;

            if (playType == PlayType.Next)
                ID = currentlyID == songsList.Count - 1 ? 0 : currentlyID + 1;

            return songsListWithIDs.First(x => x.Index == ID).Value;
        }
    }
}
