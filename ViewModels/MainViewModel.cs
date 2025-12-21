using CommunityToolkit.Mvvm.ComponentModel;

namespace ViewModels;


public partial class MainViewModel: ViewModelBase
{

    [ObservableProperty]
    private ViewModelBase _currentView;

    private readonly HomeViewModel _homeView = new();

    private readonly CategoryPrepViewModel _categoryPrepView = new();

    public MainViewModel(){
        CurrentView = _homeView;
    }


}
