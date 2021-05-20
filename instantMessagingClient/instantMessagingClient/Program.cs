using System;
using System.Net;
using System.Security;
using EasyConsoleApplication;
using EasyConsoleApplication.Menus;
using EasyConsoleApplication.Pages;
using instantMessagingClient.Model;
using instantMessagingClient.Pages;
using RestSharp;

namespace instantMessagingClient
{
    internal class Program
    {
        public static SecureString getPasswordFromConsole(string displayMessage)
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
    }
}
