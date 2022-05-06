using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using BecomeAChef.Core;
using BecomeAChef.EF;
using BecomeAChef.Utils;

namespace BecomeAChef.MVVM.ViewModel
{
    class RegistrationViewModel: ObservableObject
    {
        public RelayCommand AuthCommand { get; set; }
        public RelayCommand RegCommand { get; set; }
        public RelayCommand UploadImageCommand { get; set; }



        private string name;
        public string Name
        {
            get { return name; }
            set 
            { 
                name = value; 
                OnPropertyChanged();
            }
        }


        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = value;
                OnPropertyChanged();
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }


        private BitmapImage userImage;
        public BitmapImage UserImage
        {
            get
            {
                if (userImage == null)
                {
                    Uri resourceUri = new Uri(System.AppContext.BaseDirectory + "../../Theme/Images/BackgroundImage.png", UriKind.Relative);
                    return new BitmapImage(resourceUri);
                } 

                return userImage;
            }
            set
            {
                userImage = value;
                OnPropertyChanged();
            }
        }


        private IChangeView changeView;
        public RegistrationViewModel(IChangeView changeView)
        {
            InitCommands();
            this.changeView = changeView;

        }

        private void InitCommands()
        {
            RegCommand = new RelayCommand(o =>
            {
                using (RecipeBookDBEntities db = new RecipeBookDBEntities())
                {
                    try
                    {
                        User user = new User();

                        user.Email = this.Email;
                        user.PhoneNumber = this.PhoneNumber;
                        user.Password = this.Password;
                        user.Nickname = this.Name;
                        user.ProfilePicture = new ImageConverter().GetJPGFromImageControl(this.UserImage)   ;

                        db.User.Add(user);
                        db.SaveChanges();

                    } catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    MessageBox.Show("Пользователь добавлен", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    changeView.ChangeView(ViewNames.AuthorizationView);
                }
            });

            AuthCommand = new RelayCommand(o =>
            {
                changeView.ChangeView(ViewNames.AuthorizationView);
            });

            UploadImageCommand = new RelayCommand(o =>
            {
                UserImage = new ImageConverter().GetImageFromFileDialog();
            });
        }
    }
}
