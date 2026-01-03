using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Interfaces;
using Models;
using Services;

namespace ViewModels;


public partial class ControlFightsViewModel: ViewModelBase
{


    public class StartedCategory{
        
        public int Table {get;set;}

        public Bracket Bracket{get;set;}

        public StartedCategory(int table, Bracket bracket){
            Table = table;
            Bracket = bracket;
        }
    }

    [ObservableProperty]
    private ObservableCollection<StartedCategory> _startedCategories= new ();

    [ObservableProperty]
    private StartedCategory _selectedCategory;

    [ObservableProperty]
    private int _selectedStartedTable;

    [ObservableProperty]
    private ObservableCollection<int> _remainingTables = new (new List<int>());

    [ObservableProperty]
    private ObservableCollection<Bracket> _remainingCategories = new (new List<Bracket>());

    public DisplayFightsViewModel DisplayViewModel;
    public IDialogService DialogOpener;
    public IHistoryService HistoryWriter;


    public event Action GoBack;

    [ObservableProperty]
    private Tournament _selectedTournament;
    partial void OnSelectedTournamentChanged(Tournament t)
        {
            if(t is null){
                return;
            }
            RemainingTables.Clear();
            RemainingCategories.Clear();
            StartedCategories.Clear();

            t.Tables.ForEach(table => RemainingTables.Add(table));
            t.Brackets.ForEach(bracket => RemainingCategories.Add(bracket));

            StartCategory(3,RemainingCategories[0]);
            
        }

    public ControlFightsViewModel(
            DisplayFightsViewModel vm,
            IHistoryService historyService,
            IDialogService dialogService)
    {
        this.DisplayViewModel = vm;
        this.DialogOpener = dialogService;
        this.HistoryWriter = historyService;


    }


    [RelayCommand]
    public void Back(){
        GoBack.Invoke();
    }

    private void StartCategory(int table,Bracket b){
        RemainingTables.Remove(table);
        RemainingCategories.Remove(b);
        StartedCategories.Add(new StartedCategory(table,b));
    }
}
