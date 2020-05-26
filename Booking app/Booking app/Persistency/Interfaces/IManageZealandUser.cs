using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking_app.Model;

namespace Booking_app.Persistency.Interfaces
{
    interface IManageZealandUser
    {
        List<ZealandUser> Get();
        ZealandUser Get(string email);
        bool Create(ZealandUser zealandUser);
        bool Update(ZealandUser zealandUser, string email);
        ZealandUser Delete(string email);
    }
}
