using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Booking_app.Model;
using Booking_app.Persistency;
using Booking_app.Utility;
using Booking_app.View;

namespace Booking_app.ViewModel
{
    class LoginViewModel
    {
        public LoginViewModel()
        {
            CreateUserCommand = new RelayCommand(NaviCreateUser);
            LoginCommand = new RelayCommand(Login);
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand CreateUserCommand { get; set; }

        public void Login()
        {
            Persistency.Persistency.NewBookings();
            var createdUsers = from users in Persistency.Persistency.GetUsers() where Email == users.Email select users;
            if (createdUsers.Count() == 1)
            {
                if (createdUsers.First().Password == Password)
                {
                    MainPageViewModel.LoggedUser = createdUsers.First();
                    Navigation.NavigateToPage("MainPage","Login");
                }
            }
        }

        public void NaviCreateUser()
        {
            Navigation.NavigateToPage("CreateUser", "Login");
        }
    }
}
