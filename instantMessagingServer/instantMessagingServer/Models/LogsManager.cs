using instantMessagingCore.Models.Dto;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace instantMessagingServer.Models
{
    public class LogsManager
    {
        // Singleton
        private static LogsManager instance { get; set; }

        private const int memoryLogsSize = 2;//100_000;
        private readonly ConcurrentQueue<Logs> LogsQueue;

        private DatabaseContext db { get; set; }

        public static LogsManager GetInstance()
        {
            return instance ??= new LogsManager();
        }

        private LogsManager()
        {
            LogsQueue = new ConcurrentQueue<Logs>();
            db = new DatabaseContext(Config.Configuration);

            Task.Factory.StartNew(() =>
            {
                int count;
                Logs log;
                while (true)
                {
                    count = 0;
                    while (LogsQueue.TryDequeue(out log) && count < 1000)
                    {
                        ++count;
                        db.Logs.Add(log);
                    }
                    db.SaveChanges();
                    Thread.Sleep(1000);
                }
            });
        }

        // Instance
        public void write(Logs log)
        {
            LogsQueue.Enqueue(log);
        }

    }
}
