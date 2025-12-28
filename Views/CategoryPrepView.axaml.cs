using Avalonia.Controls;
using Avalonia.Interactivity;
using ViewModels;

namespace Views;

public partial class CategoryPrepView : UserControl
{

    public CategoryPrepView()
    {
        InitializeComponent();
    }

    public void NewTournamentClicked(object sender, RoutedEventArgs e){

        (DataContext as CategoryPrepViewModel)?.HideShowMenu();

    }
}
