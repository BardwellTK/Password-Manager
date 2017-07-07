using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Password_Manager
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Options: Shows or hides the user options.
        /// </summary>
        private string labelMenuContent;
        private void Options(object sender, RoutedEventArgs e)
        {
            //Options is a different tree to the MenuLocation tree
            // (condition) ? [true path] : [false path] -----

            if (panelOptions.IsVisible)
            {
                //hide panel
                panelOptions.Visibility = Visibility.Hidden;
                //change labelMenu to original value
                labelMenu.Content = labelMenuContent;
                //change recent
                labelRecent.Text = $"Returned to '{(string)labelMenu.Content}' from Options";
                OptionsTabControl(true);
            }
            else
            {
                //make panel visible
                panelOptions.Visibility = Visibility.Visible;
                //cast needed not sure why; save value of labelMenu
                labelMenuContent = (string)labelMenu.Content;
                //change labelMenu to options
                labelMenu.Content = "Options";
                //change recent
                labelRecent.Text = "Opened Options";
                OptionsTabControl(false);
            }
        }

        
    }
}
