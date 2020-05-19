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
        public string Password { get; set; }
        public string Name { get; set; }

        public ICommand AddCommand { get; set; }

        public UserViewModel()
        {
            Persistency.Persistency.GetUsers();

            AddCommand = new RelayCommand(Add);
            //SaveCommand = new RelayCommand(Save);
        }

        //public void Register(User)
        //{
        //    if (User.Email "")
        //    {

        //    }

        //}

        public void Add()
        { 
            //UsersAdd(new User(Email, Password, Name));
        }

        

        //public void Save()
        //{
        //    PersistencyService.SaveUsersAsJsonAsync(Users);
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
