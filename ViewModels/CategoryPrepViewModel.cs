using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Interfaces;
using Models;
using Services;

namespace ViewModels;


public partial class CategoryPrepViewModel: ViewModelBase
{

    public IHistoryService historyWriter;
    public IDialogService dialogOpener;

    [ObservableProperty]
    private ObservableCollection<Bracket> _createdBrackets  = new();

    public bool BracketsEmpty => CreatedBrackets.Count == 0;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CreatedBrackets))]
    private Tournament _selectedTournament;

    [ObservableProperty]
    private bool _currentlyEditing;

    [ObservableProperty]
    private bool _menuVisible = false;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanExecuteCreate))]
    [NotifyCanExecuteChangedFor(nameof(CreateCategoryCommand))]
    private string _editingBracketName = "";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanExecuteCreate))]
    [NotifyCanExecuteChangedFor(nameof(CreateCategoryCommand))]
    private string _editingBracketType = "Double Elimination";

    public List<string> BracketTypes {get;} = new List<string>(){
        "Single Elimination",
        "Double Elimination",
    };

    [ObservableProperty]
    private ObservableCollection<Competitor> _editingCompetitorList = new();

    [ObservableProperty]
    private string _editingCompetitorName;
    [ObservableProperty]
    private float _editingCompetitorWeight;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsBracketSelected))]
    [NotifyPropertyChangedFor(nameof(StateText))]
    [NotifyPropertyChangedFor(nameof(CanExecuteCreate))]
    [NotifyCanExecuteChangedFor(nameof(CreateCategoryCommand))]
    [NotifyCanExecuteChangedFor(nameof(SaveCategoryCommand))]
    private Bracket _selectedBracket;
    partial void OnSelectedBracketChanged(Bracket value)
        {
            if(value is null){
                return;
            }
            ResetFormInputs();
            CurrentlyEditing = true;
            MenuVisible = true;
            EditingBracketName = SelectedBracket.Name;
            EditingBracketType = SelectedBracket.Elimination ?? "Double Elimination";
            FlushAndFillCompetitors(SelectedBracket.Competitors);
        }

    public bool IsBracketSelected => SelectedBracket is not null;
    public bool CanExecuteCreate=> 
        (
         !IsBracketSelected && 
         !string.IsNullOrWhiteSpace(EditingBracketName)
            );

    public string StateText => IsBracketSelected ? "Editing Category" : "New Category";

    [ObservableProperty]
    private Competitor _selectedCompetitor;

    public event Action GoBack;

    public event Action<Tournament> BeginTournament;

    public CategoryPrepViewModel(IHistoryService historyService,IDialogService dialogService){
        this.historyWriter = historyService;
        CreatedBrackets.CollectionChanged +=  (_,_) =>
            OnPropertyChanged(nameof(BracketsEmpty));
    }

    [ObservableProperty]
    private string _action;

    [RelayCommand]
    public void Back(){
        GoBack.Invoke();
    }
    [RelayCommand]
    public void AddCompetitor(){
        EditingCompetitorList.Add(new Competitor(){
                Name = EditingCompetitorName,
                Weight = EditingCompetitorWeight
                });
        EditingCompetitorName = String.Empty;
        EditingCompetitorWeight = 80;
        
    }
    [RelayCommand]
    public void NewCategory(){
        if(MenuVisible && CurrentlyEditing is false){
            MenuVisible = false;
            return;
        }
        MenuVisible = true;
        SelectedBracket = null;
        ResetFormInputs();
        CurrentlyEditing = false;

    }
    [RelayCommand(CanExecute =nameof(CanExecuteCreate))]
    public void CreateCategory(){
        Bracket newBracket = new Bracket(){
            Name = EditingBracketName,
            Competitors = new List<Competitor>(EditingCompetitorList),
            Elimination = EditingBracketType
        };
        List<Bracket> newList = new List<Bracket>(CreatedBrackets);
        newList.Add(newBracket);
        FlushAndFillBrackets(newList);
        SelectedTournament.Brackets.Add(newBracket);
        historyWriter.SaveToHistory(SelectedTournament);
        ResetFormInputs();
    }

    [RelayCommand(CanExecute =nameof(IsBracketSelected))]
    public void SaveCategory(){
        SelectedBracket = null;
        List<Bracket> newList = new List<Bracket>(CreatedBrackets);
        int i = newList.FindIndex(x => x.Name.Equals(EditingBracketName));
        if(i == -1) {
            Console.WriteLine("Something went wrong here:",EditingBracketName);
        }
        else{
            Bracket changedBracket = CreatedBrackets[i];
            changedBracket.Name = EditingBracketName;
            changedBracket.Competitors = new List<Competitor>(EditingCompetitorList);
            changedBracket.Elimination = EditingBracketType;
            CreatedBrackets[i] = changedBracket;
            SelectedTournament.Brackets[i] = changedBracket;
            historyWriter.SaveToHistory(SelectedTournament);
            ResetFormInputs();
        }
    }

    [RelayCommand]
    public void Begin(){
        BeginTournament.Invoke(SelectedTournament);
    }
    [RelayCommand]
    public void DeleteCategoryItem(Bracket b){
        SelectedTournament.Brackets.Remove(b);
        historyWriter.SaveToHistory(SelectedTournament);

        FlushAndFillBrackets(SelectedTournament.Brackets);

    }

    [RelayCommand]
    public void DeleteCompetitorItem(Competitor c){
        EditingCompetitorList.Remove(c);
    }

    public void FlushAndFillBrackets(List<Bracket>? brackets){
        if(brackets is null) return;
        brackets.Sort();
        CreatedBrackets.Clear();
        foreach(Bracket b in brackets){
            CreatedBrackets.Add(b);
        }
        ResetFormInputs();
    }
    public void FlushAndFillCompetitors(List<Competitor> competitors){
        if(competitors is null){
            return;
        }
        competitors.Sort();
        EditingCompetitorList.Clear();
        foreach(Competitor c in competitors){
            EditingCompetitorList.Add(c);
        }
    }

    public void ResetFormInputs(){
        EditingBracketName = String.Empty;
        EditingCompetitorList.Clear();  
        EditingCompetitorName = String.Empty;
        EditingCompetitorWeight = 80;
        EditingBracketType = "Double Elimination";
    }

}
