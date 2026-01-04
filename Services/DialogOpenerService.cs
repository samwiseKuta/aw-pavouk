using System;
using System.Threading.Tasks;
using Interfaces;
using ViewModels;

namespace Services;

public class DialogOpenerService : IDialogService
{
    public IDialogHost DialogHost {get;set;}

    public void OpenNewDialogWindow(DialogViewModel dialog)
    {
        DialogHost.Dialog = dialog;
        DialogHost.Dialog.Show();
    }

    public async Task OpenNewDialogWindowAsync(DialogViewModel dialog)
    {
        DialogHost.Dialog = dialog;
        DialogHost.Dialog.Show();
        await DialogHost.Dialog.AwaitResolution();
    }
}
