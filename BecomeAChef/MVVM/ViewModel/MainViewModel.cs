using BecomeAChef.Core;
using BecomeAChef.Utils;
using BecomeAChef.EF;
using System.Linq;

namespace BecomeAChef.MVVM.ViewModel
{
    class MainViewModel: ObservableObject
    {
        private StartScreenViewModel StartScreenVM;
        private OptionMenuViewModel OptionMenuVM;
        private RecipeViewModel RecipeVM;

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

        private OptionMenuViewModel LastView;

        private Recipe recipe;
        public Recipe Recipe
        {
            get { return recipe; }
            set 
            {
                recipe = value;
                RecipeVM = new RecipeViewModel(recipe);
                LastView = (OptionMenuViewModel)CurrentView;
                CurrentView = RecipeVM;
            }
        }

        public MainViewModel()
        {
            StartScreenVM = new StartScreenViewModel();
            OptionMenuVM = new OptionMenuViewModel();

            Coordinator.MainVM = this;

            CurrentView = OptionMenuVM;
        }
        
        public void GoBack()
        {
            CurrentView = LastView;
        }
    }
}
