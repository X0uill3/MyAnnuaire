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
/// Logique d'interaction pour Sieges.xaml
/// </summary>
public partial class Sieges : UserControl
{
 
        public Sieges()
        {
            InitializeComponent();
            var ContextModel = new SiegeViewModel();
            this.DataContext = ContextModel;
        }
        private void CreerButton_Click(object sender, RoutedEventArgs e)
        {
            var ContextModel = DataContext as SiegeViewModel;
            ContextModel.CreerSiege();
        }

        private void ModifierButton_Click(object sender, RoutedEventArgs e)
        {
            var ContextModel = DataContext as SiegeViewModel;
            ContextModel.ModifierSiege();
        }

        private void SupprimerButton_Click(object sender, RoutedEventArgs e)
        {
            var ContextModel = DataContext as SiegeViewModel;
            ContextModel.SupprimerSiege();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var salarie = (sender as DataGrid)?.SelectedItem as SiegeDto;
            var ContextModel = DataContext as SiegeViewModel;
            ContextModel.SiegeSelected = salarie;
            ModifierButton.IsEnabled = salarie != null;
            SupprimerButton.IsEnabled = salarie != null;
        }

    
}
