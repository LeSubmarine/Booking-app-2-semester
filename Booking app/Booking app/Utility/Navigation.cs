using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using StarfinderCharSheets.View;
using StarfinderCharSheets.View.CharCreation;

namespace StarfinderCharSheets.Utility
{
    static class Navigation
    {
        public delegate void Navigate();

        public static event Navigate navigateEvent;
        public static Type PageTarget { get; set; }
        public static string PageDeparture { get; set; }
        public static Dictionary<string,Type> PageDictionary = new Dictionary<string, Type> {{"MainPage",typeof(MainPage)},{"Names",typeof(Names)},{"RaceThemeClass",typeof(RaceThemeClass)},{"CharSheet",typeof(CharSheetViewer)}};

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
