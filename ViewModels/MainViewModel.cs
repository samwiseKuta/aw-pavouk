using CommunityToolkit.Mvvm.ComponentModel;
using Models;

namespace ViewModels;


public partial class MainViewModel: ViewModelBase
{

    [ObservableProperty]
    private ViewModelBase _currentView;


    public HomeViewModel HomeView;
    public CategoryPrepViewModel CategoryPrepView;

    public MainViewModel(
            HomeViewModel homeView,
            CategoryPrepViewModel categoryView
            )
    {
        this.HomeView = homeView;
        this.CategoryPrepView = categoryView;

        HomeView.TournamentCreated += OnTournamentCreated;

        CurrentView = HomeView;
        CategoryPrepView.GoBack += GoToHome;
    }

    private void GoToHome(){
        CurrentView = HomeView;
    }

    private void GoToCategories(){
        CurrentView = CategoryPrepView;
    }

    private void OnTournamentCreated(Tournament tournament){
        CategoryPrepView.SelectedTournament = tournament;
        GoToCategories();
    }



}
