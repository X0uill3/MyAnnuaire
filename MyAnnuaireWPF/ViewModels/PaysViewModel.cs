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

internal class PaysViewModel
{
    public string Titre { get; set; } = "Liste des pays";
    public PaysDto PaysSelected { get; set; }

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
    public void CreerPays()
    {
        var FicheSalarie = new FichePays(new PaysDto());
        var mainWindow = Application.Current.MainWindow as MainWindow;
        if (mainWindow != null)
        {
            mainWindow.ContentArea.Content = FicheSalarie;
        }
    }
    public void ModifierPays()
    {
        if (PaysSelected != null)
        {
            var FicheSalarie = new FichePays(PaysSelected);
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                FicheSalarie.DataContext = this;
                mainWindow.ContentArea.Content = FicheSalarie;
            }
        }
        else
        {
            MessageBox.Show("Aucun pays sélectionné");
        }
    }

    public async void SupprimerPays()
    {
        if (PaysSelected != null)
        {
            MessageBoxResult result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer le pays : {PaysSelected.Name} ?",
                                                      "Confirmation de suppression",
                                                      MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                bool succes = await HttpMyAnnuaireService.DeleteSalarie(PaysSelected.Id);
                if (succes)
                {
                    _listePays.Remove(PaysSelected);

                    OnPropertyChanged(nameof(ListePays));

                    MessageBox.Show($" Le pays a été supprimé");
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
