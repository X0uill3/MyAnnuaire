using MyAnnuaireModel.Dto;
using MyAnnuaireWPF.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace MyAnnuaireWPF.Views;

/// <summary>
/// Logique d'interaction pour Pays.xaml
/// </summary>
public partial class Utilisateurs : UserControl
{
    public Utilisateurs()
    {
        InitializeComponent();
        var ContextModel = new UtilisateurViewModel();
        this.DataContext = ContextModel;
    }

    private void CreerButton_Click(object sender, RoutedEventArgs e)
    {
        var ContextModel = DataContext as UtilisateurViewModel;
        ContextModel.CreerUser();
    }

    private void ModifierButton_Click(object sender, RoutedEventArgs e)
    {
        var ContextModel = DataContext as UtilisateurViewModel;
        ContextModel.ModifierUser();
    }

    private void SupprimerButton_Click(object sender, RoutedEventArgs e)
    {
        var ContextModel = DataContext as UtilisateurViewModel;
        ContextModel.SupprimerUser();
    }

    private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var salarie = (sender as DataGrid)?.SelectedItem as UtilisateurDto;
        var ContextModel = DataContext as UtilisateurViewModel;
        ContextModel.UserSelected = salarie;
        ModifierButton.IsEnabled = salarie != null;
        SupprimerButton.IsEnabled = salarie != null;
    }
}
