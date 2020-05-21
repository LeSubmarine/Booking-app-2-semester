using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Booking_app.Annotations;
using Booking_app.Model;
using Booking_app.Persistency;
using Booking_app.Utility;

namespace Booking_app.ViewModel
{
    class UserViewModel : INotifyPropertyChanged
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

       //mangler at forbinde med bindings i view CreateUser.

        public ICommand RegisterCommand { get; set; }
        public UserViewModel()
        {
            RegisterCommand = new RelayCommand(Register);
        }

        public void Register()
        {
            
            Persistency.Persistency.GetUsers();
            var createdUsers = from users in Persistency.Persistency.GetUsers() where Email == users.Email select users;
            if (createdUsers.Count() == 1 || createdUsers.Count() > 1)
            {
                if (Password == ConfirmPassword)
                {
                    var user = new User { Email = Email, Password = Password, Name = Name };
                    Persistency.Persistency.AddUser(user);
                }
            }
            //jeg mangler at lave fejl besked hvis If statements ikke er opfyldt
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
