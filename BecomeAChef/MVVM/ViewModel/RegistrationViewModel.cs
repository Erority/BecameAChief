using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                if (!Validaiton())
                {
                    return;
                }

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
                var image = new ImageConverter().GetImageFromFileDialog();

                if (image.UriSource == null) { return; }

                UserImage = image;
            });
        }

        private const string emailRegex = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
        private const string phoneRegex = @"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$";

        private bool Validaiton()
        {
            Regex emRegex = new Regex(emailRegex);
            Regex phRegex = new Regex(phoneRegex);


            if (string.IsNullOrWhiteSpace(Name))
            {
                MessageBox.Show("Введите имя", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Введите пароль", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(Email))
            {
                MessageBox.Show("Введите почту", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                MessageBox.Show("Введите телефон", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (userImage == null)
            {
                MessageBox.Show("Загрузите фото", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (!emRegex.IsMatch(Email))
            {
                MessageBox.Show("Почта должна быть формата test@text.domen", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (!phRegex.IsMatch(PhoneNumber))
            {
                MessageBox.Show("Телефон должен быть формата (000)000-00-00", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }


            return true;
        }

    }
}
    