using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking_app.Model
{
    public class ZealandUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string School { get; set; }


        public override string ToString()
        {
            return $"Email: {Email}, Password: {Password}, Name: {Name}";
        }
    }
}
