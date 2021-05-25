using System;
using System.Net;
using System.Security;
using EasyConsoleApplication;
using EasyConsoleApplication.Pages;
using instantMessagingClient.Model;
using Newtonsoft.Json;
using RestSharp;

namespace instantMessagingClient.Pages
{
    public class RegisterPage : Page
    {
        public RegisterPage()
        {
            ConsoleHelpers.Write(ConsoleColor.Yellow, "Register");
            ConsoleHelpers.Write(ConsoleColor.White, "-----");

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

                response = rest.Inscription(username, password);
                if (!response.IsSuccessful)
                {
                    ConsoleHelpers.WriteRed("There was an error, make sure the username doesn't already exists or the password isn't empty.");
                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        ConsoleHelpers.WriteRed(response.Content);
                    }
                    continue;
                }
                var responseContent = response.Content;
                Response deserializeObject = JsonConvert.DeserializeObject<Response>(responseContent);
                Session.tokens = deserializeObject;
                ConsoleHelpers.Write(ConsoleColor.White, "Successfully registered " + username + "!");
            }
            while (response.StatusCode != HttpStatusCode.OK);

            ConsoleHelpers.HitEnterToContinue();
            Application.GoTo<LoginPage>();
        }
    }
}
