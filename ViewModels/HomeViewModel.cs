using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using Services;

namespace ViewModels;


public partial class HomeViewModel: ViewModelBase
{

    public HistoryWriter hw;
    public ObservableCollection<Tournament>? TournamentHistory {get;set;}

    [ObservableProperty]
    private Tournament? _selectedTournament;

    partial void OnSelectedTournamentChanged(Tournament? value){
        if(value is null) return;
        SelectedTournament = value;
        TournamentSelectedCommand.Execute(null);
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
    private bool _showAddMenu=false;


    public event Action<Tournament> TournamentCreated;


    public HomeViewModel(HistoryWriter hw){
        this.hw = hw;
        RefreshHistoryCommand.Execute(null);
    }

    public bool CanExecuteCreateTournament => (
            !String.Empty.Equals(TournamentName) &&
            !String.Empty.Equals(TournamentLocation)
            );

    [RelayCommand(CanExecute=nameof(CanExecuteCreateTournament))]
    public void CreateTournament(){
        Tournament newTournament = new Tournament(){
                Name = TournamentName,
                Location = TournamentLocation,
                Date = TournamentDate
        };

        hw.SaveToHistory(newTournament);
        RefreshHistoryCommand.Execute(null);

        TournamentCreated.Invoke(newTournament);
    }
    [RelayCommand]
    public void TournamentSelected(){
        Tournament temp = SelectedTournament;
        SelectedTournament = null;
        TournamentCreated.Invoke(temp);
    }

    [RelayCommand]
    public void HideShowMenu(){
        ShowAddMenu = !ShowAddMenu;
    }

    [RelayCommand]
    public void RefreshHistory(string? path = null){

        TournamentHistory = new ObservableCollection<Tournament>(new List<Tournament>(hw.History));
    }
}
