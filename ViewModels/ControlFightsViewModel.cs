using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Interfaces;
using Models;
using Services;

namespace ViewModels;


public partial class ControlFightsViewModel: ViewModelBase
{


    [ObservableProperty]
    private ObservableCollection<StartedCategory> _startedCategories= new ();

    [ObservableProperty]
    private StartedCategory _selectedCategory;

    [ObservableProperty]
    private int _selectedStartedTable;

    [NotifyCanExecuteChangedFor(nameof(StartNewCategoryCommand))]
    [NotifyPropertyChangedFor(nameof(CanExecuteStartNewCategory))]
    [ObservableProperty]
    private ObservableCollection<int> _remainingTables = new (new List<int>());

    [NotifyCanExecuteChangedFor(nameof(StartNewCategoryCommand))]
    [NotifyPropertyChangedFor(nameof(CanExecuteStartNewCategory))]
    [ObservableProperty]
    private ObservableCollection<Bracket> _remainingCategories = new (new List<Bracket>());

    public bool CanExecuteStartNewCategory => (
            RemainingCategories.Count > 0 &&
            RemainingTables.Count > 0
            );

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

    [RelayCommand(CanExecute=(nameof(CanExecuteStartNewCategory)))]
    public async Task StartNewCategory(){
        StartCategoryDialogViewModel dialog = new StartCategoryDialogViewModel(){
            Title="Starting category",
            Message="Select which category on which table",
            TableOptions = new List<int>(RemainingTables),
            BracketOptions = new List<Bracket>(RemainingCategories),
        };

        DialogOpener.OpenNewDialogWindow(dialog);
        await dialog.AwaitResolution();
        if(!dialog.Confirmed) return;

        RemainingTables.Remove(dialog.SelectedTable??100);
        RemainingCategories.Remove(dialog.SelectedBracket);
        StartedCategory newCategory = new StartedCategory(dialog.SelectedTable??100,dialog.SelectedBracket);
        StartedCategories.Add(newCategory);
        SelectedCategory=newCategory;

        newCategory.Bracket.GenerateMatchesTest();
        Console.WriteLine(newCategory.Bracket.ToTreeString());
        Console.WriteLine(newCategory.Bracket.ToTreeNumberString());
    }

}
