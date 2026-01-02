using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using Services;

namespace ViewModels;


public partial class DisplayFightsViewModel: ViewModelBase
{

    [ObservableProperty]
    private Tournament _selectedTournament;

}
