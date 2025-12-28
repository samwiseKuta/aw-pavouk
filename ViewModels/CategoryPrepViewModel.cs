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


    public ObservableCollection<Bracket> CreatedBrackets {get; set;} = new();
    public HistoryWriter hw;

    public bool BracketsEmpty => CreatedBrackets.Count == 0;

    [ObservableProperty]
    private bool _editCreateMode;

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
    private Bracket.Type _editingBracketType;

    [ObservableProperty]
    private List<Competitor> _editingBracketList = new();

    [ObservableProperty]
    private string _editingCompetitorName;
    [ObservableProperty]
    private float _editingCompetitorWeight;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsBracketSelected))]
    [NotifyPropertyChangedFor(nameof(StateText))]
    [NotifyPropertyChangedFor(nameof(CanExecuteCreate))]
    private Bracket _selectedBracket;
    partial void OnSelectedBracketChanged(Bracket value)
        {
            SelectedBracket = value;
            EditingBracketName = SelectedBracket.Name;
            EditingBracketType = Bracket.Type.DoubleElimination;
            EditingBracketList = SelectedBracket.Competitors;
            EditingCompetitorName = String.Empty;
            EditingCompetitorWeight = 80;
        }

    public bool IsBracketSelected => SelectedBracket is not null;
    public bool CanExecuteCreate=> 
        (
         !IsBracketSelected && 
         EditingBracketName is not null &&
         EditingBracketList is not null
            );

    public string StateText => IsBracketSelected ? "Editing Category" : "New Category";

    [ObservableProperty]
    private Competitor _selectedCompetitor;

    public event Action GoBack;

    public CategoryPrepViewModel(HistoryWriter hw){
        this.hw = hw;
        //CreatedBrackets = new ();
        CreatedBrackets.CollectionChanged += (_, __) =>
            OnPropertyChanged(nameof(BracketsEmpty));
    }

    [ObservableProperty]
    private string _action;

    [RelayCommand]
    public void Back(){
        GoBack.Invoke();
    }
    [RelayCommand]
    public void NewCategory(){
        SelectedBracket = null;
        EditingBracketName = String.Empty;
        EditingBracketType = Bracket.Type.DoubleElimination;
        EditingCompetitorName = String.Empty;
        EditingCompetitorWeight = 80;

    }
    [RelayCommand(CanExecute =nameof(CanExecuteCreate))]
    public void CreateCategory(){
        Bracket newBracket = new Bracket(){
            Name = EditingBracketName,
            Competitors = EditingBracketList
        };
        List<Bracket> newList = new List<Bracket>(CreatedBrackets);
        newList.Add(newBracket);
        newList.Sort();
        CreatedBrackets.Clear();
        foreach(Bracket b in newList){
            Console.WriteLine(b);
            CreatedBrackets.Add(b);
        }
        //SelectedTournament.Brackets.Add(newBracket);
        //hw.SaveToHistory(SelectedTournament);
    }

    [RelayCommand(CanExecute =nameof(CanExecuteCreate))]
    public void SaveCategory(){
        List<Bracket> newList = new List<Bracket>(CreatedBrackets);
        newList.Add(
                new Bracket(){
                Name="Juniors 95kg",
                NodeCount=new Random().Next()
                });
        newList.Add(
                new Bracket(){
                Name="Juniors 75kg",
                NodeCount=new Random().Next()
                });
        newList.Add(
                new Bracket(){
                Name="Masters 95kg",
                NodeCount=new Random().Next()
                });
        newList.Add(
                new Bracket(){
                Name="Women 65kg",
                NodeCount=new Random().Next()
                });
        newList.Sort();
        CreatedBrackets.Clear();
        foreach(Bracket b in newList){
            CreatedBrackets.Add(b);
        }

    }

    [RelayCommand]
    public void Begin(){
        Console.WriteLine(EditingBracketName);
        Console.WriteLine(EditingBracketList);
        Console.WriteLine(CanExecuteCreate);
        Console.WriteLine(CreatedBrackets.Count);
        Console.WriteLine(BracketsEmpty);

        //CreatedBrackets.Clear();
    }
}
