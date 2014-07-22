using Shifumi.Core;
using Shifumi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace Shifumi.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {

        private String _nickname;

        public String Nickname
        {
            get { return _nickname; }
            set { Assign("Nickname", ref _nickname, value); }
        }
        
        private DelegateCommand _loginCommand;

        public DelegateCommand LoginCommand
        {
            get { return _loginCommand; }
            set { _loginCommand = value; }
        }


        public MainPageViewModel()
        {
            _nickname = "Momo";
            _loginCommand = new DelegateCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
        }


        private void ExecuteLoginCommand(object param)
        {
            Login();
        }

        private bool CanExecuteLoginCommand(object param)
        {
            return (_nickname != "");
        }
        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == "Nickname")
            {
                _loginCommand.RaiseCanExecuteChanged();
            }
        }
        
        private async void Login()
        {
            
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_DoWork;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            bw.RunWorkerAsync();
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            App.RootFrame.Navigate(new Uri("/Views/Home.xaml", UriKind.Relative));
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            UserService userService = new UserService();
            userService.Register(Nickname);

            System.Threading.Thread.Sleep(2000);
        }

    }
}