using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace instantMessagingClient.Model
{
    public class Heartbeat
    {

        private static Heartbeat instance { get; set; }

        public static Heartbeat getInstance()
        {
            return instance ??= new Heartbeat();
        }

        private Heartbeat()
        {
            restClient = new Rest();
            
            hearbeatTask = new Task(() =>
            {
                while (isRunning)
                {
                    restClient.sendHeartbeat();
                    Thread.Sleep(60_000);
                }
            });
        }

        private Rest restClient;
        private bool isRunning;
        private Task hearbeatTask;

        public void start()
        {
            isRunning = true;
            hearbeatTask.Start();
        }

        public void stop()
        {
            isRunning = false;
            hearbeatTask.Wait(1000);
        }

    }
}
