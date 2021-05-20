using System;
using System.Net;
using System.Security;
using EasyConsoleApplication;
using EasyConsoleApplication.Pages;
using instantMessagingClient.Model;
using RestSharp;

namespace instantMessagingClient.Pages
{
    public class RegisterPage : Page
    {
        public RegisterPage()
        {
            ConsoleHelpers.Write(ConsoleColor.Yellow, "Register");
            ConsoleHelpers.Write(ConsoleColor.White, "-----");

            string token = "";
            Rest rest = new Rest();
            IRestResponse response;
            do
            {
                //ask for username, pass until the response is correct
                string username = ConsoleHelpers.Readline(ConsoleColor.White, "Username: ");
                SecureString password = Program.getPasswordFromConsole("Password: ");
                Console.WriteLine();

                response = rest.Inscription(username, password);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ConsoleHelpers.WriteRed("There was an error, make sure the username doesn't already exists.");
                    continue;
                }
                token = response.Content;
                ConsoleHelpers.Write(ConsoleColor.White, "Successfully registered " + username + "!");
            }
            while (response.StatusCode != HttpStatusCode.OK);

            ConsoleHelpers.HitEnterToContinue();
            Application.GoTo<LoginPage>();
        }
    }
}
