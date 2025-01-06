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

class UtilisateurViewModel
{
    public string Titre { get; set; } = "Liste des utilisateurs";
    public UtilisateurDto UserSelected { get; set; }

    private ObservableCollection<UtilisateurDto> _listeUser = new();
    public ObservableCollection<UtilisateurDto> ListeUser
    {
        get => _listeUser;
        set
        {
            _listeUser = value;
            OnPropertyChanged();
        }
    }
    public void RechargerUser()
    {
        Task.Run(async () =>
        {
            return await HttpMyAnnuaireService.GetUtilisateurs();
        }).ContinueWith(t =>
        {

            _listeUser.Clear();
            foreach (var service in t.Result)
            {
                _listeUser.Add(service);
            }
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }
    public void CreerUser()
    {
        var FicheSalarie = new FicheUtilisateur(new UtilisateurDto());
        var mainWindow = Application.Current.MainWindow as MainWindow;
        if (mainWindow != null)
        {
            mainWindow.ContentArea.Content = FicheSalarie;
        }
    }
    public void ModifierUser()
    {
        if (UserSelected != null)
        {
            var FicheSalarie = new FicheUtilisateur(UserSelected);
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                FicheSalarie.DataContext = this;
                mainWindow.ContentArea.Content = FicheSalarie;
            }
        }
        else
        {
            MessageBox.Show("Aucun User sélectionné");
        }
    }

    public async void SupprimerUser()
    {
        if (UserSelected != null)
        {
            MessageBoxResult result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer l'utilisateur : {UserSelected.login} ?",
                                                      "Confirmation de suppression",
                                                      MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                bool succes = await HttpMyAnnuaireService.DeleteSalarie(UserSelected.Id);
                if (succes)
                {
                    _listeUser.Remove(UserSelected);

                    OnPropertyChanged(nameof(ListeUser));

                    MessageBox.Show($" L'utilisateur a été supprimé");
                }
            }
        }
        else
        {
            MessageBox.Show("Aucun User sélectionné");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}