using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ViewModels;
public partial class DialogViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isDialogVisible;

    protected TaskCompletionSource closeTask = new TaskCompletionSource();

    public async Task AwaitResolution(){
        await closeTask.Task;
    }

    public void Show(){
        if(!closeTask.Task.IsCompleted){
            closeTask = new TaskCompletionSource();
        }
        IsDialogVisible= true;
    }


    public void Close(){
        IsDialogVisible=false;
        closeTask.TrySetResult();
    }



}
