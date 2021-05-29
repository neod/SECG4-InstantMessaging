using System;
using System.Collections.Generic;
using System.Net;
using EasyConsoleApplication;
using EasyConsoleApplication.Menus;
using EasyConsoleApplication.Pages;
using instantMessagingClient.Model;
using instantMessagingClient.P2P;
using instantMessagingCore.Models.Dto;

namespace instantMessagingClient.Pages
{
    public class LoggedInHomePage : Page
    {
        public static IPAddress GetMyIp()
        {
            List<string> services = new List<string>()
            {
                "https://ipv4.icanhazip.com",
                "https://api.ipify.org",
                "https://ipinfo.io/ip",
                "https://checkip.amazonaws.com",
                "https://wtfismyip.com/text",
                "http://icanhazip.com"
            };
            using (var webclient = new WebClient())
                foreach (var service in services)
                {
                    try { return IPAddress.Parse(webclient.DownloadString(service)); } catch { }
                }
            return null;
        }

        public LoggedInHomePage()
        {
            Title = "Home: " + Session.sessionUsername + "(" + Session.tokens.UserId + ")";
            TitleColor = ConsoleColor.Green;
            Body = "-----";
            MenuItems.Add(new MenuItem("Friends list", clickFriendsList));
            MenuItems.Add(new MenuItem("Disconnect", clickDisconnect)
            {
                Color = ConsoleColor.Yellow
            });

            if (Session.hasAlreadyStarted == false)
            {
                Rest rest = new Rest();
                IPAddress ipv4 = GetMyIp();
                IPAddress ipv6 = GetMyIp();
                Random random = new Random();
                var myPort = Convert.ToUInt16(random.Next(49153, 65534));
                var param = new Peers(Session.tokens.UserId, ipv4.ToString(), ipv6.ToString(), myPort, DateTime.Now);
                rest.postPeers(param);
                Session.communication = new TCP(ipv4.ToString(), Convert.ToString((int)myPort));
                Session.communication.startListener();
                Session.hasAlreadyStarted = true;
            }
        }

        private void clickFriendsList()
        {
            Application.GoTo<FriendList>();
        }

        public static void clickDisconnect()
        {
            Session.sessionPassword = null;
            Session.sessionUsername = null;
            Session.tokens = null;
            Session.communication = null;
            Session.isOnMessagingPage = false;
            Application.GoTo<Home>();
        }
    }
}
