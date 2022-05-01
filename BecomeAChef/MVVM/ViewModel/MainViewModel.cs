using BecomeAChef.Core;

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
            StartScreenVM = new StartScreenViewModel();
            OptionMenuVM = new OptionMenuViewModel();   
            CurrentView = OptionMenuVM;
        }
    }
}
