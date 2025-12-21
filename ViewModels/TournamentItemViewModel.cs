using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using App;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ViewModels;


public partial class TournamentItemViewModel: ViewModelBase
{
    public ObservableCollection<Tournament>? TournamentHistory {get;set;}

    public TournamentItemViewModel(){
        TournamentHistory=new ObservableCollection<Tournament>(
                new List<Tournament>{
                new Tournament() {Name="Vanocni Paka",Location="Pindik"},
                new Tournament() {Name="Golem",Location="Brno"},
                new Tournament() {Name="Podivin",Location="Prdel"},
                new Tournament() {Name="Evls",Location="Praha"},
                new Tournament() {Name="Evls",Location="Praha"},
                new Tournament() {Name="Evls",Location="Praha"},
                new Tournament() {Name="Evls",Location="Praha"},
                new Tournament() {Name="Evls",Location="Praha"},
                new Tournament() {Name="Evls",Location="Praha"},
                new Tournament() {Name="Evls",Location="Praha"},
                new Tournament() {Name="Evls",Location="Praha"},

                }
                );
    }

}
