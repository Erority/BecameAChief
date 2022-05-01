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
        private static object userObject;

        public static object UserObject
        {
            get { return userObject; }
            set { userObject = value; }
        }

    }
}
