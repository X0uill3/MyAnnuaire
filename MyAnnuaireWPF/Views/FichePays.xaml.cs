using System.Windows;
using System.Windows.Controls;
using MyAnnuaireModel.Dto;
using MyAnnuaireModel.Entities;
using MyAnnuaireWPF.Service;

namespace MyAnnuaireWPF.Views
{
    public partial class FichePays : UserControl
    {

        public PaysDto PaysSelected { get; set; }

        public FichePays(PaysDto paysSelected)
        {
            InitializeComponent();
            PaysSelected = paysSelected;
            DataContext = this;
        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            // Fermer la page FicheUtilisateur et réafficher Utilisateurs
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                var control = new Views.Pays();
                var FamilleArticleViewModelV = new ViewModels.PaysViewModel();
                control.DataContext = FamilleArticleViewModelV;
                FamilleArticleViewModelV.RechargerPays();
                mainWindow.ContentArea.Content = control;
            }
        }

        private async void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (PaysSelected.Id == 0)
            {
                await HttpMyAnnuaireService.CreatePays(PaysSelected);
            }
            else
            {
                await HttpMyAnnuaireService.UpdatePays(PaysSelected.Id, PaysSelected);
            }
            Annuler_Click(sender, e);
        }
    }
}
