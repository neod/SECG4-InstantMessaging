using System;
using EasyConsoleApplication;
using EasyConsoleApplication.Menus;
using EasyConsoleApplication.Pages;
using instantMessagingClient.Model;

namespace instantMessagingClient.Pages
{
    public class LoggedInHomePage : Page
    {
        public LoggedInHomePage()
        {
            Title = "Home: " + Session.sessionUsername;
            TitleColor = ConsoleColor.Green;
            Body = "-----";
            MenuItems.Add(new MenuItem("Friends list", clickFriendsList));
            MenuItems.Add(new MenuItem("Delete account", deleteAccount)
            {
                Color = ConsoleColor.Red
            });
            MenuItems.Add(new MenuItem("Disconnect", clickDisconnect)
            {
                Color = ConsoleColor.Yellow
            });
        }

        private void clickFriendsList()
        {
            /*Console.WriteLine("Session token: " + Session.token);
            ConsoleHelpers.HitEnterToContinue();*/
            Application.GoTo<FriendList>();
        }

        private void clickDisconnect()
        {
            Session.sessionPassword = null;
            Session.sessionUsername = null;
            Session.tokens = null;
            Application.GoTo<LoginPage>();
        }

        private void deleteAccount()
        {
            ConsoleHelpers.AskToUserYesNoQuestion(ConsoleColor.Red, "Are you sure about that?\n");
        }
    }
}
