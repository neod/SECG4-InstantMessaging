using System;
using EasyConsoleApplication;
using EasyConsoleApplication.Pages;
using instantMessagingClient.Model;
using RestSharp;

namespace instantMessagingClient.Pages
{
    public class AddFriend : Page
    {
        public AddFriend()
        {
            ConsoleHelpers.WriteGreen("If you want to go back, type '/back'");
            string Name;
            bool nameExists;
            Rest rest = new Rest();
            do
            {
                Name = ConsoleHelpers.Readline(ConsoleColor.White, "name: ");
                IRestResponse rep = rest.SendFriendRequest(Name);
                nameExists = rep.IsSuccessful;
            } while (Name != "/back" && !nameExists);

            //ajouter l'ami si existant

            Application.GoTo<FriendList>();
        }
    }
}
