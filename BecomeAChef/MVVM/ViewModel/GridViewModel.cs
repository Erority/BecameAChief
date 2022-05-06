using BecomeAChef.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BecomeAChef.EF;
using System.Windows;
using BecomeAChef.Utils;

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

        private object selectedItem;

        public object SelectedItem
        {
            get { return selectedItem; }
            set 
            {
                selectedItem = value;
                MessageBox.Show(((Recipe)value).Title);
                Coordinator.MainVM.Recipe = (Recipe)selectedItem;
            }
        }


        private RecipeBookDBEntities db = new RecipeBookDBEntities();

        public GridViewModel()
        {
            UserRecipes = db.Recipe.ToList();
        }



        public bool FilterCollection(string searchStr)
        {
            UserRecipes = UserRecipes.Where(r => r.Title.ToLower() == searchStr.ToLower()).ToList();

            if (UserRecipes.Count <= 0)
            {
                MessageBox.Show("Рецептов с таким названием нет", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                UserRecipes = db.Recipe.ToList();

                return false;
            }

            return true;
        }
    }
}
