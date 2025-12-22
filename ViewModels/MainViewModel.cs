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
        _categoryPrepView.GoBack +=GoBackFromCategories;
        _categoryPrepView.SelectedTournament = tournament;
        _homeView.SelectedTournament = null;
        CurrentView = _categoryPrepView;
    }

    private void GoBackFromCategories(){
        CurrentView = _homeView;
    }


}
