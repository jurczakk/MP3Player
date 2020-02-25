using MP3Player.Commands;
using MP3Player.Enums;
using MP3Player.Models;
using NAudio.Wave;
using System.Linq;
using System.Windows.Input;
using System.IO;
using MP3Player.Interfaces;
using MP3Player.Helpers;

namespace MP3Player.ViewModels
{
    public class SongViewModel : BaseViewModel, ISongViewModel
    {
        private ISong song;
        public ISong Song
        {
            get { return song; }
            set { song = value; OnPropertyChanged("Song"); }
        }
        public WaveOut Player { get; }
        public ICommand PlaySong { get; private set; }
        public ICommand PauseSong { get; private set; }
        public ICommand PlayNextSong { get; private set; }
        public ICommand PlayBackSong { get; private set; }

        public SongViewModel()
        {
            Player = new WaveOut();
            song = new Song();

            PlaySong = new MainCommand(x => CanPlayMusic(x as Playlist), x => PlayMusic(x as Playlist));
            PauseSong = new MainCommand(x => CanPauseSong(), x => SongPause());
            PlayNextSong = new MainCommand(x => CanPlayMusic(x as Playlist), x => UniversalPlay(x as Playlist, PlayType.Next));
            PlayBackSong = new MainCommand(x => CanPlayMusic(x as Playlist), x => UniversalPlay(x as Playlist, PlayType.Back));
        }

        public bool CanPlayMusic(IPlaylist playlist)
        {
            return !(Song == null || string.IsNullOrWhiteSpace(playlist?.SelectedSong));
        }

        public bool CanPauseSong()
        {
            return !(Song == null || !Song.IsPlaying);
        }

        public void PlayMusic(IPlaylist playlist)
        {
            if (!Song.IsPlaying && Song.IsPausing && playlist.SelectedSong == Song.Path)
            {
                Player.Play();
                Song.IsPlaying = true;
                Song.IsPausing = false;
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

        public void UniversalPlay(IPlaylist playlist, PlayType playType)
        {
            if (Song.IsPlaying)
            {
                Player.Pause();
                Song.IsPausing = false;
            }

            playlist.SelectedSong = Utils.GetNewSongPath(playlist.SongsList, playType, Song);
            PlayerHelper(playlist);
        }

        private void PlayerHelper(IPlaylist playlist)
        {
            if (!File.Exists(playlist.SelectedSong))
                playlist.SongsList.Remove(playlist.SelectedSong);

            if (!string.IsNullOrWhiteSpace(playlist.SelectedSong) && File.Exists(playlist.SelectedSong))
            {
                Song = new Song(playlist.SelectedSong, Song.Volume) { IsPlaying = true };
                Song.PositionMax = Song.MP3.TotalTime.TotalSeconds;
               
                Song.CountTime((obj, e) =>
                {
                    if (Song.MP3.CurrentTime == Song.MP3.TotalTime && playlist.SongsList.FirstOrDefault() != null)
                    {
                        playlist.SelectedSong = Utils.GetNewSongPath(playlist.SongsList, PlayType.Next, Song);
                        PlayerHelper(playlist);
                    }

                    Song.TimeText = string.Format("{0} {1}",
                        Song.MP3.CurrentTime.ToString(@"hh\:mm\:ss"),
                        Song.MP3.TotalTime.ToString().Split('.')[0]);

                    if (Song.IsPlaying)
                        Song.PositionValue = Song.MP3.CurrentTime.TotalSeconds;
                });

                Song.ChangePosition();
                Song.Name = Path.GetFileName(Song.MP3.FileName);
                Song.MP3.Volume = Song.Volume / 100;
                Player.Init(Song.MP3);
                Player.Play();
            }
        }
    }
}