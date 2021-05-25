using System;
using EasyConsoleApplication;
using EasyConsoleApplication.Menus;
using EasyConsoleApplication.Pages;
using instantMessagingClient.Model;
using instantMessagingCore.Models.Dto;

namespace instantMessagingClient.Pages
{
    public class Notification : Page
    {
        private static int _ID;
        public Notification(int ID)
        {
            _ID = ID;//user ID that wants to be friend

            Title = "UserId: " + _ID;
            TitleColor = ConsoleColor.Green;
            Body = "-----";
            MenuItems.Add(new MenuItem("Accept", Accept));
            MenuItems.Add(new MenuItem("Decline", Decline){ Color = ConsoleColor.Red});//TODO: block
            MenuItems.Add(new MenuItem("Go back", Application.GoBack)
            {
                Color = ConsoleColor.Yellow
            });
        }

        private static void Accept()
        {
            Rest rest = new Rest();
            var reply = rest.ActionFriendRequest(Friends.Action.accept, _ID);
            if (reply.IsSuccessful)
            {
                ConsoleHelpers.WriteGreen("Successfully added UserId" + _ID);
                ConsoleHelpers.HitEnterToContinue();
            }
            else
            {
                ConsoleHelpers.WriteRed("Error while adding friend :(");
                Console.WriteLine(reply.Content);
                ConsoleHelpers.HitEnterToContinue();
            }
            Application.GoTo<FriendList>();
        }

        private static void Decline()
        {
            ConsoleKeyInfo yesOrNo = ConsoleHelpers.AskToUserYesNoQuestion(ConsoleColor.Red, "Are you sure about that?");
            if (yesOrNo.Key == ConsoleKey.Y)
            {
                Rest rest = new Rest();
                var reply = rest.ActionFriendRequest(Friends.Action.refuse, _ID);
                if (reply.IsSuccessful)
                {
                    ConsoleHelpers.WriteGreen("Successfully declined UserId" + _ID);
                    ConsoleHelpers.HitEnterToContinue();
                }
                else
                {
                    ConsoleHelpers.WriteRed("Error while declining request.");
                    ConsoleHelpers.HitEnterToContinue();
                }
                Application.GoTo<FriendList>();
            }
        }
    }
}
