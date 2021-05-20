using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using EasyConsoleApplication;
using EasyConsoleApplication.Pages;

namespace instantMessagingClient.Pages
{
    public class LoginPage : Page
    {
        public LoginPage()
        {
            ConsoleHelpers.Write(ConsoleColor.Green, "Login");
            ConsoleHelpers.Write(ConsoleColor.White, "-----");
            ConsoleHelpers.Write(ConsoleColor.White, "Enter your username and password to login: ");
            string username = ConsoleHelpers.Readline(ConsoleColor.White, "Username: ");
            SecureString password = Program.getPasswordFromConsole("Password: ");
            Console.WriteLine();

            ConsoleHelpers.Write(ConsoleColor.White, "Successfully logged in " + username + "!");
            ConsoleHelpers.HitEnterToContinue();
            Application.GoTo<LoggedInHomePage>();
        }
    }
}
