﻿using MyAnnuaireWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyAnnuaireWPF.Views
{
    /// <summary>
    /// Logique d'interaction pour Parametres.xaml
    /// </summary>
    public partial class Parametres : UserControl
    {
        public Parametres()
        {
            InitializeComponent();
            var ContextModel = new ParametresViewModel();
            this.DataContext = ContextModel;
        }

        public void TestAPI(object sender, RoutedEventArgs e)
        {
            var ContextModel = DataContext as ParametresViewModel;
            ContextModel.TestAPI();
        }
    }
}
