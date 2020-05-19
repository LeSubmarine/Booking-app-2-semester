using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Booking_app.Annotations;
using Booking_app.Model;
using Booking_app.Utility;

namespace Booking_app.ViewModel
{
    /*
     * Hvis det er mulig skal der så vidt muligt laves en mere effektiv måde at opdaterer Available rooms, istedet for at bare slette hele skidtet, og genskabe det. Det grimt, og potentielt langsomt
     */
    class BookPageViewModel : INotifyPropertyChanged
    {
        #region Instance field
        private ObservableCollection<Facility> _availableRooms;
        private DateTimeOffset _date;
        private int _selectedRoom;
        public static readonly DependencyProperty UserViewModelProperty = DependencyProperty.Register("UserViewModel", typeof(object), typeof(BookPageViewModel), new PropertyMetadata(default(object)));

        #endregion


        #region Constructor
        public BookPageViewModel()
        {
            Font = "Times New Roman";
            Date = DateTime.Today;
            UpdateAvailableRooms();
            BackCommand = new RelayCommand(Back);
            BookRoomCommand = new RelayCommand(BookRoom);
            LoggedUser = Persistency.Persistency.GetUsers()[0];
        }
        #endregion


        #region Properties
        public User LoggedUser { get; set; }
        public string Font { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand BookRoomCommand { get; set; }

        public int SelectedRoom
        {
            get => _selectedRoom;
            set { _selectedRoom = value; OnPropertyChanged();}
        }

        public ObservableCollection<Facility> AvailableRooms
        {
            get => _availableRooms;
            set { _availableRooms = value; OnPropertyChanged();}
        }

        public DateTimeOffset Date
        {
            get => _date;
            set { _date = value; UpdateAvailableRooms(); OnPropertyChanged();}
        }

        //public object UserViewModel
        //{
        //    get { return (object) GetValue(UserViewModelProperty); }
        //    set { SetValue(UserViewModelProperty, value); }
        //}

        #endregion


        #region Methods
        public void UpdateAvailableRooms()
        {
            var BookingsOnDate = from booking in Persistency.Persistency.GetBookings()
                where booking.Date == Date.DateTime
                select booking;
            var updatedAvailableRooms = new ObservableCollection<Facility>(Persistency.Persistency.GetFacilities());
            List<int> doubleRoomsBooked = new List<int>();
            foreach (var booking in BookingsOnDate)
            {
                var linqForRoom = from availableRoom in updatedAvailableRooms
                    where booking.FacilityNo == availableRoom.FacilityNo
                    select availableRoom;
                var bookedRoom = linqForRoom.Count() == 1 ? linqForRoom.First() : null;
                if (bookedRoom != null)
                {
                    if (bookedRoom.Size == 2)
                    {
                        if (doubleRoomsBooked.Contains(bookedRoom.FacilityNo))
                        {
                            updatedAvailableRooms.Remove(bookedRoom);
                        }
                        else
                        {
                            doubleRoomsBooked.Add(bookedRoom.FacilityNo);
                        }
                    }
                    else
                    {
                        updatedAvailableRooms.Remove(bookedRoom);
                    }
                }
            }

            AvailableRooms = updatedAvailableRooms;
        }

        public void Back()
        {

        }

        public void BookRoom()
        {
            if (SelectedRoom >= 0 && SelectedRoom < AvailableRooms.Count)
            {
                var newBooking = new Booking {BookingNo = Persistency.Persistency.GetBookings().Count + 1,Date = Date.DateTime,Email = LoggedUser.Email,FacilityNo = AvailableRooms[SelectedRoom].FacilityNo};
                Persistency.Persistency.AddBooking(newBooking);
                int AvailableRoomsSize = AvailableRooms.Count;
                int tempSelectedRoom = SelectedRoom;
                UpdateAvailableRooms();
                if (AvailableRoomsSize == AvailableRooms.Count)
                {
                    SelectedRoom = tempSelectedRoom;
                }
            }
        }
        #endregion


        #region IPropertyChanged implement
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
    }
}
