using MP3Player.Commands;
using MP3Player.Enums;
using MP3Player.Abstracts;
using MP3Player.Interfaces;
using MP3Player.Models;
using System.Windows.Input;
using NAudio.Wave;
using System.Threading;
using System.IO;
using System.Linq;

namespace MP3Player.ViewModels
{
    public class SongViewModel : NotifyPropertyChanged, ISongViewModel
    {
        private WaveOut WavePlayer = new WaveOut();
        private Timer Timer;

        private ISong song;
        public ISong Song
        {
            get
            {
                return song;
            }
            set
            {
                song = value;
                OnPropertyChanged("Song");
            }
        }
        public ICommand Play { get; set; }
        public ICommand Pause { get; set; }
        public ICommand PlayNext { get; set; }
        public ICommand PlayBack { get; set; }

        public SongViewModel() { }
        public SongViewModel(ISong song)
        {
            Song = song;

            Play = new MainCommand(
                x => CanPlay(x as IPlaylist),
                x => PlaySong(x as IPlaylist));

            Pause = new MainCommand(
                x => CanPause(),
                x => PauseSong());

            PlayNext = new MainCommand(
                x => CanPlay(x as IPlaylist),
                x => UniversalPlay(x as IPlaylist, PlayType.Next));

            PlayBack = new MainCommand(
                x => CanPlay(x as IPlaylist),
                x => UniversalPlay(x as IPlaylist, PlayType.Back));
        }

        private bool CanPause()
        {
            if (Song == null || !Song.IsPlaying)
                return false;
            return true;
        }

        private bool CanPlay(IPlaylist playlist)
        {
            if (Song == null || string.IsNullOrWhiteSpace(playlist?.SelectedSong?.Path))
                return false;
            return true;
        }

        private void PlaySong(IPlaylist playlist)
        {
            if (!Song.IsPlaying && Song.IsPausing && playlist.SelectedSong.Path == Song.Path)
            {
                WavePlayer.Play();
                Song.IsPlaying = true;
                Song.IsPausing = false;
                return;
            }
            else if (Song.IsPlaying)
            {
                WavePlayer.Pause();
            }

            PlayerHelper(playlist);
        }

        private void PauseSong()
        {
            WavePlayer.Pause();
            Song.IsPlaying = false;
            Song.IsPausing = true;
        }

        private void UniversalPlay(IPlaylist playlist, PlayType playType)
        {
            if (Song.IsPlaying)
            {
                WavePlayer.Pause();
                Song.IsPausing = false;
            }

            playlist.SelectedSong = GetNewSongPath(playlist, playType);

            PlayerHelper(playlist);
        }

        private void PlayerHelper(IPlaylist playlist)
        {
            if (!File.Exists(playlist.SelectedSong.Path))
                playlist.SongsList.Remove(playlist.SelectedSong);

            if (!string.IsNullOrWhiteSpace(playlist.SelectedSong.Path) && File.Exists(playlist.SelectedSong.Path))
            {
                Song = new Song(playlist.SelectedSong.Path, Song.Volume) { IsPlaying = true };
                WavePlayer.Init(Song.MP3);
                WavePlayer.Play();

                Timer = new Timer(e =>
                {
                    if (Song.MP3.CurrentTime == Song.MP3.TotalTime && playlist.SongsList.FirstOrDefault() != null)
                    {
                        if (playlist.SelectedSong == null)
                            return;

                        playlist.SelectedSong = GetNewSongPath(playlist, PlayType.Next);
                        PlayerHelper(playlist);
                    }

                    Song.TimeText =
                        $"{Song.MP3.CurrentTime:hh\\:mm\\:ss} {Song.MP3.TotalTime.ToString().Split('.')[0]}";

                    if (Song.IsPlaying)
                        Song.PositionValue = Song.MP3.CurrentTime.TotalSeconds;
                }, null, 1000, 1000);
            }
        }

        private ISongData GetNewSongPath(IPlaylist playlist, PlayType playType)
        {
            var id = playlist.SelectedSong.Id;
            var lastElement = playlist.SongsList.Count - 1;

            if (playType == PlayType.Next)
            {
                if (id == lastElement)
                {
                    id = 0;
                }
                else
                {
                    ++id;
                }
            }
            else if (playType == PlayType.Back)
            {
                if (id == 0)
                {
                    id = lastElement;
                }
                else
                {
                    --id;
                }
            }
            
            return playlist.SongsList[id];
        }
    }
}