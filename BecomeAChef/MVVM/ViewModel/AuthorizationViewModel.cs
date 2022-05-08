using BecomeAChef.Core;
using BecomeAChef.EF;
using BecomeAChef.Utils;
using System.Linq;
using System.Windows;

namespace BecomeAChef.MVVM.ViewModel
{
    class AuthorizationViewModel: ObservableObject
    {
        public RelayCommand AuthCommand { get; set; }
        public RelayCommand RegistrationCommand { get; set; }

        private string phoneOrEmail;

        public string PhoneOrEmail
        {
            get { return phoneOrEmail; }
            set 
            {
                phoneOrEmail = value;
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


        private IChangeView changeView;
        public AuthorizationViewModel(IChangeView changeView)
        {
            InitCommands();
            this.changeView = changeView;
        }

        private void InitCommands()
        {
            AuthCommand = new RelayCommand(o =>
            {
                if (!Validation()) { return; }

                changeView.ChangeView(ViewNames.ProfileView);
            });

            RegistrationCommand = new RelayCommand(o => 
            {
                changeView.ChangeView(ViewNames.RegistrationView);
            });
        }

        private bool Validation()
        {
            if (string.IsNullOrWhiteSpace(PhoneOrEmail))
            {
                MessageBox.Show("Заполните телефон или email", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Заполните пароль", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else
            {
                using (RecipeBookDBEntities db = new RecipeBookDBEntities())
                {
                    var user = db.User.Where(u => (u.PhoneNumber == PhoneOrEmail || u.Email == PhoneOrEmail) && u.Password == Password).FirstOrDefault();

                    if (user != null)
                    {
                        UserDataSaver.UserID = user.ID;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Такого пользователя не существует", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        Password = "";
                        PhoneOrEmail = "";
                        return false;
                    }
                }
            }
        }
    }
}
