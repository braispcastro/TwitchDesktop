using TwitchDesktop.Common;
using TwitchDesktop.Model.DAO;
using RestSharp;
using System;
using System.Net;

namespace TwitchDesktop.Model.Rest
{
    public class TwitchApi
    {
        public UserDAO GetUserInfo()
        {
            UserDAO result;

            try
            {
                var client = new RestClient(Constants.TwitchBaseUrl);
                var request = new RestRequest("/user", Method.GET);
                request.AddHeader("Accept", "application/vnd.twitchtv.v5+json");
                request.AddHeader("Authorization", string.Format("OAuth {0}", Configuration.AccessToken));
                request.AddHeader("Client-ID", Constants.TwitchClientId);

                var response = client.Execute<UserDAO>(request);
                result = response.StatusCode == HttpStatusCode.OK ? response.Data : null;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                result = null;
            }

            return result;
        }

        public StreamsDAO GetFollowedStreams()
        {
            StreamsDAO result;

            try
            {
                var client = new RestClient(Constants.TwitchBaseUrl);
                var request = new RestRequest("/streams/followed?limit={limit}", Method.GET);
                request.AddHeader("Accept", "application/vnd.twitchtv.v5+json");
                request.AddHeader("Authorization", string.Format("OAuth {0}", Configuration.AccessToken));
                request.AddHeader("Client-ID", Constants.TwitchClientId);
                request.AddUrlSegment("limit", "100");

                var response = client.Execute<StreamsDAO>(request);
                result = response.StatusCode == HttpStatusCode.OK ? response.Data : null;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                result = null;
            }

            return result;
        }
    }
}
