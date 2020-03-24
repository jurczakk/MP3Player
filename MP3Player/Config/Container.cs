using Autofac;
using MP3Player.Commands;
using MP3Player.Interfaces.Commands;
using MP3Player.Interfaces.Models;
using MP3Player.Interfaces.ViewModels;
using MP3Player.Models;
using MP3Player.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MP3Player.Config
{
    public static class Container
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ObservableCollection<string>>().As<IList<string>>();
            builder.RegisterType<Playlist>().As<IPlaylist>();
            builder.RegisterType<DeleteSongCommand>().As<IDeleteSongCommand>();
            builder.RegisterType<ClearPlaylistCommand>().As<IClearPlaylistCommand>();
            builder.RegisterType<AddSongsCommand>().As<IAddSongsCommand>();

            builder.RegisterType<Song>().As<ISong>();
            builder.RegisterType<PlayCommand>().As<IPlayCommand>();
            builder.RegisterType<PauseCommand>().As<IPauseCommand>();
            builder.RegisterType<PlayNextCommand>().As<IPlayNextCommand>();
            builder.RegisterType<PlayBackCommand>().As<IPlayBackCommand>();

            builder.RegisterType<PlaylistViewModel>().As<IPlaylistViewModel>();
            builder.RegisterType<SongViewModel>().As<ISongViewModel>();
            builder.RegisterType<MainViewModel>().As<IMainViewModel>();

            return builder.Build();
        }
    }
}
