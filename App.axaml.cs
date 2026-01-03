using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Services;
using ViewModels;

namespace App;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {

            // Get an array of plugins to remove
            var dataValidationPluginsToRemove =
                BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

            // remove each entry found
            foreach (var plugin in dataValidationPluginsToRemove)
            {
                BindingPlugins.DataValidators.Remove(plugin);
            }

            MainViewModel mainView;

            HistoryWriterService historyWriter = new HistoryWriterService("history.json");
            WindowOpenerService windowOpener = new WindowOpenerService();
            DialogOpenerService dialogOpener = new DialogOpenerService(){DialogHost=mainView};

            HomeViewModel homeView = new HomeViewModel(historyWriter);
            CategoryPrepViewModel categoryView= new CategoryPrepViewModel(historyWriter);
            DisplayFightsViewModel displayView = new DisplayFightsViewModel();
            ControlFightsViewModel controlView = new ControlFightsViewModel(displayView);


            mainView = new MainViewModel(
                    homeView,
                    categoryView,
                    displayView,
                    controlView,
                    windowOpener
                    );
            dialogOpener

            desktop.MainWindow = new MainView(){
                DataContext = mainView
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
