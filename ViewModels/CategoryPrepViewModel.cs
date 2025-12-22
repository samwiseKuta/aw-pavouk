using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;

namespace ViewModels;


public partial class CategoryPrepViewModel: ViewModelBase
{


    public ObservableCollection<Bracket> CreatedBrackets {get; set;}
    
    public bool BracketsEmpty => CreatedBrackets.Count == 0;

    [ObservableProperty]
    private Tournament _selectedTournament;

    [ObservableProperty]
    private Bracket _selectedBracket;

    [ObservableProperty]
    private bool _showAddMenu=false;

    public event Action GoBack;

    public CategoryPrepViewModel(){
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
        IconText="&#xE248;";
    }

    [RelayCommand]
    public void Begin(){
        TestBracketCommand.Execute(null);
    }
    [RelayCommand]
    public void TestBracket(){
        CreatedBrackets.Add(
                new Bracket(){
                Name="Juniors 95kg",
                Count=new Random().Next()
                });
        CreatedBrackets.Add(
                new Bracket(){
                Name="Juniors 75kg",
                Count=new Random().Next()
                });
        CreatedBrackets.Add(
                new Bracket(){
                Name="Masters 95kg",
                Count=new Random().Next()
                });
        CreatedBrackets.Add(
                new Bracket(){
                Name="Women 65kg",
                Count=new Random().Next()
                });
        List<Bracket> list = new List<Bracket>(CreatedBrackets);
        list.Sort();
        CreatedBrackets=new ObservableCollection<Bracket>(list);
    }
    
}
