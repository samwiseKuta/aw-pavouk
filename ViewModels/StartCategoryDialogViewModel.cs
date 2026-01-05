using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    [NotifyCanExecuteChangedFor(nameof(ConfirmCommand))]
    [NotifyPropertyChangedFor(nameof(CanExecuteConfirm))]
    [ObservableProperty]
    private List<Bracket> _bracketOptions = new();
    partial void OnBracketOptionsChanged(List<Bracket> value){
        BracketOptions = value;
        SelectedBracket = value[0];
    }

    [NotifyCanExecuteChangedFor(nameof(ConfirmCommand))]
    [NotifyPropertyChangedFor(nameof(CanExecuteConfirm))]
    [ObservableProperty]
    private Bracket? _selectedBracket = null;

    [NotifyCanExecuteChangedFor(nameof(ConfirmCommand))]
    [NotifyPropertyChangedFor(nameof(CanExecuteConfirm))]
    [ObservableProperty]
    private List<int> _tableOptions = new();
    partial void OnTableOptionsChanged(List<int> value){
        TableOptions = value;
        SelectedTable = TableOptions[0];
    }

    [NotifyCanExecuteChangedFor(nameof(ConfirmCommand))]
    [NotifyPropertyChangedFor(nameof(CanExecuteConfirm))]
    [ObservableProperty]
    private int? _selectedTable;



    public bool CanExecuteConfirm => 
        (SelectedBracket is not null &&
        SelectedTable is not null);




    [ObservableProperty]
    private bool _confirmed;

    [RelayCommand(CanExecute = (nameof(CanExecuteConfirm)))]
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
