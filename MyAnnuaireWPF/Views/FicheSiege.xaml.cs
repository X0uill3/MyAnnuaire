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

namespace MyAnnuaireWPF.Views
{
    /// <summary>
    /// Logique d'interaction pour FicheSiege.xaml
    /// </summary>
    public partial class FicheSiege : UserControl
    {
        public SiegeDto SiegeSelected { get; set; }

        public FicheSiege(SiegeDto siegeSelected)
        {
            InitializeComponent();
            SiegeSelected = siegeSelected;
            DataContext = this;
            RemplirListe();
        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            // Fermer la page FicheUtilisateur et réafficher Utilisateurs
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                var control = new Views.Sieges();
                var FamilleArticleViewModelV = new ViewModels.SiegeViewModel();
                control.DataContext = FamilleArticleViewModelV;
                FamilleArticleViewModelV.RechargerSiege();
                mainWindow.ContentArea.Content = control;
            }
        }

        private async void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (SiegeSelected.Id == 0)
            {
                await HttpMyAnnuaireService.CreateSiege(SiegeSelected);
            }
            else
            {
                await HttpMyAnnuaireService.UpdateSiege(SiegeSelected.Id, SiegeSelected);
            }
            Annuler_Click(sender, e);
        }

        private async void RemplirListe()
        {
            ListeVilles.ItemsSource = await HttpMyAnnuaireService.GetVilles();
            ListeTypeSiege.ItemsSource = await HttpMyAnnuaireService.GetTypeSieges();
        }
    }
}
