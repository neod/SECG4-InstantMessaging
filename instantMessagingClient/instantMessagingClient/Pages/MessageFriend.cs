using System;
using System.Threading.Tasks;
using EasyConsoleApplication;
using EasyConsoleApplication.Pages;
using instantMessagingClient.Model;

namespace instantMessagingClient.Pages
{
    public class MessageFriend : Page
    {
        public MessageFriend(int ID)
        {
            Session.isOnMessagingPage = true;

            Console.Clear();
            const string backCommand = "/back";
            CurrentChat chat = new CurrentChat(ID, backCommand);

            Session.communication.friendsHost = "127.0.0.1";
            Session.communication.friendsPort = "50000";
            Session.communication.startClient();

            chat.display();
            chat.readLine();

            chat.onTextChange += (sender, e) =>
            {
                chat.display();
            };
            while (chat.isRunning);
            Session.isOnMessagingPage = false;
            Application.GoBack();
        }
    }
}
