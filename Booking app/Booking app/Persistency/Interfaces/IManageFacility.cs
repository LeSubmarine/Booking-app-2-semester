using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking_app.Model;

namespace Booking_app.Persistency.Interfaces
{
    interface IManageFacility
    {
        List<Facility> Get();
        Facility Get(int facilityNo);
        bool Create(Facility facility);
        bool Update(Facility facility, int facilityNo);
        Facility Delete(int facilityNo);
    }
}
