using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Brushes = System.Windows.Media.Brushes;
using FontFamily = System.Windows.Media.FontFamily;

namespace WPF_Password_Manager
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Options: Shows or hides the user options.
        /// </summary>
        private string labelMenuContent;

        private bool dataProtection = true;
        private bool easyRead = false;

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
                //Re-draw menu
                MenuHandler();
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

        private List<Control> allControls;
        private FontFamily easyReadFontStyle;
        private Font easyReadFont;
        private void EasyRead()
        {
            //Toggle easy read
            easyRead = !easyRead;

            if (easyRead)
            {
                labelEasyRead.Foreground = Brushes.ForestGreen;
                foreach (Control tb in FindVisualChildren<Control>(this))
                {
                    // do something with tb here
                    tb.FontFamily = new System.Windows.Media.FontFamily("Segoe UI");
                    tb.FontSize = 18;
                    if (tb.Name == "labelMenu")
                    {
                        tb.FontSize = 18;
                    }
                }
                labelEasyRead.Content = "Status: Enabled";
                labelRecent.Text = "Easy Read Enabled";
            }
            else
            {
                labelEasyRead.Foreground = Brushes.Firebrick;
                labelEasyRead.Content = "Status: Disabled";
                labelRecent.Text = "Easy Read Disabled";
                foreach (Control tb in FindVisualChildren<Control>(this))
                {
                    // do something with tb here
                    tb.FontSize = 12;
                }
                foreach (TextBox tb in FindVisualChildren<TextBox>(this))
                {
                    // do something with tb here
                    tb.FontFamily = new FontFamily("Geomanist");
                }
                foreach (Label tb in FindVisualChildren<Label>(this))
                {
                    // do something with tb here
                    tb.FontFamily = new FontFamily("Teko");
                    tb.FontSize = 16;
                    if (tb.Name == "labelDataProtection" || tb.Name == "labelEasyRead")
                    {
                        tb.FontFamily = new FontFamily("Segoe UI");
                        tb.FontSize = 12;
                    }
                    if (tb.Name == "labelMenu")
                    {
                        tb.FontSize = 18;
                    }
                }
                foreach (Button tb in FindVisualChildren<Button>(this))
                {
                    // do something with tb here
                    tb.FontFamily = new FontFamily("Geomanist");
                }
                listViewer.FontFamily = new FontFamily("Geomanist");
            }
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private void DataProtection()
        {
            //Toggle Data Protection
            dataProtection = !dataProtection;

            if (dataProtection)
            {
                labelDataProtection.Foreground = Brushes.ForestGreen;
                labelDataProtection.Content = "Status: Enabled";
                labelRecent.Text = "Data Protection Enabled";
            }
            else
            {
                labelDataProtection.Foreground = Brushes.Firebrick;
                labelDataProtection.Content = "Status: Disabled";
                labelRecent.Text = "Data Protection Disabled";
            }
        }


        private void ButtonEasyRead_OnClick(object sender, RoutedEventArgs e)
        {
            EasyRead();
        }
    }
}
