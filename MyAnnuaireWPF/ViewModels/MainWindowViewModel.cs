using MyAnnuaireModel.Dto;
using MyAnnuaireModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MyAnnuaireWPF.Views;
using System.Windows.Input;

namespace MyAnnuaireWPF.ViewModels;

public class MainWindowViewModel
{
    public MainWindowViewModel()
    {
        MenuNavigationVisibility = Visibility.Collapsed;
    }

    public Visibility MenuNavigationVisibility { get; set; }

    public UtilisateurDto CurrentUser { get; set; } // Représente l'utilisateur connecté

    public UserControl OpenSalarie()
    {
        var control = new Salaries();
        var salarieViewModel = new SalarieViewModel();
        control.DataContext = salarieViewModel;
        salarieViewModel.RechargerService();
        salarieViewModel.RechargerSiege();
        salarieViewModel.ActualiserSalarie();
        return control;
    }
    public UserControl OpenTypeSiege()
    {
        var control = new TypeSieges();
        var salarieViewModel = new TypeSiegeViewModel();
        control.DataContext = salarieViewModel;
        salarieViewModel.RechargerTypeSiege();
        return control;
    }
    public UserControl OpenPays()
    {
        var control = new Views.Pays();
        var salarieViewModel = new PaysViewModel();
        control.DataContext = salarieViewModel;
        salarieViewModel.RechargerPays();
        return control;
    }
    public UserControl OpenService()
    {
        var control = new Views.Services();
        var salarieViewModel = new ServiceViewModel();
        control.DataContext = salarieViewModel;
        salarieViewModel.RechargerService();
        return control;
    }
    public UserControl OpenVille()
    {
        var control = new Views.Villes();
        var salarieViewModel = new VilleViewModel();
        control.DataContext = salarieViewModel;
        salarieViewModel.RechargerVille();
        return control;
    }
    public UserControl OpenUtilisateur()
    {
        var control = new Views.Utilisateurs();
        var salarieViewModel = new UtilisateurViewModel();
        control.DataContext = salarieViewModel;
        salarieViewModel.RechargerUser();
        return control;
    }
    public UserControl OpenSiege()
    {
        var control = new Views.Sieges();
        var salarieViewModel = new SiegeViewModel();
        control.DataContext = salarieViewModel;
        salarieViewModel.RechargerSiege();
        return control;
    }
    public UserControl OpenRechercher()
    {
        var control = new Views.Rechercher();
        var salarieViewModel = new RechercherViewModel();
        control.DataContext = salarieViewModel;
        return control;
    }
    public UserControl OpenAccueil()
    {
        var control = new Views.Accueil();
        return control;
    }
    public UserControl OpenLogin(KeyEventArgs e)
    {
        // Vérifie si Ctrl + V est pressé
        if (Keyboard.IsKeyDown(Key.LeftCtrl) && e.Key == Key.T)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                if (mainWindow.Environnement.Visibility == Visibility.Collapsed)
                {
                    var control = new Views.Login();
                    return control;
                }
                else
                {
                    MessageBox.Show("Vous êtes déjà connecté !");
                }
            }
        }
        return null;
    }
}

