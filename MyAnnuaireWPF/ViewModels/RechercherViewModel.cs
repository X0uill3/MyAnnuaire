using MyAnnuaireModel.Dto;
using MyAnnuaireWPF.Service;
using MyAnnuaireWPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyAnnuaireWPF.ViewModels;

class RechercherViewModel
{
    public string Titre { get; set; } = "Rechercher un salarié";
    public SalarieDto SalarieSelected { get; set; }

    private ObservableCollection<SalarieDto> _listeSalaries = new();
    public ObservableCollection<SalarieDto> ListeSalaries
    {
        get => _listeSalaries;
        set
        {
            _listeSalaries = value;
            OnPropertyChanged();
        }
    }
    public void RechercherSalaries(string parametre)
    {
        Task.Run(async () =>
        {
            return await HttpMyAnnuaireService.RechercherSalarie(parametre);
        }).ContinueWith(t =>
        {

            _listeSalaries.Clear();

            if (t.IsFaulted == true)
            {
                return;
            }

            foreach (var service in t.Result)
            {
                _listeSalaries.Add(service);
            }
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
