using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking_app.Model;

namespace Booking_app.Persistency.Interfaces
{
    interface IManageBooking
    {
        List<Booking> Get();
        Booking Get(int bookingNo);
        bool Create(Booking booking);
        bool Update(Booking booking, int bookingNo);
        Booking Delete(int bookingNo);
    }
}
