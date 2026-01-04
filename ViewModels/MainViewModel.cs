using CommunityToolkit.Mvvm.ComponentModel;
using Interfaces;
using Models;

namespace ViewModels;


public partial class MainViewModel: ViewModelBase, IDialogHost
{

    [ObservableProperty]
    private ViewModelBase _currentView;

    [NotifyPropertyChangedFor(nameof(Dialog))]
    [ObservableProperty]
    private DialogViewModel _currentDialog = new();
    public DialogViewModel Dialog { get => CurrentDialog; set => CurrentDialog = value; }

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
            IWindowService windowService
            )
    {
        this.HomeView = homeView;
        this.CategoryPrepView = categoryView;
        this.DisplayFightsView = displayFightsView;
        this.ControlFightsView = controlFightsView;
        this.WindowService = windowService;

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
