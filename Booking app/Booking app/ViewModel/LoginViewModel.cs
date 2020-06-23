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
            //ManageFacility manageBooking = new ManageFacility();
            //manageBooking.Create(new Facility{FacilityNo = 0,Floor = 1,Size = 1});
            //manageBooking.Create(new Facility{FacilityNo = 1,Floor = 2,Size = 1});
            //manageBooking.Create(new Facility{FacilityNo = 2,Floor = 1,Size = 1});
            //manageBooking.Create(new Facility{FacilityNo = 3,Floor = 2,Size = 1});
            //manageBooking.Create(new Facility{FacilityNo = 4,Floor = 1,Size = 2});
            //manageBooking.Create(new Facility{FacilityNo = 5,Floor = 2,Size = 2});
            //manageBooking.Create(new Facility{FacilityNo = 6,Floor = 1,Size = 2});
            //manageBooking.Create(new Facility{FacilityNo = 7,Floor = 2,Size = 2});
            //manageBooking.Create(new Facility{FacilityNo = 8,Floor = 1,Size = 1});
            
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand CreateUserCommand { get; set; }

        public void Login()
        {
            var createdUsers = from users in Persistency.PersistencyService.GetUsers() where Email == users.Email select users;
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
