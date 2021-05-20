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
                ConsoleHelpers.Write(ConsoleColor.White, "Enter your username and password to login: ");
                string username = ConsoleHelpers.Readline(ConsoleColor.White, "Username: ");
                SecureString password = getPasswordFromConsole("Password: ");
                Console.WriteLine();

                ConsoleHelpers.Write(ConsoleColor.White, "Successfully logged in " + username + "!");
                ConsoleHelpers.HitEnterToContinue();
                Application.GoTo<LoggedInHomePage>();
            }
        }

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
                    SecureString password = getPasswordFromConsole("Password: ");
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

        public class LoggedInHomePage : Page
        {
            public LoggedInHomePage()
            {
                Title = "Home";
                TitleColor = ConsoleColor.Green;
                Body = "-----";
                MenuItems.Add(new MenuItem("Friends list", () => Application.GoTo<LoggedInFriendList>()));
                MenuItems.Add(new MenuItem("Delete account", deleteAccount)
                {
                    Color = ConsoleColor.Red
                });
                MenuItems.Add(new MenuItem("Disconnect", Application.Exit)
                {
                    Color = ConsoleColor.Yellow
                });
            }

            private void deleteAccount()
            {
                ConsoleHelpers.AskToUserYesNoQuestion(ConsoleColor.Red, "Are you sure about that?\n");
            }

        }

        public class LoggedInFriendList : Page
        {
            public LoggedInFriendList()
            {
                Title = "Friend list";
                TitleColor = ConsoleColor.Green;
                Body = "-----";
                int i;
                //for amis
                for (i = 0; i < 2; i++)
                {
                    //passer id en parametre vers next fenetre
                    MenuItems.Add(new MenuItem("Friend" + i, () => Application.GoTo<Friend>(i)));
                }
                //for notif
                for (int j = 0; j < 2; j++)
                {
                    MenuItems.Add(new MenuItem("User" + (j + i) + " wants to add you.", () => Application.GoTo<Notification>(j + i))
                    {
                        Color = ConsoleColor.Green
                    });
                }
                MenuItems.Add(Separator.Instance);
                MenuItems.Add(new MenuItem("Add friend", () => Application.GoTo<AddFriend>()));
                MenuItems.Add(new MenuItem("Go back", Application.GoBack)
                {
                    Color = ConsoleColor.Yellow
                });
            }
        }

        public class AddFriend : Page
        {
            public AddFriend()
            {
                Console.WriteLine("If you want to go back, type '/back'");
                string Name = ConsoleHelpers.Readline(ConsoleColor.White, "name: ");
                if (Name == "/back")
                {
                    Application.GoTo<LoggedInHomePage>();
                }
                //do while name exists puis go back to LoggedInHomePage (psk le goback va pas fonctionner si on se mets sur friendlist)
            }
        }

        public class Friend : Page
        {
            private readonly int _ID;

            public Friend(int ID)
            {
                _ID = ID;

                Title = "Friend" + _ID;
                TitleColor = ConsoleColor.Green;
                Body = "-----";
                MenuItems.Add(new MenuItem("Message", () => Application.GoTo<MessageFriend>(_ID)));
                MenuItems.Add(new MenuItem("Delete from friendlist", () =>
                    ConsoleHelpers.AskToUserYesNoQuestion(ConsoleColor.Red, "Are you sure about that?")
                )
                {
                    Color = ConsoleColor.Red
                });
                MenuItems.Add(new MenuItem("Go back", Application.GoBack)
                {
                    Color = ConsoleColor.Yellow
                });
            }
        }

        public class MessageFriend : Page
        {
            private readonly int _ID;

            public MessageFriend(int ID)
            {
                _ID = ID;
                Console.WriteLine("If you want to go back, type '/back'");
                string text = ConsoleHelpers.Readline(ConsoleColor.White, "You: ");
                if (text == "/back")
                {
                    Application.GoTo<LoggedInHomePage>();
                }
                // /exit pour goback??
            }
        }

        public class Notification : Page
        {
            private readonly int _ID;
            public Notification(int ID)
            {
                this._ID = ID;//notification id

                Title = "User" + _ID;
                TitleColor = ConsoleColor.Green;
                Body = "-----";
                MenuItems.Add(new MenuItem("Accept", () => Console.WriteLine("add")));
                MenuItems.Add(new MenuItem("Decline", () =>
                    ConsoleHelpers.AskToUserYesNoQuestion(ConsoleColor.Red, "Are you sure about that?")
                )
                {
                    Color = ConsoleColor.Red
                });
                MenuItems.Add(new MenuItem("Go back", Application.GoBack)
                {
                    Color = ConsoleColor.Yellow
                });
            }
        }
    }
}
