using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BecomeAChef.Core;
using BecomeAChef.EF;

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
            UserFavoritesReceips = db.Recipe.Where(r => r.UserID == 1).ToList();    
        }
    }
}
