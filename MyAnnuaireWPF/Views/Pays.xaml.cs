using MyAnnuaireModel.Dto;
using MyAnnuaireWPF.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace MyAnnuaireWPF.Views;

/// <summary>
/// Logique d'interaction pour Pays.xaml
/// </summary>
public partial class Pays : UserControl
{
    public Pays()
    {
        InitializeComponent();
        var ContextModel = new PaysViewModel();
        this.DataContext = ContextModel;
    }

    private void CreerButton_Click(object sender, RoutedEventArgs e)
    {
        var ContextModel = DataContext as PaysViewModel;
        ContextModel.CreerPays();
    }

    private void ModifierButton_Click(object sender, RoutedEventArgs e)
    {
        var ContextModel = DataContext as PaysViewModel;
        ContextModel.ModifierPays();
    }

    private void SupprimerButton_Click(object sender, RoutedEventArgs e)
    {
        var ContextModel = DataContext as PaysViewModel;
        ContextModel.SupprimerPays();
    }

    private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var salarie = (sender as DataGrid)?.SelectedItem as PaysDto;
        var ContextModel = DataContext as PaysViewModel;
        ContextModel.PaysSelected = salarie;
        ModifierButton.IsEnabled = salarie != null;
        SupprimerButton.IsEnabled = salarie != null;
    }
}
