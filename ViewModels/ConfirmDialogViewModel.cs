using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ViewModels;

public partial class ConfirmDialogViewModel : DialogViewModel
{

    [ObservableProperty]
    private string _title = "Confirmation Required";
    [ObservableProperty]
    private string _message= "Confirm your changes?";
    [ObservableProperty]
    private string _cancelText= "Cancel";
    [ObservableProperty]
    private string _confirmText  = "Confirm";
    [ObservableProperty]
    private string _iconText = "\xe4e0";




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
