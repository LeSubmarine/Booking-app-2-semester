using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Booking_app.Utility
{
    public class MessageDialogChoiceHelper
    {
        public static async Task<bool> YesNoBoxAsync(string title, string content)
        {

            MessageDialog dialog = new MessageDialog(content,title);
            dialog.Commands.Add(new UICommand("Yes", null));
            dialog.Commands.Add(new UICommand("No", null));
            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;
            var cmd = await dialog.ShowAsync();

            if (cmd.Label == "Yes")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
}
}
