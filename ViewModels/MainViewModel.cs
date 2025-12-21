using CommunityToolkit.Mvvm.ComponentModel;
using Models;

namespace ViewModels;


public partial class MainViewModel: ViewModelBase
{

    [ObservableProperty]
    private ViewModelBase _currentView;


    private readonly HomeViewModel _homeView;
    private readonly CategoryPrepViewModel _categoryPrepView = new();

    public MainViewModel(){
        _homeView = new();
        _homeView.TournamentCreated += OnTournamentCreated;

        CurrentView = _homeView;
    }

    private void OnTournamentCreated(Tournament tournament){
        CurrentView = _categoryPrepView;
    }


}
