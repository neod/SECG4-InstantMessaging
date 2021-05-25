using System;
using System.Net.Sockets;
using System.Text;
using EasyConsoleApplication;
using EasyConsoleApplication.Pages;
using instantMessagingClient.Model;
using instantMessagingCore.Crypto;
using instantMessagingCore.Models.Dto;

namespace instantMessagingClient.Pages
{
    public class MessageFriend : Page
    {
        private static int _ID;

        public MessageFriend(int ID)
        {
            _ID = ID;
            Console.Clear();
            const string backCommand = "/back";
            ConsoleHelpers.WriteGreen("If you want to go back, type '"+ backCommand + "'");

            TcpClient clientSocket = new TcpClient();
            try
            {
                clientSocket.Connect("127.0.0.1", 44307);
            }
            catch (Exception e)
            {
                ConsoleHelpers.WriteRed(e.Message);
                throw;
            }
            
            //Peers peer = new Peers(Session.tokens.userId, );
            //nous sommes A, nous avons la clé publique de B
            RSAManager rsaA = new RSAManager();
            string publicKeyA = rsaA.GetKey(false);//pour que B reponde il a besoin de ca
            RSAManager rsaB = new RSAManager(new RSAManager().GetKey(false));//B
            string publicKeyB = rsaB.GetKey(false);

            if (clientSocket.Connected)
            {
                string rawString;
                do
                {
                    rawString = ConsoleHelpers.Readline(ConsoleColor.White, "You: ");
                    if (string.IsNullOrEmpty(rawString) || rawString == backCommand) continue;
                    
                    byte[] text = Encoding.ASCII.GetBytes(rawString);
                    RSAManager rsaClientB = new RSAManager(publicKeyB);
                    text = rsaClientB.Encrypt(text);


                    //dans clientB:

                } while (rawString != backCommand);
            }
            //Application.GoTo<FriendList>();
            Application.GoBack();
        }
    }
}
