using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BecomeAChef.Core;
using BecomeAChef.EF;
using BecomeAChef.Utils;

namespace BecomeAChef.MVVM.ViewModel
{
    class FavouritesViewModel: ObservableObject
    {
        private List<Recipe> userFavoritesReceips;

        public List<Recipe> UserFavoritesReceips
        {
            get { return userFavoritesReceips; }
            set 
            { 
                userFavoritesReceips = value;
                OnPropertyChanged();
            }
        }

        private object selectedItem;
        public object SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                UserDataSaver.LastView = "FavouritesView";
                Coordinator.MainVM.Recipe = (Recipe)selectedItem;
            }
        }


        private Visibility textBlockNoContentVisibility;

        public Visibility TextBlockNoContentVisibility
        {
            get { return textBlockNoContentVisibility; }
            set 
            { 
                textBlockNoContentVisibility = value;
                OnPropertyChanged();
            }
        }


        public FavouritesViewModel()
        {
            using (RecipeBookDBEntities db = new RecipeBookDBEntities()) 
            {
                var user = db.User.Where(u => u.ID == UserDataSaver.UserID).FirstOrDefault();

                UserFavoritesReceips = user.Recipe1.ToList();
                
                if (userFavoritesReceips.Count <=0 ) 
                    TextBlockNoContentVisibility = Visibility.Visible;
                else
                    TextBlockNoContentVisibility = Visibility.Hidden;
            }
        }

        public bool FilterCollection (string searchStr)
        {
            UserFavoritesReceips = UserFavoritesReceips.Where(r => r.Title.ToLower() == searchStr.ToLower()).ToList();

            using (RecipeBookDBEntities db = new RecipeBookDBEntities())
            {
                var user = db.User.Where(u => u.ID == UserDataSaver.UserID).FirstOrDefault();

                UserFavoritesReceips = user.Recipe1.ToList();
                if (UserFavoritesReceips.Count <= 0)
                {
                    MessageBox.Show("Рецептов с таким названием нет", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    UserFavoritesReceips = db.Recipe.Where(r => r.UserID == user.ID).ToList();

                    return false;
                }
            }

            return true;
        }
    }
}
