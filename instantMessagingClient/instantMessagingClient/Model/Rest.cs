﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using EasyConsoleApplication;
using instantMessagingClient.Pages;
using instantMessagingCore.Models.Dto;
using Newtonsoft.Json;
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

        private static bool isTokenDateValid()
        {
            bool Answer = true;
            int res = DateTime.Compare(Session.tokens.ExpirationDate, DateTime.Now);
            if (res > 0)
            {
                Answer = false;
            }

            return Answer;
        }

        private static bool updateToken()
        {
            bool connected = true;
            if (!isTokenDateValid())
            {
                Rest rest = new Rest();
                var response = rest.Login(Session.sessionUsername, Session.sessionPassword);
                if (response != null)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var responseContent = response.Content;
                        Tokens deserializeObject = JsonConvert.DeserializeObject<Tokens>(responseContent);
                        Session.tokens = deserializeObject;
                    }
                    else
                    {
                        Session.tokens = null;
                        connected = false;
                    }
                }
                else
                {
                    connected = false;
                }
            }

            return connected;
        }

        private static bool isValid()
        {
            if (updateToken()) return true;
            ConsoleHelpers.WriteRed("Invalid token. You will be disconnected.");
            ConsoleHelpers.HitEnterToContinue();
            LoggedInHomePage.clickDisconnect();
            return false;
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

        public IRestResponse getMyFriendList()
        {
            IRestResponse rep = null;
            if (isValid())
            {
                this.request = new RestRequest("/api/Friends", Method.GET);
                request.AddHeader("authorization", "Bearer " + Session.tokens.Token);
                rep = this.client.Get(this.request);
            }

            return rep;
        }

        public IRestResponse getFriendRequests()
        {
            IRestResponse rep = null;
            if (isValid())
            {
                this.request = new RestRequest("api/Friends/pendingrequest", Method.GET);
                request.AddHeader("authorization", "Bearer " + Session.tokens.Token);
                rep = this.client.Get(this.request);
            }

            return rep;
        }

        public IRestResponse SendFriendRequest(string FriendName)
        {
            IRestResponse rep = null;
            if (isValid())
            {
                this.request = new RestRequest("api/Friends/request/{friendName}", Method.GET);
                request.AddHeader("authorization", "Bearer " + Session.tokens.Token);
                request.AddUrlSegment("friendName", FriendName);
                rep = this.client.Get(this.request);
            }

            return rep;
        }

        public IRestResponse ActionFriendRequest(Friends.Action requestAction, int _ID)
        {
            IRestResponse rep = null;
            if (isValid())
            {
                this.request = new RestRequest("api/Friends/pendingrequest/{senderId}/{requestAction}", Method.GET);
                request.AddHeader("authorization", "Bearer " + Session.tokens.Token);
                request.AddUrlSegment("senderId", _ID);
                request.AddUrlSegment("requestAction", requestAction);
                rep = this.client.Get(this.request);
            }

            return rep;
        }
    }
}
