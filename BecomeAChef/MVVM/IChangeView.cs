using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecomeAChef.MVVM
{
    internal interface IChangeView
    {
        void ChangeView(ViewNames viewNames);
    }


    public enum ViewNames
    {
        ProfileView, 
        AuthorizationView, 
        RegistrationView
    }
}
