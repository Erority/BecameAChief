using BecomeAChef.Core;
using BecomeAChef.EF;
using BecomeAChef.Utils;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BecomeAChef.MVVM.ViewModel
{
    class ProfileViewModel: ObservableObject
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

        public RelayCommand ChangeUserImageCommand { get; set; }

        private BitmapImage userImage;
        public BitmapImage UserImage
        {
            get
            {
                return userImage;
            }
            set
            {
                userImage = value;
                OnPropertyChanged();
            }
        }

        public RecipeBookDBEntities db { get; } = new RecipeBookDBEntities();

        public ProfileViewModel()
        {
            UserRecipes = db.Recipe.Where(r => r.UserID == ((User)(UserDataSaver.UserObject)).ID).ToList();
            UserImage = new ImageConverter().LoadImage(((User)UserDataSaver.UserObject).ProfilePicture);
            InitCommands();
        }

        private void InitCommands()
        {
            ChangeUserImageCommand = new RelayCommand(o =>
            {
                UserImage = GetImageFromFileDialog();
                UpdateUserImageInDB(new ImageConverter().GetJPGFromImageControl(UserImage));
            });
        }

        private BitmapImage GetImageFromFileDialog()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            BitmapImage bitmap = new BitmapImage();

            if (openFile.ShowDialog() == true)
            {
                bitmap = new BitmapImage(new Uri(openFile.FileName));
            }

            return bitmap;
        }

        private void UpdateUserImageInDB(byte[] imageCodeArray)
        {
            var currentUser = ((User)(UserDataSaver.UserObject));
            currentUser.ProfilePicture = imageCodeArray;
            db.Entry(currentUser).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
