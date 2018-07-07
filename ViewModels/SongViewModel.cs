using MP3Player.Commands;
using MP3Player.Enums;
using MP3Player.Models;
using NAudio.Wave;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

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
            Song = new Song(null);
            Counter = new Counter();
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
        /// if selected song does not null then we can PlayMusic
        /// </summary>
        /// <param name="_pathsSongs"></param>
        /// <returns></returns>
        public bool CanPlayMusic(Playlist _pathsSongs)
        {
            if (_pathsSongs!=null || Song.IsPausing && 
                new[] { _pathsSongs.SelectedSong, Song.SongPath }.Any(r => !string.IsNullOrWhiteSpace(r)))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// if Song is not null, and isPlaying boolean is set as true, we can Pause song
        /// </summary>
        /// <returns></returns>
        public bool CanPauseSong() => Song != null ? Song.IsPlaying ? true : false : false;


        /// <summary>
        /// Check if we can play earlier or Next song
        /// </summary>
        /// <param name="_pathsSongs"></param>
        /// <returns></returns>
        public bool CanPlayBackOrNextSong(Playlist _pathsSongs) =>
            _pathsSongs != null 
                ? !string.IsNullOrWhiteSpace(_pathsSongs.SelectedSong) 
                    ? Song != null 
                        ? true 
                        : false
                : false
            : false;


        /// <summary>
        /// We're using this method to play selected song by click on button
        /// </summary>
        /// <param name="_pathsSongs"></param>
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
        /// Just pause the song
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
        /// <param name="_pathsSongs"></param>
        /// <param name="_playType"></param>
        public void UniversalPlay(Playlist _pathsSongs, PlayType _playType)
        {
            if (Song.IsPlaying)
            {
                Player.Pause();
                Song.IsPausing = false;
            }
            _pathsSongs.SelectedSong = GetNewSongPath(_pathsSongs.SongsList, _playType);
            PlayerHelper(_pathsSongs);
        }

        #region Helper Methods
        /// <summary>
        /// Create songsList with IDs and get songPath to the next song,
        /// if we choose PlayType NEXT we will play next song
        /// else if we choose PlayType BACK we will play earlier song
        /// </summary>
        /// <param name="_songsList"></param>
        /// <param name="_playType"></param>
        /// <returns></returns>
        private string GetNewSongPath(ObservableCollection<string> _songsList, PlayType _playType)
        {
            var songsListWithIDs = _songsList.Select((x, i) => new { Index = i, Value = x });
            var currentlyID = songsListWithIDs.First(x => x.Value == Song.SongPath).Index;
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
        private int NewSongID(int _currentlyID, int _totalCount, PlayType _playType)
        {
            if (_playType == PlayType.Next)
                return _currentlyID == _totalCount - 1 ? 0 : _currentlyID + 1;
            return _currentlyID == 0 ? _totalCount - 1 : _currentlyID - 1;
        }


        /// <summary>
        /// Main logic of playin' music
        /// First of all we check if our Selected Song Path is not null 
        /// then we will create new instance of Song, and use our Counter Model
        /// And Use CountTime method, to calculate a time of our song, 
        /// and change its position on position slider.
        /// set volume etc.
        /// </summary>
        /// <param name="_pathsSongs"></param>
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
                        _pathsSongs.SelectedSong = GetNewSongPath(_pathsSongs.SongsList, PlayType.Next);
                        PlayMusic(_pathsSongs);
                    }
                    
                    Counter.TimeText = string.Format("{0} {1}", 
                        Counter.Song.MP3.CurrentTime.ToString(@"hh\:mm\:ss"),
                        Counter.Song.MP3.TotalTime.ToString().Split('.')[0]);
                        
                    if (Counter.Song.IsPlaying)
                    {
                        Counter.PositionValue = Counter.Song.MP3.CurrentTime.TotalSeconds;
                    }    
                });
                Counter.ChangePosition();
                Song.SongName = Song.MP3.FileName.Split('\\')[Song.MP3.FileName.Split('\\').Length - 1];
                Song.MP3.Volume = Song.Volume / 100;
                Player.Init(Song.MP3);
                Player.Play();
            }
        }
        #endregion
    }
}
