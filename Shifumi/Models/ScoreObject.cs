using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shifumi.Models
{
    class ScoreObject
    {
        private User[] _users;

        public User[] Users
        {
            get { return _users; }
            set { _users = value; }
        }
        
    }
}
