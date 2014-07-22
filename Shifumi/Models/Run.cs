using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shifumi.Models
{
    class Run
    {

        #region Fields
        private int _id;
        private int _turn;
        private int _score;
        private int _value;

        private Game _game;
        private User _user;

        #endregion

        #region Accessors
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int Turn
        {
            get { return _turn; }
            set { _turn = value; }
        }

        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }
        
        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public Game Game
        {
            get { return _game; }
            set { _game = value; }
        }

        public User User
        {
            get { return _user; }
            set { _user = value; }
        }
        #endregion
        
    }
}