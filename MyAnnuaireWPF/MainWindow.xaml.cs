using Microsoft.VisualBasic;
using MyAnnuaireWPF.Service;
using MyAnnuaireWPF.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyAnnuaireWPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly MainWindowViewModel _viewModel;

    public MainWindow()
    {
        InitializeComponent();

        _viewModel = new MainWindowViewModel();
        DataContext = _viewModel;
        MenuNavigation.Visibility = _viewModel.MenuNavigationVisibility;

        var control = new Views.Accueil();
        ContentArea.Content = control;

        // Afficher la ProgressBar dès le lancement
        ProgressBarLoading.Value = 0; // Réinitialiser la valeur de la ProgressBar à 0
        ProgressBarLoading.Visibility = Visibility.Visible;
        Task.Run(async () =>
        {
            try
            {
                // Appeler la méthode de connexion en arrière-plan
                bool isAuthenticated = await Task.Run(async () => await HttpMyAnnuaireService.Login("admin@myannuaire.fr", "P@ssw0rd"));

                // Si la connexion est réussie, mettre la ProgressBar à 100%
                Dispatcher.Invoke(() =>
                {
                    ProgressBarLoading.Value = 100;
                    ProgressBarLoading.Visibility = Visibility.Hidden;
                    if (isAuthenticated)
                    {
                        MenuNavigation.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        MessageBox.Show("Erreur d'authentification", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                });
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() =>
                {
                    ProgressBarLoading.Visibility = Visibility.Hidden;
                    MessageBox.Show($"Erreur lors de la tentative de connexion : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                });
            }
        });
    }

    private void Open_Salarie(object sender, RoutedEventArgs e)
    {
        ContentArea.Content = _viewModel.OpenSalarie();
    }
    private void Open_TypeSiege(object sender, RoutedEventArgs e)
    {
        ContentArea.Content = _viewModel.OpenTypeSiege();
    }
    private void Open_Pays(object sender, RoutedEventArgs e)
    {
        ContentArea.Content = _viewModel.OpenPays();
    }
    private void Open_Service(object sender, RoutedEventArgs e)
    {
        ContentArea.Content = _viewModel.OpenService();
    }
    private void Open_Ville(object sender, RoutedEventArgs e)
    {
        ContentArea.Content = _viewModel.OpenVille();
    }
    private void Open_Utilisateur(object sender, RoutedEventArgs e)
    {
        ContentArea.Content = _viewModel.OpenUtilisateur();
    }
    private void Open_Siege(object sender, RoutedEventArgs e)
    {
        ContentArea.Content = _viewModel.OpenSiege();
    }
    private void Open_Rechercher(object sender, RoutedEventArgs e)
    {
        ContentArea.Content = _viewModel.OpenRechercher();
    }
    private void Open_Accueil(object sender, RoutedEventArgs e)
    {
        var accueilControl = _viewModel.OpenAccueil();
        if (accueilControl != null)
        {
            ContentArea.Content = accueilControl;
        }
    }

    private void Rechercher_KeyDown(object sender, KeyEventArgs e)
    {
        var accueilControl = _viewModel.OpenLogin(e);
        if (accueilControl != null)
        {
            ContentArea.Content = accueilControl;
        }
    }

}