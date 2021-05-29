using System;
using System.Threading.Tasks;
using EasyConsoleApplication;
using EasyConsoleApplication.Pages;
using instantMessagingClient.Model;
using instantMessagingCore.Models.Dto;
using Newtonsoft.Json;

namespace instantMessagingClient.Pages
{
    public class MessageFriend : Page
    {
        public MessageFriend(int ID)
        {
            Session.isOnMessagingPage = true;

            Console.Clear();
            const string backCommand = "/back";

            //get public key of friend
            Rest rest = new Rest();
            var reponse = rest.getPublicKeyFriend(ID);
            if (reponse != null)
            {
                if (reponse.IsSuccessful)
                {
                    var responseContent = reponse.Content;
                    PublicKeys friendsPublicKeys = JsonConvert.DeserializeObject<PublicKeys>(responseContent);

                    CurrentChat chat = new CurrentChat(ID, backCommand, friendsPublicKeys);

                    Session.communication.friendsHost = "127.0.0.1";
                    Session.communication.friendsPort = "50000";
                    Session.communication.startClient();

                    chat.display();
                    chat.readLine();

                    ChatManager cm = ChatManager.getInstance();
                    cm.AddEvent(ID, (sender, e) =>
                    {
                        chat.display();
                    });
                    cm.AddEvent(Session.tokens.UserId, (sender, e) =>
                    {
                        chat.display();
                    });
                    while (chat.isRunning) ;

                    cm.ClearEvent(ID);
                    cm.ClearEvent(Session.tokens.UserId);

                    Session.isOnMessagingPage = false;
                    Application.GoBack();
                }
            }
            else
            {
                ConsoleHelpers.WriteRed("Error retrievings friends public key");
                ConsoleHelpers.HitEnterToContinue();
                Application.GoBack();
            }
        }
    }
}
