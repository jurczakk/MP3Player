using System.Windows;
using MP3Player.Config;

namespace MP3Player.Views
{
    public partial class MP3Player : Window
    {
        public MP3Player()
        {
            InitializeComponent();
            // DataContext = BootStraper.ResolveConfig();
            DataContext = Container.GetMainViewModel();
        }
    }
}
