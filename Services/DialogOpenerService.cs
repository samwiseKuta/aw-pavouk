using ViewModels;

namespace Services;

public class DialogOpenerService : IDialogService
{
    private DialogViewModel DialogReference;

    public void OpenNewDialogWindow(DialogViewModel dialog)
    {
        DialogReference = dialog;
        DialogReference.Show();
    }

    public DialogOpenerService(DialogViewModel dialogReference){
        this.DialogReference = dialogReference;
    }
}
