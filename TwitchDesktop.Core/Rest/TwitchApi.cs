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

        public StreamsDAO GetLiveFollowedStreams(string streamType, int offset)
        {
            StreamsDAO result;

            try
            {
                var client = new RestClient(Constants.TwitchBaseUrl);
                var request = new RestRequest("/streams/followed?stream_type={stream_type}&limit={limit}&offset={offset}", Method.GET);
                request.AddHeader("Accept", "application/vnd.twitchtv.v5+json");
                request.AddHeader("Authorization", string.Format("OAuth {0}", Configuration.AccessToken));
                request.AddHeader("Client-ID", Constants.TwitchClientId);
                request.AddUrlSegment("stream_type", streamType);
                request.AddUrlSegment("limit", "100");
                request.AddUrlSegment("offset", offset.ToString());

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
        
        public StreamsDAO GetTotalFollowedStreams(long userId, int offset)
        {
            StreamsDAO result;

            try
            {
                var client = new RestClient(Constants.TwitchBaseUrl);
                var request = new RestRequest("/users/{user_id}/follows/channels?offset={offset}", Method.GET);
                request.AddHeader("Accept", "application/vnd.twitchtv.v5+json");
                request.AddHeader("Authorization", string.Format("OAuth {0}", Configuration.AccessToken));
                request.AddHeader("Client-ID", Constants.TwitchClientId);
                request.AddUrlSegment("user_id", userId.ToString());
                request.AddUrlSegment("offset", offset.ToString());

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

        public TokenSignatureDAO GetTokenAndSignature(string channel)
        {
            TokenSignatureDAO result;

            try
            {
                var client = new RestClient(Constants.TwitchApiUrl);
                var request = new RestRequest("//channels/{channel_name}/access_token", Method.GET);
                request.AddHeader("Accept", "application/vnd.twitchtv.v5+json");
                request.AddHeader("Client-ID", Constants.TwitchClientId);
                request.AddUrlSegment("channel_name", channel);

                var response = client.Execute<TokenSignatureDAO>(request);
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
