using MyAnnuaireModel.Dto;
using MyAnnuaireWPF.ViewModels;
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
/// Logique d'interaction pour Villes.xaml
/// </summary>
public partial class Villes : UserControl
{
    public Villes()
    {
        InitializeComponent();
        var ContextModel = new VilleViewModel();
        this.DataContext = ContextModel;
    }

    private void CreerButton_Click(object sender, RoutedEventArgs e)
    {
        var ContextModel = DataContext as VilleViewModel;
        ContextModel.CreerVille();
    }

    private void ModifierButton_Click(object sender, RoutedEventArgs e)
    {
        var ContextModel = DataContext as VilleViewModel;
        ContextModel.ModifierVille();
    }

    private void SupprimerButton_Click(object sender, RoutedEventArgs e)
    {
        var ContextModel = DataContext as VilleViewModel;
        ContextModel.SupprimerVille();
    }

    private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var salarie = (sender as DataGrid)?.SelectedItem as VilleDto;
        var ContextModel = DataContext as VilleViewModel;
        ContextModel.VilleSelected = salarie;
        ModifierButton.IsEnabled = salarie != null;
        SupprimerButton.IsEnabled = salarie != null;
    }
}
