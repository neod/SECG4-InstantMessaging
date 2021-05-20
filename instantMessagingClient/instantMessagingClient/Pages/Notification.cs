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
    public class Notification : Page
    {
        private readonly int _ID;
        public Notification(int ID)
        {
            this._ID = ID;//notification id

            Title = "User" + _ID;
            TitleColor = ConsoleColor.Green;
            Body = "-----";
            MenuItems.Add(new MenuItem("Accept", () => Console.WriteLine("add")));
            MenuItems.Add(new MenuItem("Decline", () =>
                ConsoleHelpers.AskToUserYesNoQuestion(ConsoleColor.Red, "Are you sure about that?")
            )
            {
                Color = ConsoleColor.Red
            });
            MenuItems.Add(new MenuItem("Go back", Application.GoBack)
            {
                Color = ConsoleColor.Yellow
            });
        }
    }
}
