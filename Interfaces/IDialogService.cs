using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using ViewModels;

namespace Interfaces;

public interface IDialogService
{
    public void OpenNewDialogWindow(DialogViewModel dialog);
    public Task OpenNewDialogWindowAsync(DialogViewModel dialog);
}
