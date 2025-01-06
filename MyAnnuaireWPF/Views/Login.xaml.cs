using System.Windows;
using System.Windows.Controls;
using MyAnnuaireModel.Dto;
using MyAnnuaireWPF.Service;
using Microsoft.VisualBasic;
using MyAnnuaireWPF.ViewModels;

namespace MyAnnuaireWPF.Views;

public partial class Login : UserControl
{

    public Login()
    {
        InitializeComponent();
        this.DataContext = new LoginViewModel();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var ContextModel = DataContext as LoginViewModel;
        ContextModel.Authentifier(Identifiant.Text, Cleauthentification.Password);
        Identifiant.Text = "";
        Cleauthentification.Password = "";
    }
}