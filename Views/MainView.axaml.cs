using Avalonia.Controls;
using Avalonia.Interactivity;
using App.ViewModels;

namespace App;

public partial class MainView : Window
{
    public MainView()
    {
        InitializeComponent();
    }

    public void NewTournamentClicked(object sender, RoutedEventArgs e){

        (DataContext as MainViewModel)?.HideShowMenu();

    }
}
