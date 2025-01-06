using System.Windows;
using System.Windows.Controls;
using MyAnnuaireModel.Dto;
using MyAnnuaireWPF.Service;
using MyAnnuaireWPF.ViewModels;

namespace MyAnnuaireWPF.Views
{
    public partial class Salaries : UserControl
    {
        public Salaries()
        {
            InitializeComponent();
            var ContextModel = new SalarieViewModel();
            this.DataContext = ContextModel;
        }

        private void CreerButton_Click(object sender, RoutedEventArgs e)
        {
            var ContextModel = DataContext as SalarieViewModel;
            ContextModel.CreerArticle();
        }

        private void ModifierButton_Click(object sender, RoutedEventArgs e)
        {
            var ContextModel = DataContext as SalarieViewModel;
            ContextModel.ModifierArticle();
        }

        private void SupprimerButton_Click(object sender, RoutedEventArgs e)
        {
            var ContextModel = DataContext as SalarieViewModel;
            ContextModel.SupprimerArticle();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var salarie = (sender as DataGrid)?.SelectedItem as SalarieDto;
            var ContextModel = DataContext as SalarieViewModel;
            ContextModel.SalarieSelected = salarie;
            ModifierButton.IsEnabled = salarie != null;
            SupprimerButton.IsEnabled = salarie != null;
        }
    }
}
