using MyAnnuaireModel.Dto;
using MyAnnuaireWPF.Service;
using MyAnnuaireWPF.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace MyAnnuaireWPF.Views
{
    /// <summary>
    /// Logique d'interaction pour FicheVille.xaml
    /// </summary>
    public partial class FicheVille : UserControl
    {
        public VilleDto VilleSelected { get; set; }


        public FicheVille(VilleDto villeSelected)
        {
            InitializeComponent();
            VilleSelected = villeSelected;
            DataContext = this;
            RemplirListe();
        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            // Fermer la page FicheUtilisateur et réafficher Utilisateurs
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                var control = new Views.Villes();
                var FamilleArticleViewModelV = new ViewModels.VilleViewModel();
                control.DataContext = FamilleArticleViewModelV;
                FamilleArticleViewModelV.RechargerVille();
                mainWindow.ContentArea.Content = control;
            }
        }

        private async void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (VilleSelected.Id == 0)
            {
                await HttpMyAnnuaireService.CreateVille(VilleSelected);
            }
            else
            {
                await HttpMyAnnuaireService.UpdateVille(VilleSelected.Id, VilleSelected);
            }
            Annuler_Click(sender, e);
        }
        private async void RemplirListe()
        {
            ListePays.ItemsSource = await HttpMyAnnuaireService.GetPays();
        }
    }
}
