using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Password_Manager
{
    public partial class MainWindow
    {
        private void Error(string function) //For unexpected errors, do nothing
        {
                    labelRecent.Text = $"Error: {function}";
        }

        private void Error(string function, bool main)
        {
            Error(function); //state the error to the user

            //decide whether or not to return to main menu
            if (main)
            {
                labelRecent.Text += ", returning to main menu!";
                menuIndex = MenuLocation.Main;
                MenuHandler();
            }
        }


        private void Error(ErrorCode code)
        {
            switch (code)
            {
                case ErrorCode.DeleteItem:
                    labelRecent.Text = "Error deleting item!";
                    break;
                case ErrorCode.ArgumentNullException:
                    labelRecent.Text = "Error Argument Null Exception.";
                    break;
                case ErrorCode.NullReferenceException:
                    labelRecent.Text = "Error Null Reference Exception.";
                    break;
            }
        }
    }
}
