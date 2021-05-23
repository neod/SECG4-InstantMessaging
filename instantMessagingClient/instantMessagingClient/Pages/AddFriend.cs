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
            string Name;
            bool nameExists = false;
            do
            {
                Name = ConsoleHelpers.Readline(ConsoleColor.White, "name: ");
                nameExists = true;//faire appel API
            } while (Name != "/back" && !nameExists);

            //ajouter l'ami si existant

            Application.GoTo<FriendList>();
        }
    }
}
