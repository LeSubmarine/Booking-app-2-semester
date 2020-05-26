using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Booking_app.Model;
using Booking_app.Utility;

namespace Booking_app.ViewModel
{
    class UserInfoViewModel
    {

        public UserInfoViewModel()
        {
            BackCommand = new RelayCommand(Back);
            LoggedUser = MainPageViewModel.LoggedUser;
        }

        public ICommand BackCommand { get; set; }
        public ZealandUser LoggedUser { get; set; }

        public void Back()
        {
            Navigation.NavigateToPage("MainPage", "UserInfo");
        }
    }
}
