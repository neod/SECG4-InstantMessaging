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
            string text;
            do
            {
                text = ConsoleHelpers.Readline(ConsoleColor.White, "You: ");
                //send text to server
            } while (text != "/back");
            //Application.GoTo<FriendList>();
            Application.GoBack();
        }
    }
}
