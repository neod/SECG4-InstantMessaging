using System;
using EasyConsoleApplication;
using EasyConsoleApplication.Pages;
using instantMessagingClient.Model;

namespace instantMessagingClient.Pages
{
    public class MessageFriend : Page
    {
        private static int _ID;

        public MessageFriend(int ID)
        {
            _ID = ID;
            Session.isOnMessagingPage = true;

            Console.Clear();
            const string backCommand = "/back";
            ConsoleHelpers.WriteGreen("If you want to go back, type '"+ backCommand + "'");

            Session.communication.friendsHost = "127.0.0.1";
            Session.communication.friendsPort = "60000";
            Session.communication.startClient();

            string rawString;
            do
            {
                rawString = ConsoleHelpers.Readline(ConsoleColor.White, "You: ");
                Session.communication.sendMessage(rawString);
                /*if (string.IsNullOrEmpty(rawString) || rawString == backCommand) continue;
                byte[] text = Encoding.ASCII.GetBytes(rawString);*/
            } while (rawString != backCommand);
            Session.isOnMessagingPage = false;
            Application.GoBack();
        }
    }
}
