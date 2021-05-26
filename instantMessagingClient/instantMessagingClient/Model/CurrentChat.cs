using System;
using System.Linq;
using System.Threading.Tasks;
using EasyConsoleApplication;
using instantMessagingClient.Database;

namespace instantMessagingClient.Model
{
    public class CurrentChat
    {
        public bool isRunning { get; set; }

        public DatabaseContext db { get; set; }

        public int friendID { get; set; }

        public string backCommand { get; set; }

        public event EventHandler onTextChange;

        protected virtual void onTextChangeTrigger(EventArgs e)
        {
            onTextChange?.Invoke(this, e);
        }

        public CurrentChat(int friendID, string backCommand)
        {
            this.friendID = friendID;
            this.backCommand = backCommand;
            this.db = new DatabaseContext();
        }
        
        public void readLine()
        {
            isRunning = true;
            ConsoleHelpers.WriteGreen("If you want to go back, type '" + backCommand + "'");
            Task.Factory.StartNew(() =>
            {
                string rawString;
                do
                {
                    rawString = ConsoleHelpers.Readline(ConsoleColor.White, "You: ");
                    MyMessages msg = new MyMessages(Session.tokens.UserId, rawString);
                    Session.communication.sendMessage(msg);
                    /*if (string.IsNullOrEmpty(rawString) || rawString == backCommand) continue;
                    byte[] text = Encoding.ASCII.GetBytes(rawString);*/

                    Console.SetCursorPosition(0, ((Console.CursorTop > 0) ? Console.CursorTop - 1 : 0));
                    Console.WriteLine("".PadRight(Console.BufferWidth));
                    onTextChangeTrigger(EventArgs.Empty);
                } while (rawString != backCommand);
                isRunning = false;
            });
        }

        public void display()
        {
            Console.SetCursorPosition(0, 0);
            ConsoleHelpers.WriteGreen("If you want to go back, type '" + backCommand + "'");

            this.db.MyMessages.ToList().ForEach(m => Console.WriteLine(m.message));
        }
    }
}
