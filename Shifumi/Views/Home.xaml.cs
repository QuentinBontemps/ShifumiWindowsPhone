using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Shifumi.Models;
using Shifumi.ViewModels;

namespace Shifumi
{
    public partial class Home : PhoneApplicationPage
    {
        public Home()
        {
            InitializeComponent();
            
            this.DataContext = new HomeViewModel();
        }
    }
}