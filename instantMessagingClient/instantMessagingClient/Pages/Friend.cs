using System;
using EasyConsoleApplication;
using EasyConsoleApplication.Menus;
using EasyConsoleApplication.Pages;

namespace instantMessagingClient.Pages
{
    public class Friend : Page
    {
        private readonly int _ID;

        public Friend(int ID)
        {
            _ID = ID;

            Title = "Friend" + _ID;
            TitleColor = ConsoleColor.Green;
            Body = "-----";
            MenuItems.Add(new MenuItem("Message", () => Application.GoTo<MessageFriend>(_ID)));
            MenuItems.Add(new MenuItem("Delete from friendlist", DeleteFriend){ Color = ConsoleColor.Red });
            MenuItems.Add(new MenuItem("Go back", Application.GoBack)
            {
                Color = ConsoleColor.Yellow
            });
        }

        private static void DeleteFriend()
        {
            ConsoleKeyInfo yesOrNo = ConsoleHelpers.AskToUserYesNoQuestion(ConsoleColor.Red, "Are you sure about that?");
            if (yesOrNo.Key == ConsoleKey.Y)
            {
                //delete friend and go to friendlist
                Application.GoTo<FriendList>();
            }
        }
    }
}
