using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Models;
using Services;

namespace ViewModels;


public partial class MainViewModel: ViewModelBase
{

    [ObservableProperty]
    private ViewModelBase _currentView;

    [ObservableProperty]
    private DialogViewModel _currentDialog;

    public IWindowService WindowService;


    public HomeViewModel HomeView;
    public CategoryPrepViewModel CategoryPrepView;
    public DisplayFightsViewModel DisplayFightsView;
    public ControlFightsViewModel ControlFightsView;

    public MainViewModel(
            HomeViewModel homeView,
            CategoryPrepViewModel categoryView,
            DisplayFightsViewModel displayFightsView,
            ControlFightsViewModel controlFightsView,
            IWindowService windowService,
            DialogViewModel dialogReference
            )
    {
        this.HomeView = homeView;
        this.CategoryPrepView = categoryView;
        this.DisplayFightsView = displayFightsView;
        this.ControlFightsView = controlFightsView;
        this.WindowService = windowService;
        this.CurrentDialog = dialogReference;

        HomeView.TournamentPicked += OnTournamentCreated;
        CategoryPrepView.GoBack += GoToHome;
        CategoryPrepView.BeginTournament += OnBeginTournament;
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
        CurrentDialog.IsDialogVisible=true;
        CategoryPrepView.SelectedTournament = tournament;
        CategoryPrepView.FlushAndFillBrackets(tournament.Brackets);
        GoToCategories();
    }

    private void OnBeginTournament(Tournament t){
        ControlFightsView.SelectedTournament = t;
        DisplayFightsView.SelectedTournament = t;
        WindowService.OpenNewWindow(DisplayFightsView);
        GoToFights();
    }






}
