using System;
using EasyConsoleApplication;
using EasyConsoleApplication.Menus;
using EasyConsoleApplication.Pages;
using instantMessagingClient.Model;
using instantMessagingClient.P2P;

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

            Session.communication = new TCP("127.0.0.1", "60000");
            Session.communication.startListener();
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
