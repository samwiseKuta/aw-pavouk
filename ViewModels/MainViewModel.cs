using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App.ViewModels;


public partial class MainViewModel: ViewModelBase
{
    [ObservableProperty]
    private bool _showAddMenu=false;

    [RelayCommand]
    public void HideShowMenu(){
        ShowAddMenu = !ShowAddMenu;
    }

}
