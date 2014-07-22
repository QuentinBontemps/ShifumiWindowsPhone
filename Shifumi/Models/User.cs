using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shifumi.Models
{
    public class User
    {
        #region Fields
        private int _id;
        private String _name;
        private String _token;
        private int _score;
        private String _udid;

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

        public String Token
        {
            get { return _token; }
            set { _token = value; }
        }

        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }

        public String UDID
        {
            get { return _udid; }
            set { _udid = value; }
        }
              
        #endregion
    }
}
