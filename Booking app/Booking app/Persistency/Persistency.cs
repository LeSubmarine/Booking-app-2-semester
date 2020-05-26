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
