using Avalonia;
using Avalonia.Controls;

namespace Controls;

public class AddButton:Button
{

    public static readonly StyledProperty<string> PrompTextProperty= AvaloniaProperty.Register<AddButton,string>(
            nameof(Prompt)
            );

    public string Prompt{
        get => GetValue(PrompTextProperty);
        set => SetValue(PrompTextProperty,value);
    }



}
