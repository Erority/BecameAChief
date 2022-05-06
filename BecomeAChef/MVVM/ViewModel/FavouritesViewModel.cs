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

        private RecipeBookDBEntities db = new RecipeBookDBEntities();


        public FavouritesViewModel()
        {
            UserFavoritesReceips = ((User)UserDataSaver.UserObject).Recipe1.ToList();    
        }

        public bool FilterCollection (string searchStr)
        {
            UserFavoritesReceips = UserFavoritesReceips.Where(r => r.Title.ToLower() == searchStr.ToLower()).ToList();

            if (UserFavoritesReceips.Count <= 0)
            {
                MessageBox.Show("Рецептов с таким названием нет", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                UserFavoritesReceips = db.Recipe.Where(r => r.UserID == ((User)UserDataSaver.UserObject).ID).ToList();

                return false;
            }

            return true;
        }
    }
}
