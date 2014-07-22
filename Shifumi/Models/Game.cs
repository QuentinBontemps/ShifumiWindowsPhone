using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shifumi.Models
{
    class Game
    {

        #region Constant

        public const int ACTION_STONE = 3;
        public const int ACTION_PAPER = 1;
        public const int ACTION_SCISSORS = 2;

        public const int ROUND_DELAY = 30000;


        #endregion

        #region Fields

        private int _id;
        private String _name;
        private DateTime _date;

        #endregion

        #region Accessors

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }
        
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        #endregion
    }
}
