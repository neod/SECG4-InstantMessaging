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
        private readonly int _ID;

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
            RSAManager clefPriveDeA = new RSAManager(new RSAManager().GetKey(true));//A
            RSAManager clefPublicDeB = new RSAManager(new RSAManager().GetKey(false));//B

            if (clientSocket.Connected)
            {
                string rawString;
                do
                {
                    rawString = ConsoleHelpers.Readline(ConsoleColor.White, "You: ");
                    if (string.IsNullOrEmpty(rawString) || rawString == backCommand) continue;
                    
                    byte[] text = Encoding.ASCII.GetBytes(rawString);
                    text = clefPublicDeB.Encrypt(text);
                } while (rawString != backCommand);
            }
            //Application.GoTo<FriendList>();
            Application.GoBack();
        }
    }
}
