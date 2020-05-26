using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Booking_app.Model;

namespace Booking_app.Persistency
{
    public class Persistency
    {
        public static List<User> Users { get; set; } = new List<User>(NewUsers());
        public static List<Booking> Bookings { get; set; } = new List<Booking>(NewBookings());
        public static List<Facility> Facilities { get; set; } = new List<Facility>(NewFacilities());

        public static List<User> GetUsers()
        {
            return Users;
        }

        

        public static void SaveUsers(List<User> users)
        {
            Users = users;
        }

        public static List<Booking> GetBookings()
        {
            return Bookings;
        }

        public static void SaveBooking(List<Booking> bookings)
        {
            Bookings = bookings;
        }

        public static List<Facility> GetFacilities()
        {
            return Facilities;
        }

        public static void SaveFacilities(List<Facility> facilities)
        {
            Facilities = facilities;
        }


        public static List<User> NewUsers()
        {
            List<User> tempUsers = new List<User>();
            tempUsers.Add(new User{Email = "henrik@henrik.dk",Name = "Henrik",Password = "Henrik password"});
            tempUsers.Add(new User{Email = "mike@mike.dk",Name = "Mike",Password = "Mike password"});
            tempUsers.Add(new User{Email = "tobi@tobi.dk",Name = "Tobi",Password = "Tobi password"});
            tempUsers.Add(new User{Email = "lærer@lærer.dk",Name = "Lærer",Password = "Lærer password"});
            tempUsers.Add(new User { Email = "jan", Name = "Jan", Password = "jan" });

            return tempUsers;
        }


        public static List<Booking> NewBookings()
        {
            List<Booking> tempBookings = new List<Booking>();
            tempBookings.Add(new Booking {BookingNo = 1, Email = "henrik@henrik.dk",Date = (DateTime.Today).AddDays(4),FacilityNo = 0});
            tempBookings.Add(new Booking {BookingNo = 2, Email = "henrik@henrik.dk",Date = (DateTime.Today),FacilityNo = 5});
            tempBookings.Add(new Booking {BookingNo = 3, Email = "mike@mike.dk",Date = (DateTime.Today).AddDays(1),FacilityNo = 4});
            tempBookings.Add(new Booking {BookingNo = 4, Email = "mike@mike.dk",Date = (DateTime.Today),FacilityNo = 0});
            tempBookings.Add(new Booking {BookingNo = 5, Email = "tobi@tobi.dk",Date = (DateTime.Today),FacilityNo = 1});
            tempBookings.Add(new Booking {BookingNo = 6, Email = "tobi@tobi.dk",Date = (DateTime.Today),FacilityNo = 4});
            tempBookings.Add(new Booking {BookingNo = 7, Email = "lærer@lærer.dk",Date = (DateTime.Today).AddDays(1),FacilityNo = 5});
            tempBookings.Add(new Booking {BookingNo = 8, Email = "lærer@lærer.dk",Date = (DateTime.Today).AddDays(2),FacilityNo = 1});


            return tempBookings;
        }
        public static List<Facility> NewFacilities()
        {
            List<Facility> tempFacilities = new List<Facility>();
            tempFacilities.Add(new Facility {FacilityNo = 1, Floor = 1, Size = 1});
            tempFacilities.Add(new Facility {FacilityNo = 2, Floor = 1, Size = 2});
            tempFacilities.Add(new Facility {FacilityNo = 3, Floor = 1, Size = 2});
            tempFacilities.Add(new Facility {FacilityNo = 4, Floor = 1, Size = 2});
            tempFacilities.Add(new Facility {FacilityNo = 5, Floor = 2, Size = 1});
            tempFacilities.Add(new Facility {FacilityNo = 6, Floor = 2, Size = 2});
            tempFacilities.Add(new Facility {FacilityNo = 7, Floor = 2, Size = 1});
            tempFacilities.Add(new Facility {FacilityNo = 8, Floor = 2, Size = 1});


            return tempFacilities;
        }

        public static void AddUser(User user)
        {
            var users = GetUsers();
            users.Add(user);
            SaveUsers(users);
        }

        public static void AddBooking(Booking booking)
        {
            var bookings = GetBookings();
            bookings.Add(booking);
            SaveBooking(bookings);
        }

        public static void RemoveBooking(Booking removedBooking)
        {
            var bookings = GetBookings();
            bookings.Remove(removedBooking);
            SaveBooking(bookings);
        }
    }
}
