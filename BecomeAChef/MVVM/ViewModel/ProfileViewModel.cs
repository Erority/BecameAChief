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
using System.Windows;
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

        private User userData;

        public User UserData
        {
            get { return userData; }
            set 
            { 
                userData = value;
                OnPropertyChanged();
            }
        }


        public ProfileViewModel()
        {


            using (RecipeBookDBEntities db = new RecipeBookDBEntities())
            {
                var currentUser = db.User.Where(u => u.ID == UserDataSaver.UserID).FirstOrDefault();


                UserRecipes = db.Recipe.Where(r => r.User.ID == currentUser.ID).ToList();
                UserImage = new ImageConverter().LoadImage(currentUser.ProfilePicture);

                UserData = currentUser;

                UserData.Nickname = currentUser.Nickname;
                UserData.Email = currentUser.Email;
                UserData.PhoneNumber = currentUser.PhoneNumber;

            }

            InitCommands();
        }

        private void InitCommands()
        {
            ChangeUserImageCommand = new RelayCommand(o =>
            {
                UserImage = new ImageConverter().GetImageFromFileDialog();
                UpdateUserImageInDB(new ImageConverter().GetJPGFromImageControl(UserImage));
            });
        }


        public void ChangeUserData()
        {
            using (RecipeBookDBEntities db = new RecipeBookDBEntities())
            {
                var currentUser = db.User.Where(u => u.ID == UserDataSaver.UserID).FirstOrDefault();

                currentUser.Nickname = UserData.Nickname;
                currentUser.PhoneNumber = UserData.PhoneNumber;
                currentUser.Email = UserData.Email;


                try
                {
                    db.Entry(currentUser).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            MessageBox.Show("Данные изменены успешно");
        }


        private void UpdateUserImageInDB(byte[] imageCodeArray)
        {
            using (RecipeBookDBEntities db = new RecipeBookDBEntities())
            {
                var currentUser = db.User.Where(u => u.ID == UserDataSaver.UserID).FirstOrDefault();

                currentUser.ProfilePicture = imageCodeArray;
                try
                {
                    db.Entry(currentUser).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            MessageBox.Show("Данные изменены успешно");
        }
    }
}
