using System;
using System.IO;
using System.Net;
using System.Security;
using EasyConsoleApplication;
using EasyConsoleApplication.Pages;
using instantMessagingClient.Model;
using instantMessagingCore.Models.Dto;
using Newtonsoft.Json;
using RestSharp;

namespace instantMessagingClient.Pages
{
    public class LoginPage : Page
    {
        public LoginPage()
        {
            ConsoleHelpers.Write(ConsoleColor.Green, "Login");
            ConsoleHelpers.Write(ConsoleColor.White, "-----");
            ConsoleHelpers.Write(ConsoleColor.White, "Enter your username and password to login: ");

            Rest rest = new Rest();
            IRestResponse response;

            do
            {
                //ask for username, pass until the response is correct
                string username;
                SecureString password;
                do
                {
                    username = ConsoleHelpers.Readline(ConsoleColor.White, "Username: ");
                    password = Program.getPasswordFromConsole("Password: ");
                    if (username == null)
                    {
                        ConsoleHelpers.WriteRed("\nUsername/Password can't be empty");
                    }
                } while (username == null);
                Console.WriteLine();

                response = rest.Login(username, password);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ConsoleHelpers.WriteRed("There was an error, make sure you registered or that your name and password are correct.");
                    continue;
                }
                var responseContent = response.Content;
                Tokens deserializeObject = JsonConvert.DeserializeObject<Tokens>(responseContent);
                Session.tokens = deserializeObject;
                Session.sessionPassword = password;
                Session.sessionUsername = username;
                ConsoleHelpers.WriteGreen("Successfully logged in " + username + "!");
                Console.WriteLine();
            }
            while (response.StatusCode != HttpStatusCode.OK);

            ConsoleHelpers.HitEnterToContinue();
            Application.GoTo<LoggedInHomePage>();
        }
    }
}
