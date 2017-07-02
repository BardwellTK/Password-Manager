using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Password_Manager.DataTypes;

namespace WPF_Password_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// 
    /// References:
    /// #1: Embedding fonts into application: https://stackoverflow.com/questions/6453640/how-to-include-external-font-in-wpf-application-without-installing-it
    /// 
    /// Note:
    /// #1: 12/06/17 @ 13:11 -  Removed TitleItem and SubItem, redundant Data type was used in adding Data to listViewer,
    ///                         and changed {Container, Box, Entity} to have Titled fields, as in first letter is capitalised,
    ///                         then changed {Sub, Box, Main} Menus to use anonymous types to add data to listviewer
    /// Progress:
    /// #1: 12/06/17 @ 13:37 -  Delete(), Select() and Copy() complete, now to continue working on buttons.
    /// #2: 12/06/17 @ 22:09 -  Spent a lot of time procrastinating, but completed all button methods and split up the file.
    /// #3: 12/06/17 @ 23:26 -  Added ShiftList(int index) for when deleting items from list, because Id's weren't dynamically changing to fit in list
    /// #4: 12/06/17 @ 23:36 -  Minor error in ShiftList() there was a 'less than or equal to' rather than 'less than' in a for loop
    /// #5: 15/06/17 @ 22:39 -  Removed buttonCopy, replaced with buttonSelect, and buttonSelect dynamically changes to act as either select or copy
    /// #6: 15/06/17 @ 22:40 -  Organised UI a lot, added search feature, undo/redo & options buttons, also added Canvas(Panel) for options
    /// VERSION 1 (CURRENT):
    /// TODO Make Organise and refurbish event handling (UNDO & REDO)
    ///     - Add and Delete (Mostly working)
    ///     - Edit and ReTitle (Mostly working)
    ///     ----- Both aobve, sometimes run into errors when having multiple events etc
    ///     ----- and stacking on eachother, but working when only one is done.
    ///     - Back and Select (Disabled)
    ///     ----- Was really difficult to figure out how to work
    ///     ----- Was not worth the time for such a small thing
    ///     ----- Disabled functionality.
    /// TODO Make items movable through menus
    /// TODO Set up secure error handling
    /// TODO File in SQL Database
    /// TODO Saves are related to each device
    /// ----- Each device has it's own location for the data file
    /// 
    /// 
    /// VERSION 2 (NEXT STAGE):
    /// TODO Fully custom UI
    /// TODO Encrpytion
    /// </summary>
    public partial class MainWindow : Window
    {
        //Field Initialisation
        private Container containers;
        private Container SelectedContainer;
        private MenuLocation menuIndex;
        

        public MainWindow() //Constructor
        {
            InitializeComponent(); //pre-app logic
            InitializeWindow(); //create and organise variables
        }

        private void ShiftList(int index)
        {
            switch (menuIndex)
            {
                case MenuLocation.Main:
                    //remove item from container list
                    containers.RemoveAt(index);
                    containers.EvaluteID();
                    break;
                case MenuLocation.Container:
                    //remove item from container list
                    containers.Perspective.RemoveAt(index);
                    //from index, reassign list values
                    containers.Perspective.EvaluteID();
                    break;
                case MenuLocation.Box:
                    //remove item from container list
                    containers.Perspective.Perspective.RemoveAt(index);
                    containers.Perspective.Perspective.EvaluteID();
                    break;
            }
        }
        
    }
}
