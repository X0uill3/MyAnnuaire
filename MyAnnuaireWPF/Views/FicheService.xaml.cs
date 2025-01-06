using MyAnnuaireModel.Dto;
using MyAnnuaireWPF.Service;
using System.Windows;
using System.Windows.Controls;

namespace MyAnnuaireWPF.Views;

/// <summary>
/// Logique d'interaction pour FicheService.xaml
/// </summary>
public partial class FicheService : UserControl
{

    public ServiceDto ServiceSelected { get; set; }

    public FicheService(ServiceDto serviceSelected)
    {
        InitializeComponent();
        ServiceSelected = serviceSelected;
        DataContext = this;
    }

    private void Annuler_Click(object sender, RoutedEventArgs e)
    {
        // Fermer la page FicheUtilisateur et réafficher Utilisateurs
        var mainWindow = Application.Current.MainWindow as MainWindow;
        if (mainWindow != null)
        {
            var control = new Views.Services();
            var FamilleArticleViewModelV = new ViewModels.ServiceViewModel();
            control.DataContext = FamilleArticleViewModelV;
            FamilleArticleViewModelV.RechargerService();
            mainWindow.ContentArea.Content = control;
        }
    }

    private async void Enregistrer_Click(object sender, RoutedEventArgs e)
    {
        if (ServiceSelected.Id == 0)
        {
            await HttpMyAnnuaireService.CreateService(ServiceSelected);
        }
        else
        {
            await HttpMyAnnuaireService.UpdateService(ServiceSelected.Id, ServiceSelected);
        }
        Annuler_Click(sender, e);
    }
}
