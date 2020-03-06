using FlashCardGame.UI.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;

using FlashCardGame.Modules.Game;
using FlashCardGame.Core;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace FlashCardGame.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AppCenter.Start("cbab1745-6bff-4d73-96fa-d646517e527b",
                   typeof(Analytics), typeof(Crashes));
            AppCenter.Start("cbab1745-6bff-4d73-96fa-d646517e527b",
                               typeof(Analytics), typeof(Crashes));
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IRandomNumberGenerator, RandomNumberGenerator>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<GameModule>();
        }
    }
}