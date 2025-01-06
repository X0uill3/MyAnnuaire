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

namespace MyAnnuaireWPF.Views
{
    /// <summary>
    /// Logique d'interaction pour TypeSiege.xaml
    /// </summary>
    public partial class TypeSieges : UserControl
    {
        public TypeSieges()
        {
            InitializeComponent();
            var ContextModel = new TypeSiegeViewModel();
            this.DataContext = ContextModel;
        }

        private void CreerButton_Click(object sender, RoutedEventArgs e)
        {
            var ContextModel = DataContext as TypeSiegeViewModel;
            ContextModel.CreerTypeSiege();
        }

        private void ModifierButton_Click(object sender, RoutedEventArgs e)
        {
            var ContextModel = DataContext as TypeSiegeViewModel;
            ContextModel.ModifierTypeSiege();
        }

        private void SupprimerButton_Click(object sender, RoutedEventArgs e)
        {
            var ContextModel = DataContext as TypeSiegeViewModel;
            ContextModel.SupprimerTypeSiege();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var salarie = (sender as DataGrid)?.SelectedItem as TypeSiegeDto;
            var ContextModel = DataContext as TypeSiegeViewModel;
            ContextModel.TypeSiegeSelected = salarie;
            ModifierButton.IsEnabled = salarie != null;
            SupprimerButton.IsEnabled = salarie != null;
        }
    }
}
