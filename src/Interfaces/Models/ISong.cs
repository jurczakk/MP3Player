using NAudio.Wave;

namespace MP3Player.Interfaces.Models
{
    public interface ISong
    {
        bool IsPausing { get; set; }
        bool IsPlaying { get; set; }
        AudioFileReader MP3 { get; set; }
        string Name { get; set; }
        string Path { get; set; }
        double PositionMax { get; set; }
        double PositionValue { get; set; }
        string TimeText { get; set; }
        float Volume { get; set; }
        void ChangePosition();
    }
}