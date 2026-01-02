using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Models;

namespace ViewModels;


public partial class MainViewModel: ViewModelBase
{

    [ObservableProperty]
    private ViewModelBase _currentView;


    public HomeViewModel HomeView;
    public CategoryPrepViewModel CategoryPrepView;
    public DisplayFightsViewModel DisplayFightsView;
    public ControlFightsViewModel ControlFightsView;

    public MainViewModel(
            HomeViewModel homeView,
            CategoryPrepViewModel categoryView,
            DisplayFightsViewModel displayFightsView,
            ControlFightsViewModel controlFightsView
            )
    {
        this.HomeView = homeView;
        this.CategoryPrepView = categoryView;
        this.DisplayFightsView = displayFightsView;
        this.ControlFightsView = controlFightsView;

        HomeView.TournamentPicked += OnTournamentCreated;
        CategoryPrepView.GoBack += GoToHome;
        CategoryPrepView.BeginTournament += OnBeginTournament;;
        ControlFightsView.GoBack += GoToCategories;

        CurrentView = HomeView;
    }

    private void GoToHome(){
        CurrentView = HomeView;
    }

    private void GoToCategories(){
        CurrentView = CategoryPrepView;
    }
    private void GoToFights(){
        CurrentView = ControlFightsView;
    }

    private void OnTournamentCreated(Tournament tournament){
        CategoryPrepView.SelectedTournament = tournament;
        CategoryPrepView.FlushAndFillBrackets(tournament.Brackets);
        GoToCategories();
    }

    private void OnBeginTournament(Tournament t){
        ControlFightsView.SelectedTournament = t;
        GoToFights();
    }






}
