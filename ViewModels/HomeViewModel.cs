using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;

namespace ViewModels;


public partial class HomeViewModel: ViewModelBase
{
    public ObservableCollection<Tournament>? TournamentHistory {get;set;}

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


    public HomeViewModel(){
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
        TournamentHistory.Add(newTournament);

        File.WriteAllText("history.json",JsonSerializer.Serialize(TournamentHistory));

        TournamentCreated.Invoke(newTournament);
    }

    [RelayCommand]
    public void HideShowMenu(){
        ShowAddMenu = !ShowAddMenu;
    }

    [RelayCommand]
    public void RefreshHistory(string? path = null){

        Tournament[] history = 
            JsonSerializer.Deserialize<Tournament[]>(File.ReadAllText(path ?? "history.json"));
        TournamentHistory=new ObservableCollection<Tournament>(
                new List<Tournament>(history)
                );
        TournamentHistory = new ObservableCollection<Tournament>(new List<Tournament>(history));
    }
}
