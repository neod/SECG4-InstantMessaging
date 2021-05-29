using System;
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
                string ipv4 = new WebClient().DownloadString("http://ipv4.icanhazip.com").Replace("\\r\\n", "").Replace("\\n", "").Trim();
                string ipv6 = new WebClient().DownloadString("http://ipv6.icanhazip.com").Replace("\\r\\n", "").Replace("\\n", "").Trim();
                Random random = new Random();
                var myPort = Convert.ToUInt16(random.Next(49153, 65534));
                var param = new Peers(Session.tokens.UserId, ipv4, ipv6, myPort, DateTime.Now);
                rest.postPeers(param);
                Session.communication = new TCP(ipv4, Convert.ToString((int)myPort));
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
