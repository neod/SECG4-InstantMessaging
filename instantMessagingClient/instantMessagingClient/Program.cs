using System;
using System.Net;
using System.Security;
using EasyConsoleApplication;
using EasyConsoleApplication.Menus;
using EasyConsoleApplication.Pages;
using instantMessagingClient.Model;
using RestSharp;

namespace instantMessagingClient
{
    internal class Program
    {
        private static SecureString getPasswordFromConsole(string displayMessage)
        {
            SecureString pass = new SecureString();
            Console.Write(displayMessage);
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (!char.IsControl(key.KeyChar))
                {
                    pass.AppendChar(key.KeyChar);
                    Console.Write("*");
                }
                else
                {
                    if (key.Key != ConsoleKey.Backspace || pass.Length <= 0) continue;
                    pass.RemoveAt(pass.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter);
            return pass;
        }

        private static void Main(string[] args)
        {
            ConsoleSettings.DefaultColor = ConsoleColor.White;

            Menu mainMenu = new Menu("Instant messaging client");
            mainMenu.Items.Add(new MenuItem("Login", () => Application.GoTo<LoginPage>())
            {
                Color = ConsoleColor.Green
            });
            mainMenu.Items.Add(new MenuItem("Register", () => Application.GoTo<RegisterPage>())
            {
                Color = ConsoleColor.Yellow
            });
            mainMenu.Items.Add(Separator.Instance);
            mainMenu.Items.Add(new MenuItem("Quit", Application.Exit));

            Application.Render(mainMenu);
            Console.WriteLine("Application Terminated.");
            ConsoleHelpers.HitEnterToContinue();
        }
        
        public class LoginPage : Page
        {
            public LoginPage()
            {
                ConsoleHelpers.Write(ConsoleColor.Green, "Login");
                ConsoleHelpers.Write(ConsoleColor.White, "-----");
                ConsoleHelpers.Write(ConsoleColor.White, "Enter you username and password to login: ");
                string user = ConsoleHelpers.Readline(ConsoleColor.White, "Username: ");
                SecureString password = getPasswordFromConsole("Password: ");
                Console.WriteLine();
                //ConsoleHelpers.HitEnterToContinue();
                //Application.GoTo<LoginPage>();
            }
        }

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
                    string username = ConsoleHelpers.Readline(ConsoleColor.White, "Username: ");
                    SecureString password = getPasswordFromConsole("Password: ");
                    Console.WriteLine();

                    response = rest.Inscription(username, password);
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        ConsoleHelpers.WriteRed("There was an error, make sure the username doesn't already exists.");
                        continue;
                    }
                    var token = response.Content;
                    ConsoleHelpers.Write(ConsoleColor.White, "Successfully registered " + username + "!");
                }
                while (response.StatusCode != HttpStatusCode.OK);

                ConsoleHelpers.HitEnterToContinue();
                Application.GoTo<LoginPage>();
            }
        }

        public class PageAvecParametre : Page
        {
            private readonly string _dependency;

            public PageAvecParametre(string dependency)
            {
                _dependency = dependency;
                Title = "blabla";
                TitleColor = ConsoleColor.Yellow;
                Body = "-----";
                MenuItems.Add(new MenuItem("Back", Application.GoBack));
                MenuItems.Add(new MenuItem("Option 1", () => Console.WriteLine($"{_dependency} Action 2")));
                MenuItems.Add(new MenuItem("Option 2", () => Console.WriteLine($"{_dependency} Action 3")));
                MenuItems.Add(Separator.Instance);
                MenuItems.Add(new MenuItem("Back", Application.GoBack));
                MenuItems.Add(new MenuItem("Quit", Application.Exit));
            }
        }
    }
}
