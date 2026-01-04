using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using Services;
using Interfaces;
using System.Threading.Tasks;

namespace ViewModels;


public partial class HomeViewModel: ViewModelBase
{

    public IHistoryService HistoryService;
    public IDialogService DialogService;


    [ObservableProperty]
    private ObservableCollection<Tournament>? _tournamentHistory = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsTournamentSelected))]
    [NotifyCanExecuteChangedFor(nameof(InvokeTournamentPickedCommand))]
    private Tournament? _selectedTournament;

    partial void OnSelectedTournamentChanged(Tournament? value){
        if(value is null) return;
        TournamentSelectedCommand.Execute(value);
    }

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Enter a name")]
    [NotifyCanExecuteChangedFor(nameof(CreateTournamentCommand))]
    private string _tournamentName = String.Empty;
    
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Enter a location")]
    [NotifyCanExecuteChangedFor(nameof(CreateTournamentCommand))]
    private string _tournamentLocation = String.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Enter a date")]
    private DateTime _tournamentDate = DateTime.Now;

    [ObservableProperty]
    private int _tournamentTables = 3;

    [ObservableProperty]
    private bool _showAddMenu=false;

    [ObservableProperty]
    private bool _currentlyEditing;

    public bool IsTournamentSelected => SelectedTournament is not null;


    public event Action<Tournament> TournamentPicked;

    public HomeViewModel(IHistoryService historyService,IDialogService dialogService){
        this.HistoryService = historyService;
        this.DialogService = dialogService;
        RefreshHistoryCommand.Execute(null);
    }

    public  bool CanExecuteCreateTournament => (
            !String.Empty.Equals(TournamentName) &&
            !String.Empty.Equals(TournamentLocation)
            );

    [RelayCommand()]
    public void NewTournament(){
        if(ShowAddMenu is true && CurrentlyEditing is false && String.IsNullOrEmpty(TournamentName)){
            ShowAddMenu = false;
            return;
        }
        SelectedTournament = null;
        CurrentlyEditing = false;
        ShowAddMenu = true;
        ResetFormInputs();
        OnPropertyChanged(nameof(SelectedTournament));
    }
    [RelayCommand(CanExecute=nameof(CanExecuteCreateTournament))]
    public void CreateTournament(){
        Tournament newTournament = new Tournament(){
                Name = TournamentName,
                Location = TournamentLocation,
                Tables = NewTables(),
                Date = TournamentDate
        };
        HistoryService.SaveToHistory(newTournament);
        RefreshHistoryCommand.Execute(null);
        SelectedTournament = newTournament;
        ResetFormInputs();
        InvokeTournamentPickedCommand.Execute(null);
    }
    [RelayCommand]
    public void SaveTournament(){
        SelectedTournament.Name = TournamentName;
        SelectedTournament.Date = TournamentDate;
        SelectedTournament.Location = TournamentLocation;
        SelectedTournament.Tables = NewTables();
        HistoryService.SaveToHistory(SelectedTournament);
        SelectedTournament = null;
        CurrentlyEditing=false;
        ResetFormInputs();
        ShowAddMenu = false;
        RefreshHistoryCommand.Execute(null);
    }

    [RelayCommand(CanExecute=nameof(IsTournamentSelected))]
    public void InvokeTournamentPicked(){
        TournamentPicked(SelectedTournament);

    }
    [RelayCommand]
    public void TournamentSelected(Tournament t){
        ShowAddMenu=true;
        CurrentlyEditing=true;
        SelectedTournament = t;
        TournamentName = t.Name;
        TournamentLocation = t.Location;
        TournamentDate = t.Date;
        TournamentTables = t.Tables?.Count ?? 3;
        OnPropertyChanged(nameof(IsTournamentSelected));
    }

    [RelayCommand]
    public void RefreshHistory(){
        ResetFormInputs();
        SelectedTournament=null;
        TournamentHistory.Clear();
        new List<Tournament>(HistoryService.GetHistory()).ForEach(x => TournamentHistory.Add(x));
    }
    [RelayCommand]
    public async Task DeleteHistoryItem(Tournament t){
        ConfirmDialogViewModel confirmDialog = new ConfirmDialogViewModel(){
            Title=$"Delete {t.Name}?",
            Message="Delete tournament from history?"
        };
        DialogService.OpenNewDialogWindow(confirmDialog);
        await confirmDialog.AwaitResolution();
        if(!confirmDialog.Confirmed) return;
        HistoryService.RemoveFromHistory(t);
        RefreshHistoryCommand.Execute(null);
    }
    public void ResetFormInputs(){
        TournamentName = String.Empty;
        TournamentDate = new DateTime();
        TournamentTables = 3;
        TournamentLocation = String.Empty;
    }
    private List<int> NewTables(){
        List<int> tables = new List<int>();
        for(int i =1; i <= TournamentTables; i++){
            tables.Add(i);
        }
        return tables;
    }
}
