using MP3Player.Interfaces;

namespace MP3Player.Models
{
    public class SongData : ISongData
    {
        public int Id { get; set; }
        public string Path { get; set; }
    }
}
