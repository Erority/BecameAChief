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
                using (RecipeBookDBEntities db = new RecipeBookDBEntities())
                {
                    var user = db.User.Where(u => (u.PhoneNumber == PhoneOrEmail || u.Email == PhoneOrEmail) && u.Password == Password).FirstOrDefault();
                    UserDataSaver.UserID = user.ID;

                    if (user != null)
                    {
                        changeView.ChangeView(ViewNames.ProfileView);
                    } else
                    {
                        MessageBox.Show("Такого пользователя не существует", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        Password = "";
                        PhoneOrEmail = "";
                    }
                }
            });

            RegistrationCommand = new RelayCommand(o => 
            {
                changeView.ChangeView(ViewNames.RegistrationView);
            });
        }
    }
}
