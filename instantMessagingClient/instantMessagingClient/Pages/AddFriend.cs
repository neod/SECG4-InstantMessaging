using System;
using EasyConsoleApplication;
using EasyConsoleApplication.Pages;

namespace instantMessagingClient.Pages
{
    public class AddFriend : Page
    {
        public AddFriend()
        {
            ConsoleHelpers.WriteGreen("If you want to go back, type '/back'");
            string Name = ConsoleHelpers.Readline(ConsoleColor.White, "name: ");
            if (Name == "/back")
            {
                Application.GoTo<FriendList>();
            }
            //do while name exists puis go back to LoggedInHomePage (psk le goback va pas fonctionner si on se mets sur friendlist)
        }
    }
}
