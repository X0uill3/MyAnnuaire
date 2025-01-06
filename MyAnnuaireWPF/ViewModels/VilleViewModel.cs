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

class VilleViewModel
{
    public string Titre { get; set; } = "Liste des villes";
    public VilleDto VilleSelected { get; set; }

    private ObservableCollection<VilleDto> _listeVilles = new();
    public ObservableCollection<VilleDto> ListeVilles
    {
        get => _listeVilles;
        set
        {
            _listeVilles = value;
            OnPropertyChanged();
        }
    }
    public void RechargerVille()
    {
        Task.Run(async () =>
        {
            return await HttpMyAnnuaireService.GetVilles();
        }).ContinueWith(t =>
        {

            _listeVilles.Clear();
            foreach (var service in t.Result)
            {
                _listeVilles.Add(service);
            }
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }
    private ObservableCollection<PaysDto> _listePays = new();
    public ObservableCollection<PaysDto> ListePays
    {
        get => _listePays;
        set
        {
            _listePays = value;
            OnPropertyChanged();
        }
    }
    public void RechargerPays()
    {
        Task.Run(async () =>
        {
            return await HttpMyAnnuaireService.GetPays();
        }).ContinueWith(t =>
        {

            _listePays.Clear();
            foreach (var service in t.Result)
            {
                _listePays.Add(service);
            }
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }
    public void CreerVille()
    {
        var FicheSalarie = new FicheVille(new VilleDto());
        var mainWindow = Application.Current.MainWindow as MainWindow;
        if (mainWindow != null)
        {
            mainWindow.ContentArea.Content = FicheSalarie;
        }
    }
    public void ModifierVille()
    {
        if (VilleSelected != null)
        {
            var FicheSalarie = new FicheVille(VilleSelected);
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                FicheSalarie.DataContext = this;
                mainWindow.ContentArea.Content = FicheSalarie;
            }
        }
        else
        {
            MessageBox.Show("Aucune Ville sélectionné");
        }
    }

    public async void SupprimerVille()
    {
        if (VilleSelected != null)
        {
            MessageBoxResult result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer la ville : {VilleSelected.Name} ?",
                                                      "Confirmation de suppression",
                                                      MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                bool succes = await HttpMyAnnuaireService.DeleteSalarie(VilleSelected.Id);
                if (succes)
                {
                    _listeVilles.Remove(VilleSelected);

                    OnPropertyChanged(nameof(ListeVilles));

                    MessageBox.Show($" La ville a été supprimée");
                }
            }
        }
        else
        {
            MessageBox.Show("Aucune Ville sélectionné");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}