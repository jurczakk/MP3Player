using MP3Player.Commands;
using MP3Player.Enums;
using MP3Player.Models;
using NAudio.Wave;
using System.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.IO;

namespace MP3Player.ViewModels
{
    public class SongViewModel : BaseViewModel
    {
        private Song song;
        private Counter counter;

        public WaveOut Player { get; }
        public Song Song
        {
            get { return song; }
            set 
            { 
                song = value; 
                OnPropertyChanged("Song"); 
            }
        }
        public Counter Counter
        {
            get { return counter; }
            set
            {
                counter = value; 
                OnPropertyChanged("Counter");
            }
        }

        public ICommand PlaySong { get; private set; }
        public ICommand PauseSong { get; private set; }
        public ICommand PlayNextSong { get; private set; }
        public ICommand PlayBackSong { get; private set; }

        public SongViewModel()
        {
            song = new Song(null);
            counter = new Counter();
            Player = new WaveOut();
            PlaySong = new MainCommand(x => CanPlayMusic(x as Playlist), x => PlayMusic(x as Playlist));
            PauseSong = new MainCommand(x => CanPauseSong(), x => SongPause());
            PlayNextSong = new MainCommand(x => CanPlayMusic(x as Playlist), x => UniversalPlay(x as Playlist, PlayType.Next));
            PlayBackSong = new MainCommand(x => CanPlayMusic(x as Playlist), x => UniversalPlay(x as Playlist, PlayType.Back));
        }

        public bool CanPlayMusic(Playlist playlist) =>
            string.IsNullOrWhiteSpace(playlist?.SelectedSong) || Song == null ? false : true;

        public bool CanPauseSong() =>
            Song == null || !Song.IsPlaying ? false : true;

        public void PlayMusic(Playlist playlist)
        {
            if (!Song.IsPlaying && Song.IsPausing && (new[] { Song.Path, playlist.SelectedSong }.Any(x => x == Song.Path)))
            {
                Player.Play();
                Song.IsPausing = false;
                Song.IsPlaying = true;
                return;
            }
            else if (Song.IsPlaying)
            {
                Player.Pause();
            }

            PlayerHelper(playlist);
        }

        public void SongPause()
        {
            Player.Pause();
            Song.IsPlaying = false;
            Song.IsPausing = true;
        }

        public void UniversalPlay(Playlist playlist, PlayType playType)
        {
            if (Song.IsPlaying)
            {
                Player.Pause();
                Song.IsPausing = false;
            }

            playlist.SelectedSong = GetNewSongPath(playlist.SongsList, playType, Song);
            PlayerHelper(playlist);
        }

        private void PlayerHelper(Playlist playlist)
        {
            if (!string.IsNullOrWhiteSpace(playlist.SelectedSong))
            {
                Song = new Song(playlist.SelectedSong) { IsPlaying = true, Volume = Song.Volume };
                Counter.Song = Song;
                Counter.PositionMax = Song.MP3.TotalTime.TotalSeconds;
                Counter.CountTime((obj, e) =>
                {
                    if (Counter.Song.MP3.CurrentTime == Counter.Song.MP3.TotalTime && playlist.SongsList.FirstOrDefault() != null)
                    {
                        playlist.SelectedSong = GetNewSongPath(playlist.SongsList, PlayType.Next, Song);
                        PlayMusic(playlist);
                    }

                    Counter.TimeText = string.Format("{0} {1}",
                        Counter.Song.MP3.CurrentTime.ToString(@"hh\:mm\:ss"),
                        Counter.Song.MP3.TotalTime.ToString().Split('.')[0]);

                    if (Counter.Song.IsPlaying)
                        Counter.PositionValue = Counter.Song.MP3.CurrentTime.TotalSeconds;
                });

                Counter.ChangePosition();
                Song.Name = Path.GetFileName(Song.MP3.FileName);
                Song.MP3.Volume = Song.Volume / 100;
                Player.Init(Song.MP3);
                Player.Play();
            }
        }

        private string GetNewSongPath(ObservableCollection<string> songsList, PlayType playType, Song song)
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