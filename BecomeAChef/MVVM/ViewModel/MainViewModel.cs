using BecomeAChef.Core;
using BecomeAChef.Utils;
using BecomeAChef.EF;
using System.Linq;

namespace BecomeAChef.MVVM.ViewModel
{
    class MainViewModel: ObservableObject
    {
        StartScreenViewModel StartScreenVM;
        OptionMenuViewModel OptionMenuVM;


        private object currentView;

        public object CurrentView 
        { 
            get { return currentView; } 
            set 
            { 
                currentView = value;
                OnPropertyChanged();
            } 
        }



        public MainViewModel()
        {

            using (RecipeBookDBEntities db = new RecipeBookDBEntities())
            {
                UserDataSaver.UserObject = (object)db.User.Where(u => u.ID == 2).FirstOrDefault();
            }

            StartScreenVM = new StartScreenViewModel();
            OptionMenuVM = new OptionMenuViewModel();   
            CurrentView = OptionMenuVM;

        }
    }
}
