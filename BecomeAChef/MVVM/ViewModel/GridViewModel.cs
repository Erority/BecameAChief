using BecomeAChef.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BecomeAChef.EF;

namespace BecomeAChef.MVVM.ViewModel
{
    class GridViewModel: ObservableObject
    {
        private List<Recipe> userRecipes;

        public List<Recipe> UserRecipes
        {
            get { return userRecipes; }
            set 
            {
                userRecipes = value;
                OnPropertyChanged();
            }
        }

        private RecipeBookDBEntities db = new RecipeBookDBEntities();

        public GridViewModel()
        {
            UserRecipes = db.Recipe.ToList();
        }
    }
}
