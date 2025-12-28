using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;
using Services;

namespace ViewModels;


public partial class CategoryPrepViewModel: ViewModelBase
{


    public ObservableCollection<Bracket> CreatedBrackets {get; set;}
    public HistoryWriter hw;

    public bool BracketsEmpty => CreatedBrackets.Count == 0;

    [ObservableProperty]
    private Tournament _selectedTournament;

    [ObservableProperty]
    private Bracket _selectedBracket;

    [ObservableProperty]
    private Competitor _selectedCompetitor;

    [ObservableProperty]
    private bool _showAddMenu=false;

    public event Action GoBack;

    public CategoryPrepViewModel(HistoryWriter hw){
        this.hw = hw;
        CreatedBrackets = new ObservableCollection<Bracket>(new List<Bracket>());
        CreatedBrackets.CollectionChanged += (_, __) =>
            OnPropertyChanged(nameof(BracketsEmpty));
    }

    [ObservableProperty]
    private string _iconText;
    [ObservableProperty]
    private string _action;

    [RelayCommand]
    public void HideShowMenu(){
        ShowAddMenu = !ShowAddMenu;
    }

    [RelayCommand]
    public void Back(){
        GoBack.Invoke();
    }

    [RelayCommand]
    public void CreateCategory(){
    }

    [RelayCommand]
    public void Begin(){
        TestBracket();
    }
    [RelayCommand]
    public void TestBracket(){
        List<Bracket> newList = new List<Bracket>(CreatedBrackets);
        newList.Add(
                new Bracket(){
                Name="Juniors 95kg",
                Count=new Random().Next()
                });
        newList.Add(
                new Bracket(){
                Name="Juniors 75kg",
                Count=new Random().Next()
                });
        newList.Add(
                new Bracket(){
                Name="Masters 95kg",
                Count=new Random().Next()
                });
        newList.Add(
                new Bracket(){
                Name="Women 65kg",
                Count=new Random().Next()
                });
        newList.Sort();
        CreatedBrackets.Clear();
        foreach(Bracket b in newList){
            CreatedBrackets.Add(b);
        }
    }
    
}
