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
    /// Logique d'interaction pour Rechercher.xaml
    /// </summary>
    public partial class Rechercher : UserControl
    {
        public Rechercher()
        {
            InitializeComponent();
            var ContextModel = new RechercherViewModel();
            this.DataContext = ContextModel;
        }

        private void Search(object sender, TextChangedEventArgs e)
        {
            var ContextModel = DataContext as RechercherViewModel;
            ContextModel.RechercherSalaries(inputs.Text);
        }
        
        private void SearchButton(object sender, RoutedEventArgs e)
        {
            var ContextModel = DataContext as RechercherViewModel;
            ContextModel.RechercherSalaries(inputs.Text);
        }
        
        private void Detail_Click(object sender, RoutedEventArgs e)
        {
            var salarie = dtg?.SelectedItem as SalarieDto;
            var ContextModel = new ViewModels.SalarieViewModel();
            ContextModel.SalarieSelected = salarie;

            ContextModel.FichePrecedente = new Views.Rechercher();
            ContextModel.ModifierArticle();
        }
    }
}
