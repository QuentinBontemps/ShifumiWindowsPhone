using Shifumi.Core;
using Shifumi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.ComponentModel;

namespace Shifumi.ViewModels
{
    class HomeViewModel : ViewModelBase
    {

        #region fields

        private int _actionSelected;
        private String _actionChoosed;
        private bool _commandEnable;
        private DelegateCommand _sendCommand;
        private DelegateCommand _actionCommand;
        private DelegateCommand _scoreBoardCommand;
        private int _currentRound;
        private int _countDown;
        private int _bestScore;
        private int _yourScore;
        private String _bestUser;
                
        #endregion


        #region properties
        protected int ActionSelected
        {
            get { return _actionSelected; }
            set { Assign("ActionSelected", ref _actionSelected, value); }
        }

        public String ActionChoosed
        {
            get { return _actionChoosed; }
            set { Assign("ActionChoosed", ref _actionChoosed, value); }
        }

        public bool CommandEnable
        {
            get { return _commandEnable; }
            set { Assign("CommandEnable", ref _commandEnable, value); }
        }

        public DelegateCommand SendCommand
        {
            get { return _sendCommand; }
            set { _sendCommand = value; }
        }

        public DelegateCommand ActionCommand
        {
            get { return _actionCommand; }
            set { _actionCommand = value; }
        }

        public int CurrentRound
        {
            get { return _currentRound; }
            set { Assign("CurrentRound", ref _currentRound, value); }
        }

        public int CountDown
        {
            get { return _countDown; }
            set { Assign("CountDown", ref _countDown, value); }
        }

        public DelegateCommand ScoreBoardCommand
        {
            get { return _scoreBoardCommand; }
            set { _scoreBoardCommand = value; }
        }

        public int YourScore
        {
            get { return _yourScore; }
            set { Assign("YourScore", ref _yourScore, value); }
        }


        public int BestScore
        {
            get { return _bestScore; }
            set { Assign("BestScore", ref _bestScore, value); }
        }

        public String BestUser
        {
            get { return _bestUser; }
            set { Assign("BestUser", ref _bestUser, value); }
        }
        #endregion


        public HomeViewModel()
        {
            _sendCommand = new DelegateCommand(ExecuteSendCommand, CanExecuteSendCommand);
            _actionCommand = new DelegateCommand(ExecuteActionCommand, CanExecuteActionCommand);
            _scoreBoardCommand = new DelegateCommand(ExecuteScoreBoardCommand);
            _actionSelected = 0;
            _commandEnable = true;

            BestUser = "toto";
            BestScore = 10;
            YourScore = 51;
            
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_DoWork;
            bw.RunWorkerAsync();
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            GameService gameService = new GameService(this);
            AppSettings appSettings = new AppSettings();
            RegisterObject register = appSettings.UserTokenSetting;
            gameService.Result(1, register.Round);
        }


        public void ExecuteSendCommand(object param)
        {
            SendSelectedAction();
        }


        #region commands
        public void ExecuteActionCommand(object param)
        {

            Debug.WriteLine(param.GetType());
            switch (param.ToString())
            {
                case "Stone":
                    ActionSelected = Game.ACTION_STONE;
                    ActionChoosed = "The choosen action is Stone";
                    break;

                case "Paper":
                    ActionSelected = Game.ACTION_PAPER;
                    ActionChoosed = "The choosen action is Paper";
                    break;

                case "Scissors":
                    ActionSelected = Game.ACTION_SCISSORS;
                    ActionChoosed = "The choosen action is Scissors";
                    break;
                default:
                    break;
            }
        }

        public bool CanExecuteActionCommand(object param)
        {
            return true;
        }

        public bool CanExecuteSendCommand(object param)
        {
            return (_actionSelected != 0 && CommandEnable);
        }

        public void ExecuteScoreBoardCommand(object param)
        {
            App.RootFrame.Navigate(new Uri("/Views/ScoreBoard.xaml", UriKind.Relative));
        }
        #endregion


        
        
        public async void SendSelectedAction()
        {

            CommandEnable = false;
            AppSettings appSettings = new AppSettings();
            RegisterObject register = appSettings.UserTokenSetting;
            CurrentRound = register.Round;
            CountDown = register.TimeStart;
            
            GameService gameService = new GameService(this);
            var a = await gameService.Play(1, register.Round, register.Token, _actionSelected);

        }


        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == "ActionSelected")
            {
                _sendCommand.RaiseCanExecuteChanged();
            }
        }

        public void ParseRuns(RunsObject runsObject)
        {
            Debug.WriteLine("ParseRuns");
            
            AppSettings appSettings = new AppSettings();
            User currentUser = appSettings.CurrentUser;

           appSettings.UserTokenSetting.Round = runsObject.Round;

           StringBuilder stBuilder = new StringBuilder();


            foreach (Run run  in runsObject.Runs)
            {
                String win = "win";
                if(run.Score == 0) win = "loose";

                stBuilder.Append(run.User.Name + " => " + win + " { "+ run.Value +" } \n");
                
            }
            //System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
            //            {
            //                MessageBox.Show(stBuilder.ToString());
            //            });
            CommandEnable = true;
            CurrentRound = appSettings.UserTokenSetting.Round;

            Random rnd = new Random();
            int action = rnd.Next(1, 3);
            _actionSelected = action;
            SendSelectedAction();

            
        }

        public void DisplayScore(ScoreObject scores)
        {
        
            BestScore = scores.Users[0].Score;
            BestUser = scores.Users[0].Name;


            User user = scores.Users.First(u => u.Name == "Momo");

            YourScore = user.Score;
            

            Debug.WriteLine(scores.Users[0].Name);
        }
    }
}
