using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BecomeAChef.EF;

namespace BecomeAChef.Utils
{
    internal static class UserDataSaver
    {
        private static int userID;

        public static int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

    }
}
