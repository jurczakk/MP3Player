using Autofac;
using MP3Player.Models;
using MP3Player.ViewModels;
using MP3Player.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MP3Player.Config
{
    public static class Container
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            
            //builder.RegisterType<ObservableCollection<string>>()
            //    .InstancePerLifetimeScope().AsSelf().As<IList<string>>();

            builder.RegisterType<ObservableCollection<Tuple<int, string>>>()
                .InstancePerLifetimeScope().AsSelf().As < IList<Tuple<int, string>>>();
              
            builder.RegisterType<Song>().As<ISong>();
            builder.RegisterType<Playlist>().As<IPlaylist>();
            builder.RegisterType<SongViewModel>().As<ISongViewModel>();
            builder.RegisterType<PlaylistViewModel>().As<IPlaylistViewModel>();
            builder.RegisterType<MainViewModel>().As<IMainViewModel>();

            return builder.Build();
        }

        public static IMainViewModel GetMainViewModel()
        {
            using var scope = Configure().BeginLifetimeScope();
            return scope.Resolve<IMainViewModel>();
        }
    }
}
