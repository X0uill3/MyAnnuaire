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

internal class TypeSiegeViewModel
{
    public string Titre { get; set; } = "Liste des type de sièges";
    public TypeSiegeDto TypeSiegeSelected { get; set; }

    private ObservableCollection<TypeSiegeDto> _listeTypeSieges = new();
    public ObservableCollection<TypeSiegeDto> ListeTypeSieges
    {
        get => _listeTypeSieges;
        set
        {
            _listeTypeSieges = value;
            OnPropertyChanged();
        }
    }
    public void RechargerTypeSiege()
    {
        Task.Run(async () =>
        {
            return await HttpMyAnnuaireService.GetTypeSieges();
        }).ContinueWith(t =>
        {

            _listeTypeSieges.Clear();
            foreach (var service in t.Result)
            {
                _listeTypeSieges.Add(service);
            }
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }
    public void CreerTypeSiege()
    {
        var FicheSalarie = new FicheTypeSiege(new TypeSiegeDto());
        var mainWindow = Application.Current.MainWindow as MainWindow;
        if (mainWindow != null)
        {
          mainWindow.ContentArea.Content = FicheSalarie;
        }
    }
    public void ModifierTypeSiege()
    {
        if (TypeSiegeSelected != null)
        {
            var FicheSalarie = new FicheTypeSiege(TypeSiegeSelected);
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                FicheSalarie.DataContext = this;
                mainWindow.ContentArea.Content = FicheSalarie;
            }
        }
        else
        {
            MessageBox.Show("Aucun type de siege sélectionné");
        }
    }

    public async void SupprimerTypeSiege()
    {
        if (TypeSiegeSelected != null)
        {
            MessageBoxResult result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer le type de siège : {TypeSiegeSelected.Name} ?",
                                                      "Confirmation de suppression",
                                                      MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                bool succes = await HttpMyAnnuaireService.DeleteSalarie(TypeSiegeSelected.Id);
                if (succes)
                {
                    _listeTypeSieges.Remove(TypeSiegeSelected);

                    OnPropertyChanged(nameof(ListeTypeSieges));

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
