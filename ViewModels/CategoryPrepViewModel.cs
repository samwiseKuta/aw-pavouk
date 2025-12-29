using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;
using Services;

namespace ViewModels;


public partial class CategoryPrepViewModel: ViewModelBase
{


    public ObservableCollection<Bracket> CreatedBrackets {get;} = new();
    public HistoryWriter hw;

    public bool BracketsEmpty => CreatedBrackets.Count == 0;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CreatedBrackets))]
    private Tournament _selectedTournament;


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

    public ObservableCollection<Competitor> EditingCompetitorList {get;set;} = new();

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
            if(value is null) return;
            ResetFormInputs();
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

    public CategoryPrepViewModel(HistoryWriter hw){
        this.hw = hw;
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
        SelectedBracket = null;
        ResetFormInputs();

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
        hw.SaveToHistory(SelectedTournament);
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
            hw.SaveToHistory(SelectedTournament);
            ResetFormInputs();
        }
    }

    [RelayCommand]
    public void Begin(){
        Console.WriteLine(EditingBracketType);
    }

    public void FlushAndFillBrackets(List<Bracket> brackets){
        if(brackets is null) return;
        brackets.Sort();
        CreatedBrackets.Clear();
        foreach(Bracket b in brackets){
            CreatedBrackets.Add(b);
        }
    }
    public void FlushAndFillCompetitors(List<Competitor> competitors){
        if(competitors is null) return;
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
