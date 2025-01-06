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

class SiegeViewModel
    {
    public string Titre { get; set; } = "Liste des sièges";
    public SiegeDto SiegeSelected { get; set; }

    private ObservableCollection<SiegeDto> _listeSieges = new();
    public ObservableCollection<SiegeDto> ListeSieges
    {
        get => _listeSieges;
        set
        {
            _listeSieges = value;
            OnPropertyChanged();
        }
    }
    public void RechargerSiege()
    {
        Task.Run(async () =>
        {
            return await HttpMyAnnuaireService.GetSieges();
        }).ContinueWith(t =>
        {

            _listeSieges.Clear();
            foreach (var service in t.Result)
            {
                _listeSieges.Add(service);
            }
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }
    public void CreerSiege()
    {
        var FicheSalarie = new FicheSiege(new SiegeDto());
        var mainWindow = Application.Current.MainWindow as MainWindow;
        if (mainWindow != null)
        {
            mainWindow.ContentArea.Content = FicheSalarie;
        }
    }
    public void ModifierSiege()
    {
        if (SiegeSelected != null)
        {
            var FicheSalarie = new FicheSiege(SiegeSelected);
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                FicheSalarie.DataContext = this;
                mainWindow.ContentArea.Content = FicheSalarie;
            }
        }
        else
        {
            MessageBox.Show("Aucun siege sélectionné");
        }
    }

    public async void SupprimerSiege()
    {
        if (SiegeSelected != null)
        {
            MessageBoxResult result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer le siège de la ville de : {SiegeSelected.Ville} ?",
                                                      "Confirmation de suppression",
                                                      MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                bool succes = await HttpMyAnnuaireService.DeleteSalarie(SiegeSelected.Id);
                if (succes)
                {
                    _listeSieges.Remove(SiegeSelected);

                    OnPropertyChanged(nameof(ListeSieges));

                    MessageBox.Show($" Le salarié a été supprimé");
                }
            }
        }
        else
        {
            MessageBox.Show("Aucun salarié sélectionné");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
