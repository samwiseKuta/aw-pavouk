using ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Views;

public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();
    }
    public void NewTournamentClicked(object sender, RoutedEventArgs e){

        (DataContext as HomeViewModel)?.HideShowMenu();

    }
}
