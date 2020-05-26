using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking_app.Model
{
    public class Facility
    {
        public int FacilityNo { get; set; }
        public int Floor { get; set; }
        public int Size { get; set; }

        public override string ToString()
        {
            return $"Class Room: {FacilityNo}, {nameof(Floor)}: {Floor}, {nameof(Size)}: {Size}";
        }
    }
}
