using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ViewModels;
public partial class DialogViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isDialogVisible;

    protected TaskCompletionSource closeTask = new TaskCompletionSource();

    public async Task WaitAsync(){
        await closeTask.Task;
    }

    public void Show(){
        if(!closeTask.Task.IsCompleted) return;
        closeTask = new TaskCompletionSource();
        IsDialogVisible= true;
    }


    public void Close(){
        IsDialogVisible=false;
        closeTask.TrySetResult();
    }



}
