using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace instantMessagingClient.Database
{
    public class myMessages
    {
        public int Id { get; set; }

        public int IdEnvoyeur { get; set; }

        public string message { get; set; }

        public myMessages(int idEnvoyeur, string message)
        {
            IdEnvoyeur = idEnvoyeur;
            this.message = message;
        }

        public myMessages()
        {
        }

        public myMessages(int id, int idEnvoyeur, string message)
        {
            Id = id;
            IdEnvoyeur = idEnvoyeur;
            this.message = message;
        }
    }
}
