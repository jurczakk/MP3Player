using MP3Player.Commands;
using MP3Player.Enums;
using MP3Player.Models;
using NAudio.Wave;
using System.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace MP3Player.ViewModels
{
    public class SongViewModel : BaseViewModel
    {
        private WaveOut player;
        private Song song;
        private Counter counter;

        public WaveOut Player { get { return player; } }
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

        public bool CanPlayMusic(Playlist _pathsSongs)
        {
            if (_pathsSongs != null || Song.IsPausing &&
                new[] { _pathsSongs.SelectedSong, Song.Path }.Any(r => !string.IsNullOrWhiteSpace(r)))
            {
                return true;
            }
            return false;
        }

        public bool CanPauseSong()
        {
            if (Song == null)
                return false;
            if (!Song.IsPlaying)
                return false;
            return true;
        }

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

        public void PlayMusic(Playlist _pathsSongs)
        {
            if ((!Song.IsPlaying && Song.IsPausing) &&
                (new[] { Song.Path, _pathsSongs.SelectedSong }.Any(q => q == Song.Path)))
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

            _pathsSongs.SelectedSong = GetNewSongPath(_pathsSongs.SongsList, _playType, Song);
            PlayerHelper(_pathsSongs);
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
                    if (Counter.Song.MP3.CurrentTime == Counter.Song.MP3.TotalTime &&
                        _pathsSongs.SongsList.FirstOrDefault() != null)
                    {
                        _pathsSongs.SelectedSong = GetNewSongPath(_pathsSongs.SongsList, PlayType.Next, Song);
                        PlayMusic(_pathsSongs);
                    }

                    Counter.TimeText = string.Format("{0} {1}",
                        Counter.Song.MP3.CurrentTime.ToString(@"hh\:mm\:ss"),
                        Counter.Song.MP3.TotalTime.ToString().Split('.')[0]);

                    if (Counter.Song.IsPlaying)
                        Counter.PositionValue = Counter.Song.MP3.CurrentTime.TotalSeconds;

                });
                Counter.ChangePosition();
                Song.Name = System.IO.Path.GetFileName(Song.MP3.FileName);
                Song.MP3.Volume = Song.Volume / 100;
                Player.Init(Song.MP3);
                Player.Play();
            }
        }

        private string GetNewSongPath(ObservableCollection<string> _songsList, PlayType _playType, Song _song)
        {
            var songsListWithIDs = _songsList.Select((x, i) => new { Index = i, Value = x });
            var currentlyID = songsListWithIDs.First(x => x.Value == _song.Path).Index;
            var ID = currentlyID == 0 ? _songsList.Count - 1 : currentlyID - 1;
            if (_playType == PlayType.Next)
                ID = currentlyID == _songsList.Count - 1 ? 0 : currentlyID + 1;
            return songsListWithIDs.First(x => x.Index == ID).Value;
        }
    }
}