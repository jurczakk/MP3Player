using System.Collections.ObjectModel;
using NAudio.Wave;
using MP3Player.Commands;
using MP3Player.Enums;
using MP3Player.Interfaces;
using MP3Player.ViewModels;
using static MP3Player.Extensions.PlaylistExtension;
using static MP3Player.Extensions.SongExtension;

namespace MP3Player.Models
{
    public static class Config
    {
        public static IMainViewModel GetMainViewModel()
        {
            var obs = new ObservableCollection<string> { };
            var playlist = new Playlist(obs);
            var getSongs = new MainCommand(x => true, x => playlist.OpenFileDialog());
            var deleteSong = new MainCommand(x => playlist.CanDeleteOrClear(), x => playlist.DeleteFile());
            var clearSongs = new MainCommand(x => playlist.CanDeleteOrClear(), x => playlist.ClearPlaylist());

            var playlistViewModel = new PlaylistViewModel(playlist, getSongs, deleteSong, clearSongs);

            var song = new Song();
            var waveOut = new WaveOut();
            var play = new MainCommand(x => song.CanPlayMusic(x as IPlaylist), x => song.PlayMusic(x as Playlist, waveOut));
            var pause = new MainCommand(x => song.CanPauseSong(), x => song.SongPause(waveOut));
            var playnext = new MainCommand(x => song.CanPlayMusic(x as IPlaylist), x => song.UniversalPlay(x as IPlaylist, PlayType.Next, waveOut));
            var playback = new MainCommand(x => song.CanPlayMusic(x as IPlaylist), x => song.UniversalPlay(x as IPlaylist, PlayType.Back, waveOut));

            var songViewModel = new SongViewModel(song, waveOut, play, pause, playnext, playback);
            
            return new MainViewModel(playlistViewModel, songViewModel);
        }
    }
}
