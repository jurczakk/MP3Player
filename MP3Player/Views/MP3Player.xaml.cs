using System.Windows;
using Autofac;
using MP3Player.Interfaces;
using MP3Player.Models;

namespace MP3Player.Views
{
    public partial class MP3Player : Window
    {
        public MP3Player()
        {
            InitializeComponent();
            var container = Config.Container.Configure();
            using (var scope = container.BeginLifetimeScope())
                DataContext = scope.Resolve<IMainViewModel>();
        }
    }
}
