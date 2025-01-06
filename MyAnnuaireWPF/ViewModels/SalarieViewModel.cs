using MyAnnuaireModel.Dto;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using MyAnnuaireWPF.Service;
using MyAnnuaireWPF.Views;
using MyAnnuaireWPF;
using System.Windows.Controls;

namespace MyAnnuaireWPF.ViewModels;

internal class SalarieViewModel : INotifyPropertyChanged
{

    public string Titre { get; set; } = "Liste des salariés";

    public UserControl FichePrecedente { get; set; }

    private ObservableCollection<SalarieDto> _listeSalaries = new();
    public ObservableCollection<SalarieDto> ListeSalaries
    {
        get => _listeSalaries;
        set
        {
            _listeSalaries = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<ServiceDto> _listeServices = new();
    public ObservableCollection<ServiceDto> ListeServices
    {
        get => _listeServices;
        set
        {
            _listeServices = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<SiegeDto> _listeSieges = new();
    public ObservableCollection<SiegeDto> ListeSieges
    {
        get => _listeSieges;
        set
        {
            _listeSieges = value;
            OnPropertyChanged();
        }
    }

    public SalarieDto SalarieSelected { get; set; }

    public Visibility FicheVisibility { get; set; } = Visibility.Collapsed;

    public void ActualiserSalarie()
    {
        Task.Run(async () =>
        {
            return await HttpMyAnnuaireService.GetSalaries();
        }).ContinueWith(t =>
        {
            if (t.Exception != null)
            {
                return;
            }

            _listeSalaries.Clear();
            foreach (var salarie in t.Result)
            {
                _listeSalaries.Add(salarie);
            }
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }


    public void CreerArticle()
    {
        var FicheSalarie = new FicheSalarie(new SalarieDto());
        var mainWindow = Application.Current.MainWindow as MainWindow;
        if (mainWindow != null)
        {
            mainWindow.ContentArea.Content = FicheSalarie;
        }
    }
    public void ModifierArticle()
    {
        if (SalarieSelected != null)
        {
            var FicheSalarie = new FicheSalarie(SalarieSelected);
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                FicheSalarie.DataContext = this;
                RechargerService();
                RechargerSiege();
                mainWindow.ContentArea.Content = FicheSalarie;
            }
        }
        else
        {
            MessageBox.Show("Aucun salarié sélectionné");
        }
    }

    public void Annuler()
    {
        if (FichePrecedente != null)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.ContentArea.Content = FichePrecedente;
            }
        }
        else
        {
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

    public async void SupprimerArticle()
    {
        if (SalarieSelected != null)
        {
            MessageBoxResult result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer l'article {SalarieSelected.Nom} ?",
                                                      "Confirmation de suppression",
                                                      MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                bool succes = await HttpMyAnnuaireService.DeleteSalarie(SalarieSelected.Id);
                if (succes)
                {
                    _listeSalaries.Remove(SalarieSelected);

                    OnPropertyChanged(nameof(ListeSalaries));

                    MessageBox.Show($" Le salarié a été supprimé");
                }
            }
        }
        else
        {
            MessageBox.Show("Aucun salarié sélectionné");
        }
    }

    public void RechargerSiege()
    {
        Task.Run(async () =>
        {
            return await HttpMyAnnuaireService.GetSieges();
        }).ContinueWith(t =>
        {

            _listeSieges.Clear();
            foreach (var siege in t.Result)
            {
                _listeSieges.Add(siege);
            }
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }    
    
    public void RechargerService()
    {
        Task.Run(async () =>
        {
            return await HttpMyAnnuaireService.GetServices();
        }).ContinueWith(t =>
        {

            _listeServices.Clear();
            foreach (var service in t.Result)
            {
                _listeServices.Add(service);
            }
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

