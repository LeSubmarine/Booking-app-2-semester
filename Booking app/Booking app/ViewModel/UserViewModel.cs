using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
    class UserViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public ICommand AddCommand { get; set; }

        public ICommand SaveCommand { get; set; }

        public UserViewModel()
        {

            AddCommand = new RelayCommand(Add);
            //SaveCommand = new RelayCommand(Save);
        }

       

        //public void Save()
        //{
        //    PersistencyService.SaveNotesAsJsonAsync(Users);
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
