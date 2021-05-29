using System;
using System.Threading.Tasks;
using EasyConsoleApplication;
using EasyConsoleApplication.Pages;
using instantMessagingClient.JsonRest;
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
            var responsePeers = rest.getPeers(ID);

            postKey friendsPublicKeys = JsonConvert.DeserializeObject<postKey>(reponse.Content);
            friendsPublicKeys.ToStringNormal();
            Peers friendPeer = JsonConvert.DeserializeObject<Peers>(responsePeers.Content);

            CurrentChat chat = new CurrentChat(ID, backCommand, friendsPublicKeys);

            Session.communication.friendsHost = friendPeer.Ipv4;
            Session.communication.friendsPort = Convert.ToString((int)friendPeer.Port);
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
}