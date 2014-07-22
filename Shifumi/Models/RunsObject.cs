using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shifumi.Models
{
    class RunsObject
    {
        private Run[] _runs;

        public Run[] Runs
        {
            get { return _runs; }
            set { _runs = value; }
        }

        private int _round;

        public int Round
        {
            get { return _round; }
            set {
                _round = value;
                AppSettings appSettings = new AppSettings();
                appSettings.UserTokenSetting.Round = value;
           
            }
        }

        private int _round_countdown;

        public int Round_countdown
        {
            get { return _round_countdown; }
            set {
                _round_countdown = value;
                AppSettings appSettings = new AppSettings();
                appSettings.UserTokenSetting.TimeStart = value;
            }
        }
        
        
        
    }
}
