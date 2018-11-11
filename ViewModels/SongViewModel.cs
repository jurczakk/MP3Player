using MP3Player.Commands;
using MP3Player.Enums;
using MP3Player.Models;
using NAudio.Wave;
using System.Linq;
using System.Windows.Input;
using E = MP3Player.Extensions.SongExtension;

namespace MP3Player.ViewModels
{
    public class SongViewModel : BaseViewModel
    {
        //Fields
        private WaveOut player;
        private Song song;
        private Counter counter;

        //Properties
        public WaveOut Player => player;
        public Song Song
        {
            get { return song; }
            set { song = value; OnPropertyChanged("Song"); }
        }
        public Counter Counter
        {
            get { return counter; }
            set { counter = value; OnPropertyChanged("Counter"); }
        }

        public ICommand PlaySong { get; private set; }
        public ICommand PauseSong { get; private set; }
        public ICommand PlayNextSong { get; private set; }
        public ICommand PlayBackSong { get; private set; }

        public SongViewModel()
        {
            song = new Song(null);
            counter = new Counter();
            player = new WaveOut();
            PlaySong = new MainCommand(r => CanPlayMusic(r as Playlist), r => PlayMusic(r as Playlist));
            PauseSong = new MainCommand(r => CanPauseSong(), r => SongPause());
            PlayNextSong = new MainCommand(r => CanPlayBackOrNextSong(r as Playlist),
                                           r => UniversalPlay(r as Playlist, PlayType.Next));
            PlayBackSong = new MainCommand(r => CanPlayBackOrNextSong(r as Playlist),
                                           r => UniversalPlay(r as Playlist, PlayType.Back));
        }

        /// <summary>
        /// Check if can play song
        /// if currently song isPausing or our playlist is not null
        /// if selected song has not then we can PlayMusic
        /// </summary>
        public bool CanPlayMusic(Playlist _pathsSongs)
        {
            if (_pathsSongs != null || Song.IsPausing &&
                new[] { _pathsSongs.SelectedSong, Song.SongPath }.Any(r => !string.IsNullOrWhiteSpace(r)))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// if Song is not null, and isPlaying boolean is set as true, we can Pause song
        /// </summary>
        public bool CanPauseSong()
        {
            if (Song == null)
                return false;
            if (!Song.IsPlaying)
                return false;
            return true;
        }

        /// <summary>
        /// Check if we can play earlier or next song
        /// </summary>
        public bool CanPlayBackOrNextSong(Playlist _pathsSongs)
        {
            if (_pathsSongs == null)
                return false;
            if (string.IsNullOrWhiteSpace(_pathsSongs.SelectedSong))
                return false;
            if (Song == null)
                return false;
            return true;
        }

        /// <summary>
        /// We're using this method to play selected song by click on button
        /// </summary>
        public void PlayMusic(Playlist _pathsSongs)
        {
            if ((!Song.IsPlaying && Song.IsPausing) &&
                (new[] { Song.SongPath, _pathsSongs.SelectedSong }.Any(q => q == Song.SongPath)))
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
            PlayerHelper(_pathsSongs);
        }

        /// <summary>
        /// Pause the song
        /// </summary>
        public void SongPause()
        {
            Player.Pause();
            Song.IsPlaying = false;
            Song.IsPausing = true;
        }

        /// <summary>
        /// We're using this method to play earlier/next song, by click on button
        /// </summary>
        public void UniversalPlay(Playlist _pathsSongs, PlayType _playType)
        {
            if (Song.IsPlaying)
            {
                Player.Pause();
                Song.IsPausing = false;
            }
            _pathsSongs.SelectedSong = E.GetNewSongPath(_pathsSongs.SongsList, _playType, Song);
            PlayerHelper(_pathsSongs);
        }

        /// <summary>
        /// Main logic of playing music
        /// First of all we check if our Selected Song Path is not null 
        /// then we will create new instance of Song, and use our Counter Model
        /// and CountTime method, to calculate a time of our song, 
        /// and change its position on position slider.
        /// set volume etc.
        /// </summary>
        private void PlayerHelper(Playlist _pathsSongs)
        {
            if (!string.IsNullOrWhiteSpace(_pathsSongs.SelectedSong))
            {
                Song = new Song(_pathsSongs.SelectedSong) { IsPlaying = true, Volume = Song.Volume };
                Counter.Song = Song;
                Counter.PositionMax = Song.MP3.TotalTime.TotalSeconds;
                Counter.CountTime((obj, e) =>
                {
                    if (Counter.Song.MP3.CurrentTime == Counter.Song.MP3.TotalTime &&
                        _pathsSongs.SongsList.FirstOrDefault() != null)
                    {
                        _pathsSongs.SelectedSong = E.GetNewSongPath(_pathsSongs.SongsList, PlayType.Next, Song);
                        PlayMusic(_pathsSongs);
                    }

                    Counter.TimeText = string.Format("{0} {1}",
                        Counter.Song.MP3.CurrentTime.ToString(@"hh\:mm\:ss"),
                        Counter.Song.MP3.TotalTime.ToString().Split('.')[0]);

                    if (Counter.Song.IsPlaying)
                        Counter.PositionValue = Counter.Song.MP3.CurrentTime.TotalSeconds;

                });
                Counter.ChangePosition();
                Song.SongName = System.IO.Path.GetFileName(Song.MP3.FileName);
                Song.MP3.Volume = Song.Volume / 100;
                Player.Init(Song.MP3);
                Player.Play();
            }
        }
    }
}
