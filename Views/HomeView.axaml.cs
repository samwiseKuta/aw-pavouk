using ViewModels;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace Views;

public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();
    }
    public void NewTournamentClicked(object sender, RoutedEventArgs e){

        (DataContext as HomeViewModel)?.HideShowMenuCommand.Execute(null);
    }

    public void CreateTournamentClicked(object sender, RoutedEventArgs e){

    }
}
