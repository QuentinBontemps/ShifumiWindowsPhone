using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shifumi.Models
{
    public class RegisterObject
    {
        private String _token;

        public String Token
        {
            get { return _token; }
            set { _token = value; }
        }

        private int _round;

        public int Round
        {
            get { return _round; }
            set { _round = value; }
        }

        private int _timeStart;

        public int TimeStart
        {
            get { return _timeStart; }
            set { _timeStart = value; }
        }
        
        
    }
}
