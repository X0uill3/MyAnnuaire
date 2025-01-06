using System.Windows;
using System.Windows.Controls;
using MyAnnuaireModel.Dto;
using MyAnnuaireModel.Entities;
using MyAnnuaireWPF.Service;
using MyAnnuaireWPF.ViewModels;
using MyAnnuaireWPF;

namespace MyAnnuaireWPF.Views
{
    public partial class FicheSalarie : UserControl
    {
        public SalarieDto SalarieSelected { get; set; }

        public FicheSalarie(SalarieDto salarieSelected)
        {
            InitializeComponent();
            SalarieSelected = salarieSelected;
            DataContext = this;
            RemplirListe();

            if (IsConnected())
            {
                div.IsEnabled = true;
                EnregistrerButton.Visibility = Visibility.Visible;
            }
            else
            {
                div.IsEnabled = false;
                EnregistrerButton.Visibility = Visibility.Hidden;
            }

        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            // Fermer la page FicheArticle et réafficher la précédente
            var ContextModel = DataContext as SalarieViewModel;
            if (ContextModel != null)
            {
                ContextModel.Annuler();
            }
            else {
                var mainWindow = Application.Current.MainWindow as MainWindow;
                if (mainWindow != null)
                {
                    var control = new Views.Salaries();
                    var articlesViewModel = new ViewModels.SalarieViewModel();
                    control.DataContext = articlesViewModel;
                    articlesViewModel.ActualiserSalarie();
                    mainWindow.ContentArea.Content = control;
                }
            }
        }

        private async void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (SalarieSelected.Id == 0)
            {
                await HttpMyAnnuaireService.CreateSalarie(SalarieSelected);
            }
            else
            {
                await HttpMyAnnuaireService.UpdateSalarie(SalarieSelected.Id, SalarieSelected);
            }
            Annuler_Click(sender, e);
        }

        private async void RemplirListe()
        {
            ListeService.ItemsSource = await HttpMyAnnuaireService.GetServices();
            ListeSiege.ItemsSource = await HttpMyAnnuaireService.GetSieges();
        }

        public bool IsConnected()
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow != null)
            {
                if (mainWindow.Environnement != null && EnregistrerButton != null)
                {
                    if(mainWindow.Environnement.Visibility == Visibility.Visible)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

    }
}
