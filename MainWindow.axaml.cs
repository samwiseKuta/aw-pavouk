using Avalonia.Controls;
using Avalonia.Interactivity;

namespace App;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void GreetButton_Click(object sender, RoutedEventArgs e){

        Bracket b = new Bracket().GenerateEmpty(20);
        var messageControl = this.FindControl<TextBlock>("MessageLabel");

        messageControl.Text = $"The bracket: \n{b.ToString()}";
    }
}
