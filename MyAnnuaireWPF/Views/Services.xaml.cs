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
    /// Logique d'interaction pour Services.xaml
    /// </summary>
    public partial class Services : UserControl
    {
        public Services()
        {
            InitializeComponent();
            var ContextModel = new ServiceViewModel();
            this.DataContext = ContextModel;
        }

        private void CreerButton_Click(object sender, RoutedEventArgs e)
        {
            var ContextModel = DataContext as ServiceViewModel;
            ContextModel.CreerService();
        }

        private void ModifierButton_Click(object sender, RoutedEventArgs e)
        {
            var ContextModel = DataContext as ServiceViewModel;
            ContextModel.ModifierService();
        }

        private void SupprimerButton_Click(object sender, RoutedEventArgs e)
        {
            var ContextModel = DataContext as ServiceViewModel;
            ContextModel.SupprimerService();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var salarie = (sender as DataGrid)?.SelectedItem as ServiceDto;
            var ContextModel = DataContext as ServiceViewModel;
            ContextModel.ServiceSelected = salarie;
            ModifierButton.IsEnabled = salarie != null;
            SupprimerButton.IsEnabled = salarie != null;
        }
    }
}
