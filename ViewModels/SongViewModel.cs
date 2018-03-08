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
        private WaveOut player;
        private Song song;
        private Counter counter;

        public WaveOut Player => player;
        public Song Song
        {
            get => song;
            set
            {
                song = value;
                OnPropertyChanged("Song");
            }
        }
        public Counter Counter
        {
            get => counter;
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
            Song = new Song(null);
            Counter = new Counter();
            player = new WaveOut();
            PlaySong = new MainCommand(r => CanPlayMusic(r as Playlist), r => PlayMusic(r as Playlist));
            PauseSong = new MainCommand(r => CanPauseSong(), r => SongPause());
            PlayNextSong = new MainCommand(r => CanPlayBackOrNextSong(r as Playlist), r => UniversalPlay(r as Playlist, PlayType.Next));
            PlayBackSong = new MainCommand(r => CanPlayBackOrNextSong(r as Playlist), r => UniversalPlay(r as Playlist, PlayType.Back));
        }

        public bool CanPlayMusic(Playlist _pathsSongs)
        {
            if (_pathsSongs != null || Song.IsPausing)
            {
                if (!Song.IsPausing && string.IsNullOrWhiteSpace(_pathsSongs.SongsList.FirstOrDefault()))
                    return false;
                if (new[] { _pathsSongs.SelectedSong, Song.SongPath }
                   .Any(z => !string.IsNullOrWhiteSpace(z) && z.Split('.')[z.Split('.').Length - 1].ToLower() == "mp3"))
                    return true;
                return false;
            }
            return false;
        }

        public bool CanPauseSong() => Song != null ? Song.IsPlaying ? true : false : false;

        public bool CanPlayBackOrNextSong(Playlist _pathsSongs) =>
            _pathsSongs != null ? !string.IsNullOrWhiteSpace(_pathsSongs.SelectedSong) ? Song != null ? true : false : false : false;

        public void PlayMusic(Playlist _pathsSongs)
        {
            if ((!Song.IsPlaying && Song.IsPausing) && (new[] { Song.SongPath, _pathsSongs.SelectedSong }.Any(q => q == Song.SongPath)))
            {
                Player.Play();
                Song.IsPausing = false;
                Song.IsPlaying = true;
                return;
            }
            else if (Song.IsPlaying)
                Player.Pause();
            PlayerHelper(_pathsSongs);
        }

        public void SongPause()
        {
            Player.Pause();
            Song.IsPlaying = false;
            Song.IsPausing = true;
        }

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
        private string GetNewSongPath(ObservableCollection<string> _songsList, PlayType _playType)
        {
            int currentlyID = 0;
            string newSongPath = string.Empty;
            var songsListWithIDs = _songsList.Select((x, i) => new { Index = i, Value = x });

            foreach (var newIndex in songsListWithIDs.Where(v => v.Value == Song.SongPath).Select(b => b.Index))
                currentlyID = newIndex;

            foreach (var newSong in songsListWithIDs.Where(q => q.Index == (NewSongID(currentlyID, _songsList.Count, _playType))).Select(b => b.Value))
                newSongPath = newSong;

            return newSongPath;
        }

        private int NewSongID(int _currentlyID, int _totalCount, PlayType _playType)
        {
            if (_playType == PlayType.Next)
                return _currentlyID == _totalCount - 1 ? 0 : _currentlyID + 1;
            return _currentlyID == 0 ? _totalCount - 1 : _currentlyID - 1;
        }

        private void PlayerHelper(Playlist _pathsSongs)
        {
            if (!string.IsNullOrWhiteSpace(_pathsSongs.SelectedSong))
            {
                Song = new Song(_pathsSongs.SelectedSong) { IsPlaying = true, Volume = Song.Volume };
                Counter.Song = Song;
                Counter.PositionMax = Song.MP3.TotalTime.TotalSeconds;
                Counter.CountTime((obj, e) =>
                {
                    if (Counter.Song.MP3.CurrentTime == Counter.Song.MP3.TotalTime && _pathsSongs.SongsList.FirstOrDefault() != null)
                    {
                        _pathsSongs.SelectedSong = GetNewSongPath(_pathsSongs.SongsList, PlayType.Next);
                        PlayMusic(_pathsSongs);
                    }
                    Counter.TimeText = string.Format("{0} {1}", Counter.Song.MP3.CurrentTime.ToString(@"hh\:mm\:ss"), Counter.Song.MP3.TotalTime.ToString().Split('.')[0]);
                    if (Counter.Song.IsPlaying)
                        Counter.PositionValue = Counter.Song.MP3.CurrentTime.TotalSeconds;
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
