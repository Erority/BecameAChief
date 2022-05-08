using BecomeAChef.Core;
using BecomeAChef.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using BecomeAChef.EF;
using System.Windows;

namespace BecomeAChef.MVVM.ViewModel
{
    class AddReceipeViewModel: ObservableObject
    {
        public RelayCommand ChangeImageCommand { get; set; }

        private string title;

        public string Title
        {
            get { return title; }
            set 
            { 
                title = value;
                OnPropertyChanged();
            }
        }

        private string content;

        public string Content
        {
            get { return content; }
            set 
            { 
                content = value;
                OnPropertyChanged();
            }
        }

        private short cookingTime;

        public short CookingTime
        {
            get { return cookingTime; }
            set 
            { 
                cookingTime = (short)value;
                OnPropertyChanged();
            }
        }

        private byte portions;

        public byte Portions
        {
            get { return portions; }
            set 
            { 
                portions = (byte)value;
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

                return image;
            }
            set
            {
                image = value;
                OnPropertyChanged();
            }
        }



        private IChangeView changeView;
        public AddReceipeViewModel(IChangeView changeView)
        {
            InitCommands();

            this.changeView = changeView;
        }

        private void InitCommands()
        {
            ChangeImageCommand = new RelayCommand(o =>
            {
                var image = new ImageConverter().GetImageFromFileDialog();

                if (image.UriSource == null) { return; }

                Image = image;
            });
        }

        public void AddRecipe()
        {
            if (!Validaiton()) { return; }

            Recipe recipe = new Recipe()
            {
                UserID = (UserDataSaver.UserID),
                Contents = Content,
                Image = new ImageConverter().GetJPGFromImageControl(Image),
                Title = this.Title,
                CookingTimeMinutes = this.CookingTime,
                Portions = this.Portions, 
                PublishedTimestamp = DateTime.Now
            };

            using (RecipeBookDBEntities db = new RecipeBookDBEntities())
            {
                try
                {
                    db.Recipe.Add(recipe);
                    db.SaveChanges();
                } catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }

            MessageBox.Show("Рецепт успешно добавлен", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            changeView.ChangeView(ViewNames.ProfileView);
        }

        private bool Validaiton()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                MessageBox.Show("Введите название рецепта", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(Content))
            {
                MessageBox.Show("Введите описание рецепта", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (image == null)
            {
                MessageBox.Show("Загрузите фото", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (CookingTime <= 0)
            {
                MessageBox.Show("Время готовки не может меньше или равно нулю", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (Portions <= 0)
            {
                MessageBox.Show("Кол-во порций не может меньше или равно нулю", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }


            return true;
        }

    }
}
