using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using instantMessagingClient.Model;
using SimpleTCP;

namespace instantMessagingClient.P2P
{
    public class TCP
    {
        public string myHost { get; set; }

        public string myPort { get; set; }

        public string friendsHost { get; set; }

        public string friendsPort { get; set; }

        public SimpleTcpClient myClient { get; set; }

        public SimpleTcpServer myServer { get; set; } 

        public TCP(string myHost, string myPort)
        {
            this.myHost = myHost;
            this.myPort = myPort;//8910
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
            myClient.DataReceived += getComfirmation;
            myClient.Connect(this.friendsHost, Convert.ToInt32(this.friendsPort));
        }

        private void Message_Received(object sender, Message e)
        {
            if (!Session.isOnMessagingPage) return;

            //string decryptedText = "";
            Console.WriteLine(e.MessageString);
        }

        public void sendMessage(string bytesToSend)
        {
            myClient.WriteAndGetReply(bytesToSend);
        }

        private static void getComfirmation(object sender, Message e)
        {
            //jsp a quoi ca sert ici
            //Console.WriteLine(e.MessageString);
        }
    }
}
