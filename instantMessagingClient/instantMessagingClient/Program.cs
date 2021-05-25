using System;
using System.Security;
using EasyConsoleApplication;
using EasyConsoleApplication.Menus;
using instantMessagingClient.Pages;

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
            Application.GoTo<Home>();
        }
    }
}
