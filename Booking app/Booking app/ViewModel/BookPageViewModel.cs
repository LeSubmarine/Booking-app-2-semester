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
using Booking_app.Persistency;

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
            LoggedUser = MainPageViewModel.LoggedUser;
            TeacherBooking = false;
        }
        #endregion


        #region Properties
        public ZealandUser LoggedUser { get; set; }
        public string Font { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand BookRoomCommand { get; set; }
        public bool TeacherBooking { get; set; }

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
            if (LoggedUser == null)
            {
                LoggedUser = MainPageViewModel.LoggedUser;
            }
            
            if (LoggedUser.Email != "lærer@lærer.dk" || Date.DateTime < DateTime.Now.AddDays(3))
            {
                var BookingsOnDate = from booking in Persistency.PersistencyService.GetBookings()
                                     where booking.Date == Date.DateTime
                                     select booking;
                var updatedAvailableRooms = new ObservableCollection<Facility>(PersistencyService.GetFacilities());
                var numberOfBookedRoomsByUser = from m in PersistencyService.GetBookings() where m.Email == LoggedUser.Email select m;
                if (numberOfBookedRoomsByUser.Count() > 2)
                {
                    for (int i = 0; i < updatedAvailableRooms.Count;)
                    {
                        updatedAvailableRooms.RemoveAt(0);
                    }
                }
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
                var ownBookings = from m in PersistencyService.GetBookings()
                    where m.Email == LoggedUser.Email && m.Date == Date.DateTime
                    select m;
                foreach (var booking in ownBookings)
                {
                    updatedAvailableRooms.Remove((from m in PersistencyService.GetFacilities() where booking.FacilityNo == m.FacilityNo select m).First());
                }
                AvailableRooms = updatedAvailableRooms;
                TeacherBooking = false;
            }
            else
            {
               AvailableRooms = new ObservableCollection<Facility>(PersistencyService.GetFacilities());
               var ownBookings = from m in PersistencyService.GetBookings()
                   where m.Email == LoggedUser.Email && m.Date == Date.DateTime
                   select m;
               foreach (var booking in ownBookings)
               {
                   AvailableRooms.Remove(PersistencyService.GetFacility(booking.FacilityNo));
               }
               TeacherBooking = true;
            }
        }

        public void Back()
        {
            Navigation.NavigateToPage("MainPage","BookPage");
        }

        public async void BookRoom()
        {
            //Er det et reelt rum der er blevet valgt
            if (SelectedRoom >= 0 && SelectedRoom < AvailableRooms.Count)
            {
                //Er det en lærer der booker
                if (TeacherBooking) //Ikke færdig - mangler error tekst
                {
                    //Vil læren gerne booke rummet selv hvis det allerede er booket?
                    if (await CheckIfBookedForTeacher(Date.DateTime, SelectedRoom))
                    {
                        var bookingsOnDateWithRoom = from m in PersistencyService.GetBookings()
                            where m.Date == Date && m.FacilityNo == AvailableRooms[SelectedRoom].FacilityNo
                            select m;
                        //Hvis der ikke er andre bookinger kører vi bare
                        if (bookingsOnDateWithRoom.Count() == 0)
                        {
                            AddBooking(Date.DateTime, LoggedUser.Email, AvailableRooms[SelectedRoom].FacilityNo);
                            UpdateAvailableRooms();
                        }
                        //Hvis der er en booking
                        else if (bookingsOnDateWithRoom.Count() == 1)
                        {
                            //Hvis det er et stort rum no problem
                            if (PersistencyService.GetFacility(bookingsOnDateWithRoom.First().FacilityNo).Size == 2)
                            {
                                AddBooking(Date.DateTime, LoggedUser.Email, AvailableRooms[SelectedRoom].FacilityNo);
                            }
                            //Hvis vi skal slette personens rum
                            else
                            {
                                //Vi sletter kun hvis det er en elevs rum
                                if (!ZealandUser.IsTeacher(
                                    PersistencyService.GetUser(bookingsOnDateWithRoom.Last().Email)))
                                {
                                    var newBooking = bookingsOnDateWithRoom.Last();
                                    newBooking.Email = LoggedUser.Email;
                                    PersistencyService.UpdateBooking(bookingsOnDateWithRoom.Last().BookingNo,
                                        newBooking);
                                }
                                else
                                {
                                    MessageDialogHelper.Show("This room is booked by another teacher", "Error");
                                }
                            }
                        }
                        //Hvis der er to bookinger
                        else
                        {
                            //Hvis den sidste booking af de to er af en lærer
                            if (ZealandUser.IsTeacher(PersistencyService.GetUser(bookingsOnDateWithRoom.Last().Email)))
                            {
                                //Hvis den første ikke er af en lærer
                                if (!ZealandUser.IsTeacher(PersistencyService.GetUser(bookingsOnDateWithRoom.First().Email)))
                                {
                                    var newBooking = bookingsOnDateWithRoom.First();
                                    newBooking.Email = LoggedUser.Email;
                                    PersistencyService.UpdateBooking(bookingsOnDateWithRoom.First().BookingNo, newBooking);
                                }
                                //Hvis begge rum er booket af en lærer
                                else
                                {
                                    MessageDialogHelper.Show("This room is booked by another teacher", "Error");
                                }
                            }
                            //Hvis det sidste rum ikke var booket af en lærer
                            else
                            {
                                var newBooking = bookingsOnDateWithRoom.Last();
                                newBooking.Email = LoggedUser.Email;
                                PersistencyService.UpdateBooking(bookingsOnDateWithRoom.Last().BookingNo, newBooking);
                            }
                        }
                    }
                }
                //Det er ikke en lærer der booker
                else
                {
                    AddBooking(Date.DateTime,LoggedUser.Email,AvailableRooms[SelectedRoom].FacilityNo);
                    UpdateAvailableRooms();
                }
                
            }
        }

        public void AddBooking(DateTime date, string email, int facilityNo)
        {
            List<Booking> allBookings = PersistencyService.GetBookings();
            Booking newBooking = new Booking { BookingNo = allBookings.Count + 1, Date = date, Email = email, FacilityNo = facilityNo };

            for (int i = 0; i < allBookings.Count; i++)
            {
                if (!((from m in allBookings where m.BookingNo == i select m).Any()))
                {
                    newBooking.BookingNo = i;
                    break;
                }
            }
            PersistencyService.AddBooking(newBooking);
        }

        public async Task<bool> CheckIfBookedForTeacher(DateTime date, int roomNo)
        {
            var bookingsOnDateWithRoom = from m in Persistency.PersistencyService.GetBookings()
                where m.Date == date && m.FacilityNo == roomNo
                select m;
            if (bookingsOnDateWithRoom.Any())
            {
                return await MessageDialogChoiceHelper.YesNoBoxAsync("Room booked",
                    "This room is already booked by a student\nDo you still want to proceed with the booking?");
            }
            else
            {
                return true;
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
