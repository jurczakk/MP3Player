using Autofac;
using MP3Player.Models;
using MP3Player.ViewModels;
using MP3Player.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MP3Player.Config
{
    public static class Container
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.Register(c => new ObservableCollection<ISongData> { }).As<IList<ISongData>>();
            builder.RegisterType<SongData>().As<ISongData>();
            builder.RegisterType<Song>().As<ISong>();
            builder.RegisterType<Playlist>().As<IPlaylist>();
            builder.RegisterType<SongViewModel>().As<ISongViewModel>();
            builder.RegisterType<PlaylistViewModel>().As<IPlaylistViewModel>();
            builder.RegisterType<MainViewModel>().As<IMainViewModel>();

            return builder.Build();
        }
    }
}
