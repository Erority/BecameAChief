using BecomeAChef.Core;
using BecomeAChef.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BecomeAChef.MVVM.ViewModel
{
    class OptionMenuViewModel : ObservableObject, IChangeView
    {
        public RelayCommand ProfileVMCommand { get; set; }
        public RelayCommand FavouritesVMCommand { get; set; }
        public RelayCommand GridVMCommand { get; set; }
        public RelayCommand AddReceipeVMCommand { get; set; }
        public RelayCommand AuthorizationVMCommand { get; set; }
        public RelayCommand RegistrationVMCommand { get; set; }




        public RelayCommand EditProfileCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand AddRecipeCommand { get; set; }

        public ProfileViewModel ProfileVM;
        public FavouritesViewModel FavouritesVM;
        public GridViewModel GridVM;
        private AuthorizationViewModel AuthroizationVM;
        private RegistrationViewModel RegistrationVM;
        private AddReceipeViewModel AddReceipeVM;

        
        private bool isCheckedAddReceipe;
        public bool IsCheckedAddReceipe
        {
            get { return isCheckedAddReceipe; }
            set
            {
                isCheckedAddReceipe = value;
                OnPropertyChanged();
            }
        }

        private bool isCheckedProfile;
        public bool IsCheckedProfile
        {
            get { return isCheckedProfile; }
            set
            {
                isCheckedProfile = value;
                OnPropertyChanged();
            }
        }

        private bool isCheckedGrid;
        public bool IsCheckedGrid
        {
            get { return isCheckedGrid; }
            set
            {
                isCheckedGrid = value;
                OnPropertyChanged();
            }
        }

        private bool isCheckedFavourites;
        public bool IsCheckedFavourites
        {
            get { return isCheckedFavourites; }
            set
            {
                isCheckedFavourites = value;
                OnPropertyChanged();
            }
        }

        private object currentView;
        public object CurrentView
        {
            get { return currentView; }
            set 
            { 
                currentView = value;
                OnPropertyChanged();
            }
        }

        private object currentSubView;
        public object CurrentSubView
        {
            get { return currentSubView; }
            set
            {
                currentSubView = value;
                OnPropertyChanged();
            }
        }

        private string currentViewName = "Кулинарная книга";

        public string CurrentViewName
        {
            get { return currentViewName; }
            set 
            { 
                currentViewName = value;
                OnPropertyChanged();
            }
        }
        
        private string searchText = "поиск";
        public string SearchText
        {
            get
            {
                return searchText;
            }

            set
            {
                searchText = value;
                OnPropertyChanged();
            }
        }

        private bool focusTextBlock;

        public bool FocusTextBlock
        {
            get { return focusTextBlock; }
            set 
            {
                if (value == true && SearchText == "поиск")
                    SearchText = "";
                else if(value == false && SearchText == "")
                    SearchText = "поиск";

                focusTextBlock = value;
                OnPropertyChanged();
            }
        }


        #region Visibility
        private Visibility editButtonVisibility;
        public Visibility EditButtonVisibility
        {
            get
            {
                return editButtonVisibility;
            }

            set
            {
                editButtonVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility searchTextBoxVisibility;
        public Visibility SearchTextBoxVisibility
        {
            get
            {
                return searchTextBoxVisibility;
            }

            set
            {
                searchTextBoxVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility profileButtonVisibility;
        public Visibility ProfileButtonVisibility
        {
            get
            {
                return profileButtonVisibility;
            }

            set
            {
                profileButtonVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility gridButtonVisibility;
        public Visibility GridButtonVisibility
        {
            get
            {
                return gridButtonVisibility;
            }

            set
            {
                gridButtonVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility favouritesButtonVisibility;
        public Visibility FavouritesButtonVisibility
        {
            get
            {
                return favouritesButtonVisibility;
            }

            set
            {
                favouritesButtonVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility addReceipeVisibility;
        public Visibility AddReceipeVisibility
        {
            get
            {
                return addReceipeVisibility;
            }

            set
            {
                addReceipeVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility addRecipeButtonVisibility;
        public Visibility AddRecipeButtonVisibility
        {
            get
            {
                return addRecipeButtonVisibility;
            }

            set
            {
                addRecipeButtonVisibility = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public OptionMenuViewModel()
        {
            AuthroizationVM = new AuthorizationViewModel(this);
            RegistrationVM = new RegistrationViewModel(this);


            EditButtonVisibility = Visibility.Hidden;
            AddRecipeButtonVisibility = Visibility.Hidden;

            InitCommands();

            if (UserDataSaver.LastView == null)
                AuthorizationVMCommand.Execute(null);
            else
            {
                GridVM = new GridViewModel();
                FavouritesVM = new FavouritesViewModel();
                ProfileVM = new ProfileViewModel();
                AddReceipeVM = new AddReceipeViewModel(this);

                if (UserDataSaver.LastView == "GridView")
                    GridVMCommand.Execute(null);
                else FavouritesVMCommand.Execute(null);
            }
        }

        private void InitCommands()
        {
            ProfileVMCommand = new RelayCommand(o =>
            {
                CurrentView = ProfileVM;
                CurrentViewName = "Профиль";
                EditButtonVisibility = Visibility.Visible;
                SearchTextBoxVisibility = Visibility.Hidden;

                GridButtonVisibility = Visibility.Visible;
                ProfileButtonVisibility = Visibility.Visible;
                FavouritesButtonVisibility = Visibility.Visible;
                AddReceipeVisibility = Visibility.Visible;
                AddRecipeButtonVisibility = Visibility.Hidden;
                IsCheckedAddReceipe = false;
                IsCheckedFavourites = false;
                IsCheckedGrid = false;
                IsCheckedProfile = true;
            });

            FavouritesVMCommand = new RelayCommand(o =>
            {
                CurrentView = FavouritesVM;
                CurrentViewName = "Избранное";
                EditButtonVisibility = Visibility.Hidden;
                SearchTextBoxVisibility = Visibility.Visible;
                AddRecipeButtonVisibility = Visibility.Hidden;
                IsCheckedAddReceipe = false;
                IsCheckedFavourites = true;
                IsCheckedGrid = false;
                IsCheckedProfile = false;
            });

            GridVMCommand = new RelayCommand(o =>
            {
                CurrentView = GridVM;
                CurrentViewName = "Кулинарная книга";
                EditButtonVisibility = Visibility.Hidden;
                SearchTextBoxVisibility = Visibility.Visible;
                AddRecipeButtonVisibility = Visibility.Hidden;
                IsCheckedAddReceipe = false;
                IsCheckedFavourites = false;
                IsCheckedGrid = true;
                IsCheckedProfile = false;
            });

            AddReceipeVMCommand = new RelayCommand(o =>
            {
                CurrentView = AddReceipeVM;
                CurrentViewName = "Новый рецепт";
                EditButtonVisibility = Visibility.Hidden;
                SearchTextBoxVisibility = Visibility.Hidden;
                AddRecipeButtonVisibility = Visibility.Visible;
                IsCheckedAddReceipe = true;
                IsCheckedFavourites = false;
                IsCheckedGrid = false;
                IsCheckedProfile = false;
            });

            RegistrationVMCommand = new RelayCommand(o =>
            {
                CurrentView = RegistrationVM;
                CurrentViewName = "Регистрация";
                GridButtonVisibility = Visibility.Hidden;
                ProfileButtonVisibility = Visibility.Hidden;
                FavouritesButtonVisibility = Visibility.Hidden;
                AddReceipeVisibility = Visibility.Hidden;
                SearchTextBoxVisibility = Visibility.Hidden;
            });

            AuthorizationVMCommand = new RelayCommand(o =>
            {
                CurrentView = AuthroizationVM;
                CurrentViewName = "Авторизация";
                GridButtonVisibility = Visibility.Hidden;
                ProfileButtonVisibility = Visibility.Hidden;
                FavouritesButtonVisibility = Visibility.Hidden;
                AddReceipeVisibility = Visibility.Hidden;
                SearchTextBoxVisibility = Visibility.Hidden;
            });

            EditProfileCommand = new RelayCommand(o => 
            {
                ProfileVM.ChangeUserData();
            });

            SearchCommand = new RelayCommand(o =>
            {
                if (CurrentView.GetType() == typeof(FavouritesViewModel)) {
                    if (!FavouritesVM.FilterCollection(SearchText)) { SearchText = "поиск"; }
                } else if (CurrentView.GetType() == typeof(GridViewModel)) {
                    if (!GridVM.FilterCollection(SearchText)) { SearchText = "поиск"; }
                }
            });

            AddRecipeCommand = new RelayCommand(o =>
            {
                AddReceipeVM.AddRecipe();
            });
        }

        public void ChangeView(ViewNames viewNames)
        {
            switch (viewNames)
            {
                case ViewNames.ProfileView:
                    ProfileVM = new ProfileViewModel();
                    FavouritesVM = new FavouritesViewModel();
                    GridVM = new GridViewModel();
                    AddReceipeVM = new AddReceipeViewModel(this);

                    ProfileVMCommand.Execute(null);
                    break;

                case ViewNames.RegistrationView:
                    RegistrationVMCommand.Execute(null);
                    break;

                case ViewNames.AuthorizationView:
                    AuthorizationVMCommand.Execute(null);
                    break;
            }
        }

    }
}
