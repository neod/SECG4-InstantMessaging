using System;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Text;
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

        public IRestResponse SendFriendRequest(string FriendName)
        {
            this.request = new RestRequest("api/Friends/request/", DataFormat.Json);
            request.AddJsonBody(FriendName);
            return this.client.Get(this.request);
        }
    }
}
