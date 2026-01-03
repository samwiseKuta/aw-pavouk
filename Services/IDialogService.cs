using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using ViewModels;

namespace Services;

public interface IDialogService
{
    public void OpenNewDialogWindow(DialogViewModel dialog);
}
