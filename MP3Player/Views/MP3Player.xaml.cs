using System.Collections.ObjectModel;
using System.Windows;
using MP3Player.Commands;
using MP3Player.Models;
using MP3Player.ViewModels;
using MP3Player.Interfaces;
using NAudio.Wave;
using static MP3Player.Extensions.PlaylistExtension;
using static MP3Player.Extensions.SongExtension;
using MP3Player.Enums;

namespace MP3Player.Views
{
    public partial class MP3Player : Window
    {
        public MP3Player()
        {
            InitializeComponent();

            var obs = new ObservableCollection<string> { };
            var playlist = new Playlist(obs);
            var getSongs = new MainCommand(x => true, x => playlist.OpenFileDialog());
            var deleteSong = new MainCommand(x => playlist.CanDeleteOrClear(), x => playlist.DeleteFile());
            var clearSongs = new MainCommand(x => playlist.CanDeleteOrClear(), x => playlist.ClearPlaylist());
            
            var playlistViewModel = new PlaylistViewModel(playlist, getSongs, deleteSong, clearSongs);

            var song = new Song();
            var waveOut = new WaveOut();
            var play = new MainCommand(x => song.CanPlayMusic(x as Playlist), x => song.PlayMusic(x as Playlist, waveOut));
            var pause = new MainCommand(x => song.CanPauseSong(), x => song.SongPause(waveOut));
            var playnext = new MainCommand(x => song.CanPlayMusic(x as Playlist), x => song.UniversalPlay(x as Playlist, PlayType.Next, waveOut));
            var playback = new MainCommand(x => song.CanPlayMusic(x as Playlist), x => song.UniversalPlay(x as Playlist, PlayType.Back, waveOut));

            var songViewModel = new SongViewModel(song, waveOut, play, pause, playnext, playback);
            var mainViewModel = new MainViewModel(playlistViewModel, songViewModel);

            DataContext = mainViewModel;
        }
    }
}
