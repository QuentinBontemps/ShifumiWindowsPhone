using Shifumi.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace Shifumi.Models
{
    class UserService : Webservice
    {

        private const String _registerPathFormat = "register/{0}/{1}.{2}";

        public string RegisterPathFormat
        {
            get { return _registerPathFormat; }
        }

       
        public void Register(String pseudo)
        {
            String absolutePath = String.Format("{0}/{1}", this.getApiPath(), String.Format(this.RegisterPathFormat, pseudo, Tools.IdTel(), this.Format));


            HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(new Uri(absolutePath));
            webRequest.Method = "PUT";
            webRequest.ContentType = "application/json";

            RegisterObject token = null;
            Send(webRequest, (result, code) =>
            {
                if (result != null)
                {
                    token = Newtonsoft.Json.JsonConvert.DeserializeObject<RegisterObject>(result);
                    AppSettings appSettings = new AppSettings();
                    appSettings.UserTokenSetting = token;


                    User u = new User();
                    u.Name = pseudo;
                    u.Token = token.Token;
                    
                    appSettings.CurrentUser = u;


                    Debug.WriteLine("################################## REGISTER FINISH #########################");
                }
            });

        }

        public void WinPoint(String pseudo)
        {
            String absolutePath = String.Format("{0}/{1}", this.getApiPath(), String.Format(this.RegisterPathFormat, pseudo, Tools.IdTel(), this.Format));


            HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(new Uri(absolutePath));
            webRequest.Method = "PUT";

            RegisterObject token = null;
            Send(webRequest, (result, code) =>
            {
                if (result != null)
                {
                    token = Newtonsoft.Json.JsonConvert.DeserializeObject<RegisterObject>(result);
                    AppSettings appSettings = new AppSettings();
                    appSettings.UserTokenSetting = token;

                    Debug.WriteLine("################################## REGISTER FINISH #########################");
                }
            });
        }
    }
}
