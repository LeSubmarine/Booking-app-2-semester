using System;
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

namespace Booking_app.ViewModel
{
    class MainPageViewModel : INotifyPropertyChanged
    {


        public MainPageViewModel()
        {
            LoggedUser = Persistency.Persistency.GetUsers()[0];
            UserBookings = new ObservableCollection<Booking>(from NewBookings in Persistency.Persistency.GetBookings() where NewBookings.Email == LoggedUser.Email select NewBookings);
        }

        public ObservableCollection<Booking> UserBookings { get; set; }
        public User LoggedUser { get; set; }
        public ICommand NavigationCommand { get; set; }

        











        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
