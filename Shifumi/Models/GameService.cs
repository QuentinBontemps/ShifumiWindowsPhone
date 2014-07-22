using Shifumi.Core;
using Shifumi.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shifumi.Models
{
    class GameService : Webservice
    {
        private int _actionSelected;
        private string _userToken;
        private int _numGame;
        private int _numTurn;
        private HomeViewModel _viewModel;

        private const String _resultPathFormat = "game/{0}/round/{1}.{2}?token={3}";

        public String ResultPathFormat
        {
            get { return _resultPathFormat; }
        }


        private const String _playPathFormat = "game/{0}/round/{1}.{2}";

        private const String _scorePathFormat = "score.{0}";

        public String ScorePathFormat
        {
            get { return _scorePathFormat; }
        }
        

        public String PayPathFormat
        {
            get { return _playPathFormat; }
        }


        public GameService(HomeViewModel viewModel)
        {
            _viewModel = viewModel;    
        }

        public async Task<bool> Play(int numGame, int numTurn, string _1token, int action)
        {
            
            String absolutePath = String.Format("{0}/{1}", this.getApiPath(), String.Format(PayPathFormat, numGame, numTurn, this.Format));
            AppSettings appSettings = new AppSettings();
            RegisterObject register = appSettings.UserTokenSetting;
            String token = register.Token;
            _actionSelected = action;
            _userToken = token;
            _numGame = numGame;
            _numTurn = numTurn;


            HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(new Uri(absolutePath));
            webRequest.Method = "PUT";
            webRequest.ContentType = "application/x-www-form-urlencoded";

            webRequest.BeginGetRequestStream(new AsyncCallback(GetPlayRequestStreamCallback), webRequest);
            
            return true;
        }

        void GetPlayRequestStreamCallback(IAsyncResult callbackResult)
        {
            HttpWebRequest myRequest = (HttpWebRequest)callbackResult.AsyncState;
            // End the stream request operation
            Stream postStream = myRequest.EndGetRequestStream(callbackResult);

            // Create the post data
            string postString = string.Format("token={0}&shifu={1}", _userToken, _actionSelected);
            byte[] byteArray = Encoding.UTF8.GetBytes(postString);

            // Add the post data to the web request
            postStream.Write(byteArray, 0, byteArray.Length);
            postStream.Close();

            // Start the web request
            myRequest.BeginGetResponse(new AsyncCallback(GetPlayResponsetStreamCallback), myRequest);
        }

        void GetPlayResponsetStreamCallback(IAsyncResult callbackResult)
        {
            HttpWebRequest request = (HttpWebRequest)callbackResult.AsyncState;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(callbackResult);
                //using (StreamReader httpWebStreamReader = new StreamReader(response.GetResponseStream()))
                //{
                //}
                int statusCode = (int)response.StatusCode;
                Debug.WriteLine("Status Code : " + statusCode  + " -- response Uri : "+response.ResponseUri);
                switch (statusCode)
                {
                    case 201:
                    case 200:
                        
                        //var thread = new System.Threading.Thread(getResult);
                        //thread.Start();

                        break;

                    case 203:
                        //ALREADY PLAYED
                        break;
                                           
                    default:
                        break;
                }

                response.Close();
            }
            catch (Exception e)
            {
            }
        }

        private void getResult()
        {
            Result(_numGame, _numTurn);
        }

        public void Result(int numGame, int _numTurn)
        {
            while (System.Threading.Thread.CurrentThread.IsAlive)
            {
                try
                {


                    AppSettings appSettings = new AppSettings();
                    RegisterObject register = appSettings.UserTokenSetting;
                    String token = register.Token;
                    int numTurn = register.Round;
                    Debug.WriteLine("Game :" + numGame + "numTurn : " + numTurn);

                    String absolutePath = String.Format("{0}/{1}", this.getApiPath(), String.Format(ResultPathFormat, numGame, numTurn, this.Format, token));

                    HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(new Uri(absolutePath));
                    webRequest.Method = "GET";

                    Run[] runsResult = null;

                    Send(webRequest, (result, code) =>
                    {
                        Debug.WriteLine(result);

                        if (result != null)
                        {
                            RunsObject runsObject = Newtonsoft.Json.JsonConvert.DeserializeObject<RunsObject>(result);

                            _viewModel.CountDown = runsObject.Round_countdown;

                            Debug.WriteLine("Count down : " + runsObject.Round_countdown);
                            if (runsObject.Runs != null)
                            {
                                runsResult = runsObject.Runs;
                                _viewModel.ParseRuns(runsObject);
                            }


                        }
                        
                        Debug.WriteLine("getResult" + numTurn);
                    });
                }
                catch
                {

                }

                GetScores();
                System.Threading.Thread.Sleep(2000);
            }

        }


        public void GetScores()
        {
            String absolutePath = String.Format("{0}/{1}", this.getApiPath(), String.Format(ScorePathFormat, this.Format));

            HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(new Uri(absolutePath));
            webRequest.Method = "GET";

            Send(webRequest, (result, code) =>
            {

                if (result != null)
                {
                    ScoreObject scoreObject = Newtonsoft.Json.JsonConvert.DeserializeObject<ScoreObject>(result);

                    _viewModel.DisplayScore(scoreObject);
                    
                }
            });
        }
    }
}
