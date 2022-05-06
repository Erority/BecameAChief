using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BecomeAChef.EF;
using BecomeAChef.Utils;
using BecomeAChef.Core;
using System.Windows.Media.Imaging;

namespace BecomeAChef.MVVM.ViewModel
{
    class RecipeViewModel: ObservableObject
    {
        public RelayCommand GoBackCommand { get; set; }

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


        public RecipeViewModel(Recipe recipe)
        {
            CurrentRecipe = recipe;

            Image = new ImageConverter().LoadImage(recipe.Image);
            InitCommands();
        }

        private void InitCommands()
        {
            GoBackCommand = new RelayCommand(o => 
            {
                Coordinator.MainVM.GoBack();
            });
        }
    }
}
