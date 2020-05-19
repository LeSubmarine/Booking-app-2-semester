using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Booking_app.View;

namespace Booking_app.Utility
{
    static class Navigation
    {
        public delegate void Navigate();

        public static event Navigate navigateEvent;
        public static Type PageTarget { get; set; }
        public static string PageDeparture { get; set; }
        public static Dictionary<string,Type> PageDictionary = new Dictionary<string, Type> {{"MainPage",typeof(Booking_app.View.MainPage) }, {"Login", typeof(Login)}, {"BookPage", typeof(BookPage)},{"CreateUser", typeof(CreateUser)}};

        public static void NavigateToPage(string pageTarget, string pageDeparture)
        {
            if (navigateEvent != null)
            {
                if (PageDeparture != pageDeparture && PageTarget != PageDictionary[pageTarget])
                {
                    PageTarget = PageDictionary[pageTarget];
                    PageDeparture = pageDeparture; 
                    navigateEvent();
                }
            }
        }
    }
}
