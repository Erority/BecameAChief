using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BecomeAChef.EF;
using BecomeAChef.Utils;
using BecomeAChef.Core;
using BecomeAChef.EF;
using System.Windows.Media.Imaging;
using System.Data.Entity;
using System.Windows;

namespace BecomeAChef.MVVM.ViewModel
{
    class RecipeViewModel: ObservableObject
    {
        public RelayCommand GoBackCommand { get; set; }

        public RelayCommand DeleteCommand { get; set; }

        private bool isInit = true;

        private Recipe currentRecipe;
        public Recipe CurrentRecipe
        {
            get { return currentRecipe; }
            set 
            { 
                currentRecipe = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage image;

        public BitmapImage Image
        {
            get 
            {
                if (image == null)
                {
                    Uri resourceUri = new Uri(System.AppContext.BaseDirectory + "../../Theme/Images/BackgroundImage.png", UriKind.Relative);
                    return new BitmapImage(resourceUri);
                }

                return image; }
            set { image = value; OnPropertyChanged(); }
        }

        private bool isCheckedFavourites;
        public bool IsCheckedFavourites
        {
            get { return isCheckedFavourites; }
            set
            {
                ChangeRecipeState(isCheckedFavourites);

                isCheckedFavourites = value;
                OnPropertyChanged();
            }
        }

        public RecipeViewModel(Recipe recipe)
        {
            CurrentRecipe = recipe;

            Image = new ImageConverter().LoadImage(recipe.Image);
            InitCommands();

            CheckedIsRecipeInFavorite();
            isInit = false;
        }

        private void InitCommands()
        {
            GoBackCommand = new RelayCommand(o => 
            {
                Coordinator.MainVM.GoBack();
            });

            DeleteCommand = new RelayCommand(o =>
            {

                var resultMess = MessageBox.Show("Вы действительно хотите удалить рецепт ?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (resultMess == MessageBoxResult.Yes)
                {

                    using (RecipeBookDBEntities db = new RecipeBookDBEntities())
                    {
                        var currentRecipe = db.Recipe.Where(r => r.ID == CurrentRecipe.ID).FirstOrDefault();

                        db.Recipe.Remove(currentRecipe);

                        try
                        {
                            db.SaveChanges();
                        } catch (Exception ex) { MessageBox.Show(ex.ToString()); }

                        Coordinator.MainVM.GoBack();
                    }
                } else { return; }

            });
        }

        private void CheckedIsRecipeInFavorite()
        {
            using (RecipeBookDBEntities db = new RecipeBookDBEntities()) 
            { 
                var favoritesRecipes = db.User.Where(u => u.ID == UserDataSaver.UserID).FirstOrDefault().Recipe1.ToList();

                foreach (var recipe in favoritesRecipes)
                {
                    if (recipe.ID == CurrentRecipe.ID)
                    {
                        IsCheckedFavourites = true;
                        break;
                    }
                }
            }
        }

        private void ChangeRecipeState(bool lastState)
        {
            if (!isInit)
            {
                using (RecipeBookDBEntities db = new RecipeBookDBEntities())
                {
                    var currentUser = db.User.Where(u => u.ID == UserDataSaver.UserID).FirstOrDefault();

                    if (lastState == false)
                    {
                        var recipe = db.Recipe.Where(r => r.ID == CurrentRecipe.ID).FirstOrDefault();
                        
                        try
                        {
                            currentUser.Recipe1.Add(recipe);
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        var recipe = db.Recipe.Where(r => r.ID == CurrentRecipe.ID).FirstOrDefault();

                        try
                        {
                            currentUser.Recipe1.Remove(recipe);
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }
    }
}
