using System;
using System.Security;
using System.Threading.Tasks;
using EasyConsoleApplication;
using EasyConsoleApplication.Menus;
using EasyConsoleApplication.Pages;

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
            Application.GoTo<HomePage>();

            Application.Render(mainMenu);
            Console.WriteLine("Application Terminated.");
            ConsoleHelpers.HitEnterToContinue();
        }

        public class HomePage : Page
        {
            public HomePage()
            {
                Title = "Home";
                TitleColor = ConsoleColor.Green;
                Body = "----";
                BodyColor = ConsoleColor.DarkGreen;
                MenuItems.Add(new MenuItem("Login", () => Application.GoTo<LoginPage>()));
                MenuItems.Add(new MenuItem("Register", () => Application.GoTo<RegisterPage>())
                {
                    Color = ConsoleColor.Yellow
                });
                MenuItems.Add(Separator.Instance);
                MenuItems.Add(new MenuItem("Quit", Application.Exit));
            }
        }

        public class LoginPage : Page
        {
            public LoginPage()
            {
                Title = "Login";
                Body = "-----";
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
                Title = "Register";
                TitleColor = ConsoleColor.Yellow;
                Body = "-----";
                string user = ConsoleHelpers.Readline(ConsoleColor.White, "Username: ");
                SecureString password = getPasswordFromConsole("Password: ");
                Console.WriteLine();
                ConsoleHelpers.Write(ConsoleColor.White, "Successfully registered " + user);
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
