using System;
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
            MenuItems.Add(new MenuItem("Accept", () =>
            {
                Application.GoTo<FriendList>();
            }));
            MenuItems.Add(new MenuItem("Decline", Decline){ Color = ConsoleColor.Red});
            MenuItems.Add(new MenuItem("Go back", Application.GoBack)
            {
                Color = ConsoleColor.Yellow
            });
        }

        private static void Decline()
        {
            ConsoleKeyInfo yesOrNo = ConsoleHelpers.AskToUserYesNoQuestion(ConsoleColor.Red, "Are you sure about that?");
            if (yesOrNo.Key == ConsoleKey.Y)
            {
                //decline and go to friendlist
                Application.GoTo<FriendList>();
            }
        }
    }
}
