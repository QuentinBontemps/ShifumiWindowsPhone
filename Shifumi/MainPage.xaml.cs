using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Shifumi.Resources;
using Shifumi.Core;
using Shifumi.Models;
using Shifumi.ViewModels;

namespace Shifumi
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructeur
        public MainPage()
        {
            InitializeComponent();

            string id = Tools.IdTel();

            Console.WriteLine(id);

            this.DataContext = new MainPageViewModel();

            // Exemple de code pour la localisation d'ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            /**
             * SI L'UTILISATEUR A DEJA REMPLI SON PSEUDO UNE PREMIERE FOIS
             * PASSER A LA PAGE SUIVANTE 
             **/

            AppSettings appSettings = new AppSettings();
            RegisterObject userToken = appSettings.UserTokenSetting;
            if (userToken != null)
            {
                //NavigationService.Navigate(new Uri("/Views/Home.xaml", UriKind.Relative));
            }
        }

    }
}