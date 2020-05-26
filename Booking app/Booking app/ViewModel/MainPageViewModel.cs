﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Booking_app.Annotations;
using Booking_app.Model;
using Booking_app.Utility;

namespace Booking_app.ViewModel
{
    class MainPageViewModel : INotifyPropertyChanged
    {


        public MainPageViewModel()
        {
            NavigationCommand = new RelayCommand(BookRoom);
            LogoutCommand = new RelayCommand(LogOut);
            UserInfoCommand = new RelayCommand(UserInfo);
            UserBookings = new ObservableCollection<Booking>(from NewBookings in Persistency.Persistency.GetBookings() where NewBookings.Email == LoggedUser.Email select NewBookings);
        }

        public ObservableCollection<Booking> UserBookings { get; set; }
        public static ZealandUser LoggedUser { get; set; }
        public ICommand NavigationCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand UserInfoCommand { get; set; }

        public void BookRoom()
        {
            Navigation.NavigateToPage("BookPage", "MainPage");
        }

        public void LogOut()
        {
            LoggedUser = null;
            Navigation.NavigateToPage("Login","MainPage");
        }

        public void UserInfo()
        {
            Navigation.NavigateToPage("UserInfo", "MainPage");
        }

        











        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
