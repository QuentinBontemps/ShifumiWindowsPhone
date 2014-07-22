using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shifumi.Models
{
    class RunService : Webservice
    {

        private const String _runPathFormat = "game/{0}/run/{1}.{2}";

        public String MyProperty
        {
            get { return _runPathFormat; }
        }


        

    }
}
