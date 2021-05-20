using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                Application.GoTo<LoggedInHomePage>();
            }
            // /exit pour goback??
        }
    }
}
