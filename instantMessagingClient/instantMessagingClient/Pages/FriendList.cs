using System;
using EasyConsoleApplication;
using EasyConsoleApplication.Menus;
using EasyConsoleApplication.Pages;
using instantMessagingClient.Model;
using instantMessagingCore.Models.Dto;
using Newtonsoft.Json;

namespace instantMessagingClient.Pages
{
    public class FriendList : Page
    {
        public FriendList()
        {
            Title = "Friend list";
            TitleColor = ConsoleColor.Green;
            Body = "-----";
            PrintFriendsAndNotifications();
            MenuItems.Add(Separator.Instance);
            MenuItems.Add(new MenuItem("Add friend", () => Application.GoTo<AddFriend>()));
            MenuItems.Add(new MenuItem("Go back", () => Application.GoTo<LoggedInHomePage>())
            {
                Color = ConsoleColor.Yellow
            });
        }

        private void PrintFriendsAndNotifications()
        {
            Rest rest = new Rest();

            //for amis
            var reponseMyFriends = rest.getMyFriendList();
            if (reponseMyFriends.IsSuccessful)
            {
                Friends[] FriendList = JsonConvert.DeserializeObject<Friends[]>(reponseMyFriends.Content);
                if (FriendList != null)
                {
                    foreach (Friends friend in FriendList)
                    {
                        MenuItems.Add(new MenuItem("Friend" + friend.UserId, () => Application.GoTo<Friend>(friend.UserId)));
                    }
                }
            }
            
            //for notif
            var reponseFriendRequests = rest.getFriendRequests();
            if (reponseFriendRequests.IsSuccessful)
            {
                Friends[] FriendList = JsonConvert.DeserializeObject<Friends[]>(reponseFriendRequests.Content);
                if (FriendList != null)
                {
                    foreach (Friends friend in FriendList)
                    {
                        MenuItems.Add(new MenuItem("UserId: " + friend.UserId + " wants to add you.",
                            () => Application.GoTo<Notification>(friend.UserId))
                        {
                            Color = ConsoleColor.Green
                        });
                    }
                }
            }
        }
    }
}
