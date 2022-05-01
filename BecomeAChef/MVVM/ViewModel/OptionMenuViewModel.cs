using BecomeAChef.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BecomeAChef.MVVM.ViewModel
{
    class OptionMenuViewModel : ObservableObject
    {
        public RelayCommand ProfileVMCommand { get; set; }
        public RelayCommand FavouritesVMCommand { get; set; }
        public RelayCommand GridVMCommand { get; set; }


        public ProfileViewModel ProfileVM;
        public FavouritesViewModel FavouritesVM;
        public GridViewModel GridVM;

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


        Button ChangeProfileButton { get; set; }


        public OptionMenuViewModel()
        {
            ProfileVM = new ProfileViewModel();
            FavouritesVM = new FavouritesViewModel();
            GridVM = new GridViewModel();

            ProfileVMCommand = new RelayCommand(o =>
            {
                CurrentView = ProfileVM;
                CurrentViewName = "Профиль";
            });

            FavouritesVMCommand = new RelayCommand(o =>
            {
                CurrentView = FavouritesVM;
                CurrentViewName = "Избранное";
            });

            GridVMCommand = new RelayCommand(o =>
            {
                CurrentView = GridVM;
                CurrentViewName = "Кулинарная книга";
            });


        }
    }
}
