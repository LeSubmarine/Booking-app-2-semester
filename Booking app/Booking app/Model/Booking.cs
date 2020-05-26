using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking_app.Model
{
    public class Booking
    {
        public int BookingNo { get; set; }
        public int FacilityNo { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return $"Class Room: {FacilityNo}, {nameof(Email)}: {Email}, {nameof(Date)}: {Date}";
        }

    }
}
