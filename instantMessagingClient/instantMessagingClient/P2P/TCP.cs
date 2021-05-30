using System;
using System.Net;
using System.Text;
using EasyConsoleApplication;
using instantMessagingClient.Database;
using instantMessagingClient.JsonRest;
using instantMessagingClient.Model;
using instantMessagingClient.Pages;
using instantMessagingCore.Crypto;
using instantMessagingCore.Models.Dto;
using SimpleTCP;

namespace instantMessagingClient.P2P
{
    public class TCP
    {
        public DatabaseContext db { get; set; }

        public string myHost { get; set; }

        public string myPort { get; set; }

        public string friendsHost { get; set; }

        public string friendsPort { get; set; }

        public SimpleTcpClient myClient { get; set; }

        public SimpleTcpServer myServer { get; set; } 

        private readonly ChatManager cm = ChatManager.getInstance();

        public TCP(string myHost, string myPort)
        {
            this.myHost = myHost;
            this.myPort = myPort;//8910
            db = new DatabaseContext();
        }

        public void Deconstruct(out SimpleTcpClient myClient, out SimpleTcpServer myServer)
        {
            myClient = this.myClient;
            myServer = this.myServer;

            myClient.Disconnect();
            myServer.Stop();
        }

        public bool startListener()
        {
            myServer = new SimpleTcpServer { Delimiter = 0x13, StringEncoder = Encoding.UTF8 };
            myServer.DataReceived += Message_Received;

            IPAddress ip = IPAddress.Parse(this.myHost);
            myServer.Stop();
            myServer.Start(ip, Convert.ToInt32(this.myPort));
            return myServer.IsStarted;
        }

        public void startClient()
        {
            myClient = new SimpleTcpClient { StringEncoder = Encoding.UTF8 };
            try
            {
                myClient.Connect(this.friendsHost, Convert.ToInt32(this.friendsPort));
            }
            catch (Exception e)
            {
                ConsoleHelpers.WriteRed(e.Message);
                ConsoleHelpers.HitEnterToContinue();
                Application.GoTo<FriendList>();
                throw;
            }
        }

        private void Message_Received(object sender, Message e)
        {
            MyMessages msg = e.MessageString.Deserialize<MyMessages>();
            
            this.db.MyMessages.Add(msg);
            this.db.SaveChanges();
            cm.AskUpdate(msg.IdEnvoyeur);
        }

        public void sendMessage(MyMessages msg, postKey friendKey)
        {
            RSAManager rsaFriend = new RSAManager(friendKey.Key);
            RSAManager myRSA = new RSAManager(Session.maKey.Key);

            byte[] text = Encoding.ASCII.GetBytes(msg.message);
            text = rsaFriend.Encrypt(text);

            byte[] mytext = Encoding.ASCII.GetBytes(msg.message);
            mytext = myRSA.Encrypt(mytext);


            string encodedStr = Convert.ToBase64String(text);
            string myencodedStr = Convert.ToBase64String(mytext);

            msg.message = encodedStr;

            string toSend = msg.Serialize();
            try
            {
                myClient.Write(toSend);
                //copy pour ma bd
                MyMessages myMsg = new MyMessages(msg.IdEnvoyeur, myencodedStr);
                this.db.MyMessages.Add(myMsg);
                this.db.SaveChanges();
            }
            catch (Exception e)
            {
                ConsoleHelpers.WriteRed("Your friend disconnected");
                ConsoleHelpers.WriteRed(e.Message);
                ConsoleHelpers.HitEnterToContinue();
                myClient.Disconnect();
                Application.GoBack();
            }
            
            cm.AskUpdate(Session.tokens.UserId);
        }
    }
}
