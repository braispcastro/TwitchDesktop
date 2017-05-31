using TwitchDesktop.Common;
using TwitchDesktop.Model.DAO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace TwitchDesktop.Core.Server
{
    public class Program
    {
        public delegate void FinishAuthEventHandler();
        public FinishAuthEventHandler FinishAuthEvent;
        private void OnFinishAuthEvent()
        {
            FinishAuthEvent?.Invoke();
        }

        public void DoWork()
        {
            //Start the webserver to listen for the Twitch auth callback
            //The second parameter is called a prefix, this needs to match your Twitch Application Redirect URI
            //Make sure the URL ends in a /
            try
            {
                WebServer ws = new WebServer(SendResponse, Constants.TwitchRedirectUri + "/");
                ws.Run();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string SendResponse(HttpListenerRequest request)
        {
            TwitchAuthorizationApi(request.QueryString.GetValues("code")[0]);

            //Show when user is authorized
            OnFinishAuthEvent();
            return "<HTML><BODY>Success!<br></BODY></HTML>";
        }


        public void TwitchAuthorizationApi(string code)
        {
            HttpWebRequest myWebRequest = null;
            ASCIIEncoding encoding = new ASCIIEncoding();
            Dictionary<string, string> postDataDictionary = new Dictionary<string, string>();

            //POST data
            postDataDictionary.Add("client_id", Constants.TwitchClientId);
            postDataDictionary.Add("client_secret", Constants.TwitchClientSecret);
            postDataDictionary.Add("grant_type", "authorization_code");
            postDataDictionary.Add("redirect_uri", Constants.TwitchRedirectUri);
            postDataDictionary.Add("state", "qwerty");
            postDataDictionary.Add("code", code);

            string postData = "";

            foreach (KeyValuePair<string, string> kvp in postDataDictionary)
            {
                postData += HttpUtility.UrlEncode(kvp.Key) + "=" + HttpUtility.UrlEncode(kvp.Value) + "&";
            }

            //We need the POST data as a byte array, using ASCII encoding to keep things simple

            byte[] byte1 = encoding.GetBytes(postData);

            //OK set up our request for the final step in the Authorization Code Flow
            //This is the destination URI as described in https://dev.twitch.tv/docs/v5/guides/authentication/

            myWebRequest = WebRequest.CreateHttp("https://api.twitch.tv/kraken/oauth2/token");

            //This request is a POST with the required content type
            myWebRequest.Method = "POST";
            myWebRequest.ContentType = "application/x-www-form-urlencoded";

            //Set the request length based on our byte array
            myWebRequest.ContentLength = byte1.Length;

            //POST
            Stream postStream = null;

            try
            {
                //Set up the request and write the data this should complete the POST
                postStream = myWebRequest.GetRequestStream();
                postStream.Write(byte1, 0, byte1.Length);
            }
            catch (Exception ex)
            {
                //We should log any exception here but I am just going to supress them for this sample
                throw ex;
            }
            finally
            {
                postStream.Close();
            }

            //response to POST
            Stream responseStream = null;
            StreamReader responseStreamReader = null;
            WebResponse response = null;
            string jsonResponse = null;

            try
            {
                //Wait for the response from the POST above and get a stream with the data
                response = myWebRequest.GetResponse();
                responseStream = response.GetResponseStream();

                //Read the response, if everything worked we'll have our JSON encoded oauth token
                responseStreamReader = new StreamReader(responseStream);
                jsonResponse = responseStreamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                //We should log any exception here but I am just going to supress them for this sample
                throw ex;
            }
            finally
            {
                responseStreamReader.Close();
                responseStream.Close();
                response.Close();
            }

            //We got the jsonResponse from Twitch let's Deserialize it,
            //I'm using Newtonsoft - Install-Package Newtonsoft.Json -Version 9.0.1
            //Class for deserializing is defined below
            AuthResponse myAuthResponse = null;

            try
            {
                myAuthResponse = JsonConvert.DeserializeObject<AuthResponse>(jsonResponse);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //Save the access token
            Configuration.AccessToken = myAuthResponse.access_token;
        }
    }
}
