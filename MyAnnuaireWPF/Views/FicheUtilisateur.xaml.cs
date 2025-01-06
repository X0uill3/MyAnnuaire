using MyAnnuaireModel.Dto;
using MyAnnuaireWPF.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyAnnuaireWPF.Views;

/// <summary>
/// Logique d'interaction pour FicheUtilisateur.xaml
/// </summary>
public partial class FicheUtilisateur : UserControl
{
    public UtilisateurDto UserSelected { get; set; }

    public FicheUtilisateur(UtilisateurDto userSelected)
    {
        InitializeComponent();
        UserSelected = userSelected;
        DataContext = this;
    }

    private void Annuler_Click(object sender, RoutedEventArgs e)
    {
        // Fermer la page FicheUtilisateur et réafficher Utilisateurs
        var mainWindow = Application.Current.MainWindow as MainWindow;
        if (mainWindow != null)
        {
            var control = new Views.Utilisateurs();
            var FamilleArticleViewModelV = new ViewModels.UtilisateurViewModel();
            control.DataContext = FamilleArticleViewModelV;
            FamilleArticleViewModelV.RechargerUser();
            mainWindow.ContentArea.Content = control;
        }
    }

    private async void Enregistrer_Click(object sender, RoutedEventArgs e)
    {
        if (UserSelected.Id == 0)
        {
            await HttpMyAnnuaireService.CreateUtilisateur(UserSelected);
        }
        else
        {
            await HttpMyAnnuaireService.UpdateUtilisateur(UserSelected.Id, UserSelected);
        }
        Annuler_Click(sender, e);
    }
}

