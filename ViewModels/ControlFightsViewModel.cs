using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;
using Services;

namespace ViewModels;


public partial class ControlFightsViewModel: ViewModelBase
{

    public DisplayFightsViewModel DisplayViewModel;


    public event Action GoBack;

    [ObservableProperty]
    private Tournament _selectedTournament;

    public ControlFightsViewModel(DisplayFightsViewModel vm,IWindowService windowService){
        windowService.OpenNewWindow(vm);
    }


    [RelayCommand]
    public void Back(){
        GoBack.Invoke();
    }
}
