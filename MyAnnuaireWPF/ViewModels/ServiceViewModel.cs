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

class ServiceViewModel
{
    public string Titre { get; set; } = "Liste des services";
    public ServiceDto ServiceSelected { get; set; }

    private ObservableCollection<ServiceDto> _listeService = new();
    public ObservableCollection<ServiceDto> ListeServices
    {
        get => _listeService;
        set
        {
            _listeService = value;
            OnPropertyChanged();
        }
    }
    public void RechargerService()
    {
        Task.Run(async () =>
        {
            return await HttpMyAnnuaireService.GetServices();
        }).ContinueWith(t =>
        {

            _listeService.Clear();
            foreach (var service in t.Result)
            {
                _listeService.Add(service);
            }
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }
    public void CreerService()
    {
        var FicheSalarie = new FicheService(new ServiceDto());
        var mainWindow = Application.Current.MainWindow as MainWindow;
        if (mainWindow != null)
        {
            mainWindow.ContentArea.Content = FicheSalarie;
        }
    }
    public void ModifierService()
    {
        if (ServiceSelected != null)
        {
            var FicheSalarie = new FicheService(ServiceSelected);
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                FicheSalarie.DataContext = this;
                mainWindow.ContentArea.Content = FicheSalarie;
            }
        }
        else
        {
            MessageBox.Show("Aucun service sélectionné");
        }
    }

    public async void SupprimerService()
    {
        if (ServiceSelected != null)
        {
            MessageBoxResult result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer le service : {ServiceSelected.Name} ?",
                                                      "Confirmation de suppression",
                                                      MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                bool succes = await HttpMyAnnuaireService.DeleteService(ServiceSelected.Id);
                if (succes)
                {
                    _listeService.Remove(ServiceSelected);

                    OnPropertyChanged(nameof(ListeServices));

                    MessageBox.Show($" Le service a été supprimé");
                }
            }
        }
        else
        {
            MessageBox.Show("Aucun service sélectionné");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}