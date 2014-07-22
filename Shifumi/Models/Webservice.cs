using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Shifumi.Models
{
    abstract class Webservice
    {
        private const String _scheme = "http";
        private const String _host  = "10.3.0.13";
        private const String _apiPath = "app.php/api";
        private const String _format = "json";
        private WebClient _webClient;
        Action<string, HttpStatusCode?> callback;
        HttpWebRequest request;

        public Webservice()
        {
            this.WebClient = new WebClient();
        }

        public WebClient WebClient
        {
            get { return _webClient; }
            set { _webClient = value; }
        }
        

        public String Scheme
        {
            get { return _scheme; }
        }

        public String Format
        {
            get { return _format; }
        }
        

	    protected String Host
	    {
            get { return _host; }
	    }

        protected String ApiPath
        {
            get { return _apiPath; }
        }

        protected String getApiPath()
        {
            return String.Format("{0}://{1}/{2}", this.Scheme, this.Host, this.ApiPath);
        }

        private async void AsyncResponce (IAsyncResult result) {
            try
            {
                /* Get the response and the response stream, then execute the callback */
                using (HttpWebResponse response = request.EndGetResponse(result) as HttpWebResponse)
                {
                    using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        StringBuilder rawResponse = new StringBuilder();
                        String rawLine;

                        /* Read and display lines from the file until the end of the file is reached */
                        while ((rawLine = streamReader.ReadLine()) != null)
                        {
                            rawResponse.Append (rawLine);
                        }

                        /* Execute with the result string and the status code */
                        callback(rawResponse.ToString(), response.StatusCode);
                    }
                }
            }
            catch (WebException exception)
            {
                /* Invoke the callback with a null response (it failed) */
                try
                {
                    callback(null, (exception.Response as HttpWebResponse).StatusCode);
                }
                catch
                {
                    /* The web exception response is null or malformed. */
                    callback(null, null);
                }
            }
            catch (Exception exception)
            {
                /* The web exception response is null or malformed. */
                callback(null, null);
            }
        }

        //public void Send(HttpWebRequest request, Action<string, HttpStatusCode?> callback)
        //{
        //    try
        //    {
        //        this.callback = callback;
        //        this.request = request;
        //        this.request.BeginGetResponse(AsyncResponce, request);
                
        //    }
        //    catch (WebException exception)
        //    {
        //        /* Invoke the callback with a null response (it failed) */
        //        try
        //        {
        //            callback(null, (exception.Response as HttpWebResponse).StatusCode);
        //        }
        //        catch
        //        {
        //            /* The web exception response is null or malformed. */
        //            callback(null, null);
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        /* The web exception response is null or malformed. */
        //        callback(null, null);
        //    }
        //}

        public void Send(HttpWebRequest request, Action<string, HttpStatusCode?> callback)
        {
            try
            {
                request.BeginGetResponse((result) =>
                {
                    try
                    {
                        /* Get the response and the response stream, then execute the callback */
                        using (HttpWebResponse response = request.EndGetResponse(result) as HttpWebResponse)
                        {
                            using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                            {
                                string rawResponse = null, rawLine;

                                /* Read and display lines from the file until the end of the file is reached */
                                while ((rawLine = streamReader.ReadLine()) != null)
                                {
                                    rawResponse += rawLine;
                                }

                                /* Execute with the result string and the status code */
                                callback(rawResponse, response.StatusCode);
                            }
                        }
                    }
                    catch (WebException exception)
                    {
                        /* Invoke the callback with a null response (it failed) */
                        try
                        {
                            callback(null, (exception.Response as HttpWebResponse).StatusCode);
                        }
                        catch
                        {
                            /* The web exception response is null or malformed. */
                            callback(null, null);
                        }
                    }
                    catch (Exception exception)
                    {
                        /* The web exception response is null or malformed. */
                        callback(null, null);
                    }
                }, request);
            }
            catch (WebException exception)
            {
                /* Invoke the callback with a null response (it failed) */
                try
                {
                    callback(null, (exception.Response as HttpWebResponse).StatusCode);
                }
                catch
                {
                    /* The web exception response is null or malformed. */
                    callback(null, null);
                }
            }
            catch (Exception exception)
            {
                /* The web exception response is null or malformed. */
                callback(null, null);
            }
        }
    }
}
