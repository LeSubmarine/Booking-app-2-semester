using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Booking_app.Annotations;
using Booking_app.Model;
using Booking_app.Persistency;
using Booking_app.Utility;

namespace Booking_app.ViewModel
{
    class CreateUserViewModel : INotifyPropertyChanged
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public ICommand CancelCommand { get; set; }

        public ICommand RegisterCommand { get; set; }
        public CreateUserViewModel()
        {
            Email = "";
            Name = "";
            Password = "";
            ConfirmPassword = "";
            CancelCommand = new RelayCommand(CancelButton);
            RegisterCommand = new RelayCommand(Register);
        }

        public void Register()
        {
            
            var createdUsers = from users in Persistency.Persistency.GetUsers() where Email == users.Email select users;
            if (!createdUsers.Any())
            {
                if (ValidateMailAdr(Email))
                {
                    var user = new ZealandUser { Email = Email, Password = Password, Name = Name };
                    Persistency.Persistency.AddUser(user);
                    Navigation.NavigateToPage("Login", "CreateUser");
                }
                else
                {
                    MessageDialogHelper.Show("Email is not valid","Error");
                }
            }
            else
            {
                MessageDialogHelper.Show("Email is already registered","Error");
            }
        }
        public static bool ValidateMailAdr(string mailAdr)
        {
            if (Regex.IsMatch(mailAdr, @"^([\w.-]+)@([\w-]+)((.(\w){2,})+)$"))
            {
                return true;
            }
            return false;
        }

        public void CancelButton()
        {
            Navigation.NavigateToPage("Login", "CreateUser");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
