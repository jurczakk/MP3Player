using System.Linq;
using System.Windows;
using Autofac;
using MP3Player.Config;
using MP3Player.Interfaces;
using MP3Player.ViewModels;

namespace MP3Player.Views
{
    public partial class MP3Player : Window
    {
        private readonly IMainViewModel MainViewModel;
        public MP3Player()
        {
            InitializeComponent();
            using var scope = Container.Configure().BeginLifetimeScope();
            MainViewModel = scope.Resolve<IMainViewModel>();
            DataContext = MainViewModel;
        }

        private void ListView_Drop(object sender, DragEventArgs e)
        {
            var data = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (data != null && data.Count() > 0)
                ((PlaylistViewModel)MainViewModel.PlaylistViewModel).AddPaths(data);
        }
    }
}
