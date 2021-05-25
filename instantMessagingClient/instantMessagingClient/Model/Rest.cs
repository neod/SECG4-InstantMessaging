using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using instantMessagingCore.Models.Dto;
using RestSharp;

namespace instantMessagingClient.Model
{
    internal class Rest
    {
        private readonly RestClient client;
        private RestRequest request;

        public Rest()
        {
            this.client = new RestClient("https://localhost:44307");
        }

        public Rest(string baseUrl)
        {
            this.client = new RestClient(baseUrl);
        }

        private bool isConnectionStillValid()
        {
            bool Answer = true;
            int res = DateTime.Compare(Session.tokens.ExpirationDate, DateTime.Now);
            if (res > 0)
            {
                Answer = false;
            }

            return Answer;
        }

        /*private bool makeNewConnection()
        {
            if (!this.isConnectionStillValid())
            {
                //stocker login qlque part dabord puis refaire connection
            }
        }*/

        public IRestResponse Inscription(string _username, SecureString _password)
        {
            this.request = new RestRequest("api/Users/Inscription", DataFormat.Json);
            var param = new User(_username, _password);
            request.AddJsonBody(param);
            return this.client.Put(this.request);
        }

        public IRestResponse Login(string _username, SecureString _password)
        {
            this.request = new RestRequest("api/Users/Connexion", DataFormat.Json);
            var param = new User(_username, _password);
            request.AddJsonBody(param);
            return this.client.Post(this.request);
        }

        public IRestResponse getMyFriendList()
        {
            this.request = new RestRequest("/api/Friends", Method.GET);
            request.AddHeader("authorization", "Bearer " + Session.tokens.Token);
            return this.client.Get(this.request);
        }

        public IRestResponse getFriendRequests()
        {
            this.request = new RestRequest("api/Friends/pendingrequest", Method.GET);
            request.AddHeader("authorization", "Bearer " + Session.tokens.Token);
            return this.client.Get(this.request);
        }

        public IRestResponse SendFriendRequest(string FriendName)
        {
            this.request = new RestRequest("api/Friends/request/{friendName}", Method.GET);
            request.AddHeader("authorization", "Bearer " + Session.tokens.Token);
            request.AddUrlSegment("friendName", FriendName);
            return this.client.Get(this.request);
        }

        public IRestResponse ActionFriendRequest(Friends.Action requestAction, int _ID)
        {
            this.request = new RestRequest("api/Friends/pendingrequest/{senderId}/{requestAction}", Method.GET);
            request.AddHeader("authorization", "Bearer " + Session.tokens.Token);
            request.AddUrlSegment("senderId", _ID);
            request.AddUrlSegment("requestAction", requestAction);
            return this.client.Get(this.request);
        }
    }
}
