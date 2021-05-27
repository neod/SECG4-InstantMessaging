using System;
using System.Net;
using System.Text;
using instantMessagingClient.Database;
using instantMessagingClient.JsonRest;
using instantMessagingClient.Model;
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
            myServer.Start(ip, Convert.ToInt32(this.myPort));
            return myServer.IsStarted;
        }

        public void startClient()
        {
            myClient = new SimpleTcpClient { StringEncoder = Encoding.UTF8 };
            myClient.Connect(this.friendsHost, Convert.ToInt32(this.friendsPort));
        }

        private void Message_Received(object sender, Message e)
        {
            MyMessages msg = e.MessageString.Deserialize<MyMessages>();
            this.db.MyMessages.Add(msg);
            this.db.SaveChanges();
            cm.AskUpdate(msg.IdEnvoyeur);
        }

        public void sendMessage(MyMessages msg)
        {
            this.db.MyMessages.Add(msg);
            this.db.SaveChanges();
            string toSend = msg.Serialize();
            myClient.Write(toSend);
        }
    }
}
