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
        public static List<ZealandUser> GetUsers()
        {
            return (new ManageZealandUser()).Get();
        }

        public static List<Booking> GetBookings()
        {
            return (new ManageBooking()).Get();
        }

        public static List<Facility> GetFacilities()
        {
            return (new ManageFacility()).Get();
        }

        public static List<Booking> NewBookings()
        {
            var temp = new ManageBooking();
            temp.Create(new Booking {BookingNo = 1, Email = "henrik@henrik.dk",Date = (DateTime.Today).AddDays(4),FacilityNo = 0});
            temp.Create(new Booking {BookingNo = 2, Email = "henrik@henrik.dk",Date = (DateTime.Today),FacilityNo = 5});
            temp.Create(new Booking {BookingNo = 3, Email = "mike@mike.dk",Date = (DateTime.Today).AddDays(1),FacilityNo = 4});
            temp.Create(new Booking {BookingNo = 4, Email = "mike@mike.dk",Date = (DateTime.Today),FacilityNo = 0});
            temp.Create(new Booking {BookingNo = 5, Email = "tobi@tobi.dk",Date = (DateTime.Today),FacilityNo = 1});
            temp.Create(new Booking {BookingNo = 6, Email = "tobi@tobi.dk",Date = (DateTime.Today),FacilityNo = 4});
            temp.Create(new Booking {BookingNo = 7, Email = "lærer@lærer.dk",Date = (DateTime.Today).AddDays(1),FacilityNo = 5});
            temp.Create(new Booking {BookingNo = 8, Email = "lærer@lærer.dk",Date = (DateTime.Today).AddDays(2),FacilityNo = 1});


            return temp.Get();
        }

        public static void AddUser(ZealandUser user)
        {
            (new ManageZealandUser()).Create(user);
        }

        public static void AddBooking(Booking booking)
        {
            (new ManageBooking()).Create(booking);
        }

        public static void RemoveBooking(Booking removedBooking)
        {
            (new ManageBooking()).Delete(removedBooking.BookingNo);
        }
    }
}
