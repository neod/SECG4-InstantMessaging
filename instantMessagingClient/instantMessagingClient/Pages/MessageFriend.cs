using System;
using EasyConsoleApplication;
using EasyConsoleApplication.Pages;

namespace instantMessagingClient.Pages
{
    public class MessageFriend : Page
    {
        private readonly int _ID;

        public MessageFriend(int ID)
        {
            _ID = ID;
            Console.Clear();
            ConsoleHelpers.WriteGreen("If you want to go back, type '/back'");
            string text = ConsoleHelpers.Readline(ConsoleColor.White, "You: ");
            if (text == "/back")
            {
                Application.GoTo<FriendList>();
            }
            // /exit pour goback??
        }
    }
}
