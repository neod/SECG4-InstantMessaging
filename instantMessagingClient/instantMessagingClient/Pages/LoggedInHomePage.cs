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
            Title = "Home";
            TitleColor = ConsoleColor.Green;
            Body = "-----";
            MenuItems.Add(new MenuItem("Friends list", clickFriendsList));
            MenuItems.Add(new MenuItem("Delete account", deleteAccount)
            {
                Color = ConsoleColor.Red
            });
            MenuItems.Add(new MenuItem("Disconnect", Application.Exit)
            {
                Color = ConsoleColor.Yellow
            });
        }

        private void clickFriendsList()
        {
            /*Console.WriteLine("Session Token: " + Session.Token);
            ConsoleHelpers.HitEnterToContinue();*/
            Application.GoTo<FriendList>();
        }

        private void deleteAccount()
        {
            ConsoleHelpers.AskToUserYesNoQuestion(ConsoleColor.Red, "Are you sure about that?\n");
        }
    }
}
