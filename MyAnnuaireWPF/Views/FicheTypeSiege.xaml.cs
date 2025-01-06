using System.Windows;
using System.Windows.Controls;
using MyAnnuaireModel.Dto;
using MyAnnuaireModel.Entities;
using MyAnnuaireWPF.Service;

namespace MyAnnuaireWPF.Views
{
    public partial class FicheTypeSiege : UserControl
    {

        public TypeSiegeDto TypeSiegeSelected { get; set; }

        public FicheTypeSiege(TypeSiegeDto typeSiegeSelected)
        {
            InitializeComponent();
            TypeSiegeSelected = typeSiegeSelected;
            DataContext = this;
        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            // Fermer la page FicheUtilisateur et réafficher Utilisateurs
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                var control = new Views.TypeSieges();
                var FamilleArticleViewModelV = new ViewModels.TypeSiegeViewModel();
                control.DataContext = FamilleArticleViewModelV;
                FamilleArticleViewModelV.RechargerTypeSiege();
                mainWindow.ContentArea.Content = control;
            }
        }

        private async void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (TypeSiegeSelected.Id == 0)
            {
                await HttpMyAnnuaireService.CreateTypeSiege(TypeSiegeSelected);
            }
            else
            {
                await HttpMyAnnuaireService.UpdateTypeSiege(TypeSiegeSelected.Id, TypeSiegeSelected);
            }
            Annuler_Click(sender, e);
        }
    }
}
