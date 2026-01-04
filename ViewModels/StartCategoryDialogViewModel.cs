using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;

namespace ViewModels;

public partial class StartCategoryDialogViewModel : DialogViewModel
{

    [ObservableProperty]
    private string _title = "Starting category";
    [ObservableProperty]
    private string _message= "Are you sure?";
    [ObservableProperty]
    private string _cancelText= "cancel";
    [ObservableProperty]
    private string _confirmText  = "Start";
    [ObservableProperty]
    private string _iconText = "\xE4D0";

    [ObservableProperty]
    private Bracket _startingBracket;

    [ObservableProperty]
    private int _startingTable;







    [ObservableProperty]
    private bool _confirmed;

    [RelayCommand]
    public void Confirm(){
        Confirmed=true;
        Close();
    }
    

    [RelayCommand]
    public void Cancel(){
        Confirmed = false;
        Close();
    }


}
