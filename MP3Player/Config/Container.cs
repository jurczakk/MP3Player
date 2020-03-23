using Autofac;
using MP3Player.Commands;
using MP3Player.Interfaces;
using MP3Player.Models;
using MP3Player.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MP3Player.Config
{
    public static class Container
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            // PlaylistViewModel
            builder.RegisterType<ObservableCollection<string>>().As<IList<string>>();
            builder.RegisterType<Playlist>().As<IPlaylist>();
            builder.RegisterType<DeleteSongCommand>().As<IDeleteSongCommand>();
            builder.RegisterType<ClearPlaylistCommand>().As<IClearPlaylistCommand>();
            builder.RegisterType<AddSongsCommand>().As<IAddSongsCommand>();


            //SongViewModel

            builder.RegisterType<Song>().As<ISong>();
            
            builder.RegisterType<MainCommand>().As<ICommand>();

            builder.RegisterType<PlaylistViewModel>().As<IPlaylistViewModel>();
            builder.RegisterType<SongViewModel>().As<ISongViewModel>();
            builder.RegisterType<MainViewModel>().As<IMainViewModel>();


            return builder.Build();
        }

        //public static IMainViewModel GetMainViewModel()
        //{
        //    var collection = new ObservableCollection<string> { };
        //    var playlist = new Playlist(collection);
        //    var getSongs = new MainCommand(x => true, x => playlist.OpenFileDialog());
        //    var deleteSong = new MainCommand(x => playlist.CanDeleteOrClear(), x => playlist.DeleteFile());
        //    var clearSongs = new MainCommand(x => playlist.CanDeleteOrClear(), x => playlist.ClearPlaylist());

        //    var playlistViewModel = new PlaylistViewModel(playlist, getSongs, deleteSong, clearSongs);

        //    var song = new Song();
        //    var waveOut = new WaveOut();
        //    var play = new MainCommand(x => song.CanPlayMusic(x as IPlaylist), x => song.PlayMusic(x as Playlist, waveOut));
        //    var pause = new MainCommand(x => song.CanPauseSong(), x => song.SongPause(waveOut));
        //    var playnext = new MainCommand(x => song.CanPlayMusic(x as IPlaylist), x => song.UniversalPlay(x as IPlaylist, PlayType.Next, waveOut));
        //    var playback = new MainCommand(x => song.CanPlayMusic(x as IPlaylist), x => song.UniversalPlay(x as IPlaylist, PlayType.Back, waveOut));

        //    var songViewModel = new SongViewModel(song, waveOut, play, pause, playnext, playback);

        //    return new MainViewModel(playlistViewModel, songViewModel);
        //}
    }
}
