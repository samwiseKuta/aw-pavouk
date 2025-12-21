using ViewModels;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using System;
using Views;

namespace App;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? data)
    {
        if(data is null) return null;

        var view = data.GetType().FullName!.Replace("ViewModel","View",StringComparison.InvariantCulture);
        var type = Type.GetType(view);

        if(type is null) return null;

        var control = (Control)Activator.CreateInstance(type)!;
        control.DataContext = data;
        return control;
        
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}
