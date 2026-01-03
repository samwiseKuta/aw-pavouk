using Interfaces;
using ViewModels;
using Views;

namespace Services;
public class WindowOpenerService : IWindowService
{

    public void OpenNewWindow(ViewModelBase newWindowDataContext){
        var window = new DisplayFightsView();
        window.DataContext = newWindowDataContext;
        window.Show();
    }
}
