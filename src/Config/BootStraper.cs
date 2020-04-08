using Autofac;
using MP3Player.Interfaces.ViewModels;

namespace MP3Player.Config
{
    public static class BootStraper
    {
        public static IMainViewModel ResolveConfig()
        {
            var container = Container.Configure();
            using var scope = container.BeginLifetimeScope();
            return scope.Resolve<IMainViewModel>();
        }
    }
}
