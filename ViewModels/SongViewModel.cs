using MP3Player.Commands;
using MP3Player.Enums;
using MP3Player.Models;
using NAudio.Wave;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace MP3Player.ViewModels
{
    public class SongViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        private Song song;
        public Song Song => song;

        private WaveOut player;
        public WaveOut Player => player;

        private Counter counter;
        public Counter Counter => counter;

        private string songName;
        public string SongName
        {
            get => songName;
            set
            {
                songName = value;
                NotifyPropertyChanged("SongName");
            }
        }

        private string currentlySongPath;
        public string CurrentlySongPath => currentlySongPath;

        private float volume;
        public float Volume
        {
            get => volume;
            set
            {
                volume = float.Parse(value.ToString("0"));
                song.Volume = volume / 100;
                NotifyPropertyChanged("Volume");
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
            player = new WaveOut();
            PlaySong = new MainCommand(a => CanPlayMusic(a as Playlist), v => PlayMusic(v as Playlist));
            PauseSong = new MainCommand(x => CanPauseSong(), q => SongPause(q as Playlist));
            PlayNextSong = new MainCommand(q => CanPlayBackOrNextSong(q as Playlist), z => UniversalPlay(z as Playlist, PlayType.Next));
            PlayBackSong = new MainCommand(z => CanPlayBackOrNextSong(z as Playlist), i => UniversalPlay(i as Playlist, PlayType.Back));
        }

        public bool CanPlayMusic(Playlist pathsSongs) 
        {
            if (pathsSongs != null || song.IsPausing)
            {
                if (!song.IsPausing && string.IsNullOrWhiteSpace(pathsSongs.SongsList.FirstOrDefault()))
                    return false;
                if (new[] { pathsSongs.SelectedSong, currentlySongPath }.Any(z => !string.IsNullOrWhiteSpace(z) && z.Split('.')[z.Split('.').Length - 1].ToLower() == "mp3"))
                    return true;
                return false;
            }
            return false;
        }

        public void PlayMusic(Playlist pathsSongs)
        {
            if ((!song.IsPlaying && song.IsPausing) && (new[] { currentlySongPath, pathsSongs.SelectedSong }.Any(q => q == song.SongPath)))
            {
                Player.Play();
                song.IsPausing = false;
                song.IsPlaying = true;
                return;
            }
            else if (song.IsPlaying)
                Player.Pause();
            PlayerHelper(pathsSongs);
        }

        public bool CanPauseSong() => song != null ? song.IsPlaying ? true : false : false;
        public void SongPause(Playlist pathsSongs)
        {
            currentlySongPath = song.SongPath;
            Player.Pause();
            song.IsPlaying = false;
            song.IsPausing = true;
        }

        public bool CanPlayBackOrNextSong(Playlist pathsSongs) => pathsSongs != null ? !string.IsNullOrWhiteSpace(pathsSongs.SelectedSong) ? song != null ? true : false : false : false;
        public void UniversalPlay(Playlist pathsSongs, PlayType playType)
        {
            if (song.IsPlaying)
            {
                Player.Pause();
                song.IsPausing = false;
            }
            pathsSongs.SelectedSong = GetNewSongPath(pathsSongs.SongsList, playType);
            PlayerHelper(pathsSongs);
        }

        #region Helper Methods
        private string GetNewSongPath(ObservableCollection<string> songsList, PlayType playType)
        {
            int currentlyID = 0;
            string newSongPath = string.Empty;
            var songsListWithIDs = songsList.Select((x, i) => new { Index = i, Value = x });
            foreach (var newIndex in songsListWithIDs.Where(v => v.Value == song.SongPath).Select(b => b.Index))
                currentlyID = newIndex;
            foreach (var newSong in songsListWithIDs.Where(q => q.Index == (NewSongID(currentlyID, songsList.Count, playType))).Select(b => b.Value))
                newSongPath = newSong;
            return newSongPath;
        }

        private int NewSongID(int currentlyID, int totalCount, PlayType playType)
        {
            if (playType == PlayType.Next)
                return currentlyID == totalCount - 1 ? 0 : currentlyID + 1;
            return currentlyID == 0 ? totalCount - 1 : currentlyID - 1;
        }

        private void PlayerHelper(Playlist pathsSongs)
        {
            song = new Song(pathsSongs.SelectedSong) { IsPlaying = true };
            counter.Song = song;
            counter.PositionMax = song.MP3.TotalTime.TotalSeconds;
            counter.CountTime((obj, e) =>
            {
                if (counter.Song.MP3.CurrentTime == counter.Song.MP3.TotalTime && pathsSongs.SongsList.FirstOrDefault() != null)
                {
                    pathsSongs.SelectedSong = GetNewSongPath(pathsSongs.SongsList, PlayType.Next);
                    PlayMusic(pathsSongs);
                }
                counter.TimeText = string.Format("{0} {1}", counter.Song.MP3.CurrentTime.ToString(@"hh\:mm\:ss"), counter.Song.MP3.TotalTime.ToString().Split('.')[0]);
                if (counter.Song.IsPlaying)
                    counter.PositionValue = counter.Song.MP3.CurrentTime.TotalSeconds;
            });
            counter.ChangePosition();
            SongName = song.MP3.FileName.Split('\\')[song.MP3.FileName.Split('\\').Length - 1];
            song.MP3.Volume = Volume / 100;
            Player.Init(song.MP3);
            Player.Play();
        }
        #endregion
    }
}
