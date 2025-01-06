using MyAnnuaireModel.Dto;
using MyAnnuaireWPF.Service;
using MyAnnuaireWPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MyAnnuaireWPF.ViewModels;

internal class LoginViewModel
{
    public async void Authentifier(string username, string password)
    {
        try
        {
            bool isAuthenticated = await HttpMyAnnuaireService.VerifyUsernameAndPassword(username, password);
            var control = new Views.Accueil();
            if (isAuthenticated)
            {
                var mainWindow = Application.Current.MainWindow as MainWindow;
                if (mainWindow != null)
                {
                    mainWindow.Environnement.Visibility = Visibility.Visible;
                    mainWindow.ContentArea.Content = control;
                }
            }
            else
            {
                if (username == "admin" || password == "P@ssw0rd")
                {
                    // Création d'un utilisateur admin par défaut
                    UtilisateurDto user = new UtilisateurDto
                    {
                        login = "admin",
                        password = "admin",
                        EstAdmin = true,
                        Discriminator = "Administrateur",
                    };
                    await HttpMyAnnuaireService.CreateUtilisateur(user);

                    bool isAuthenticated2 = await HttpMyAnnuaireService.VerifyUsernameAndPassword(username, password);

                    if (isAuthenticated2)
                    {
                        var mainWindow = Application.Current.MainWindow as MainWindow;
                        if (mainWindow != null)
                        {
                            mainWindow.Environnement.Visibility = Visibility.Visible;
                            mainWindow.ContentArea.Content = control;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erreur lors de la tentative de connexion : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
