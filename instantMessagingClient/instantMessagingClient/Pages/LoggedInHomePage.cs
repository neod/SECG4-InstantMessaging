using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyConsoleApplication;
using EasyConsoleApplication.Menus;
using EasyConsoleApplication.Pages;

namespace instantMessagingClient.Pages
{
    public class LoggedInHomePage : Page
    {
        public LoggedInHomePage()
        {
            Title = "Home";
            TitleColor = ConsoleColor.Green;
            Body = "-----";
            MenuItems.Add(new MenuItem("Friends list", () => Application.GoTo<FriendList>()));
            MenuItems.Add(new MenuItem("Delete account", deleteAccount)
            {
                Color = ConsoleColor.Red
            });
            MenuItems.Add(new MenuItem("Disconnect", Application.Exit)
            {
                Color = ConsoleColor.Yellow
            });
        }

        private void deleteAccount()
        {
            ConsoleHelpers.AskToUserYesNoQuestion(ConsoleColor.Red, "Are you sure about that?\n");
        }
    }
}
