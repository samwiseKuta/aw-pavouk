using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ViewModels;


public partial class CategoryPrepViewModel: ViewModelBase
{
    [ObservableProperty]
    private bool _showAddMenu=false;

    [RelayCommand]
    public void HideShowMenu(){
        ShowAddMenu = !ShowAddMenu;
    }

}
