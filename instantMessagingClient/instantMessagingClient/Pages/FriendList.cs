using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyConsoleApplication;
using EasyConsoleApplication.Menus;
using EasyConsoleApplication.Pages;

namespace instantMessagingClient.Pages
{
    public class FriendList : Page
    {
        public FriendList()
        {
            Title = "Friend list";
            TitleColor = ConsoleColor.Green;
            Body = "-----";
            int i;
            //for amis
            for (i = 0; i < 2; i++)
            {
                //passer id en parametre vers next fenetre
                int friendID = i;
                MenuItems.Add(new MenuItem("Friend" + i, () => Application.GoTo<Friend>(friendID)));
            }
            //for notif
            for (int j = 0; j < 2; j++)
            {
                int notifID = j;
                MenuItems.Add(new MenuItem("User" + (j + i) + " wants to add you.", () => Application.GoTo<Notification>(notifID + i))
                {
                    Color = ConsoleColor.Green
                });
            }
            MenuItems.Add(Separator.Instance);
            MenuItems.Add(new MenuItem("Add friend", () => Application.GoTo<AddFriend>()));
            MenuItems.Add(new MenuItem("Go back", () => Application.GoTo<LoggedInHomePage>())
            {
                Color = ConsoleColor.Yellow
            });
        }
    }
}
