using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace instantMessagingClient.Model
{
    class User
    {
        private string _username;
        private SecureString _password;

        public User(string username, SecureString paSecureString)
        {
            this._username = username;
            this._password = paSecureString;
        }

        public string username
        {
            get => _username;
            set => _username = value;
        }

        public SecureString SecureString
        {
            get => _password;
            set => _password = value;
        }
        
        public string password
        {
            get
            {
                IntPtr valuePtr = IntPtr.Zero;
                try
                {
                    valuePtr = Marshal.SecureStringToGlobalAllocUnicode(this._password);
                    return Marshal.PtrToStringUni(valuePtr);
                }
                finally
                {
                    Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
                }
            }
        }
    }
}
