using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using WPF_Password_Manager.DataTypes;

namespace WPF_Password_Manager
{
    public partial class MainWindow
    {

        private void MenuHandler()
        {
            listViewer.Items.Clear(); //Saves Data redundancy here
            ColumnSwitch(); //Change columns before adding Data to the listViewer
            ButtonHandler(); //Switch buttons on and off based on menu index value
            PrintMenu();
        }

        private void SearchMenu(List<Container> list)
        {
            listViewer.Items.Clear(); //Saves Data redundancy here
            ColumnSwitch(); //Change columns before adding Data to the listViewer
            ButtonHandler(); //Switch buttons on and off based on menu index value
            foreach (var item in list)
            {
                //by adding the item directly, there is no worries about what fields
                //are put where, because the menuIndex is set, the program knows
                //how to lay the data out from 'v', all it needs is the object!
                listViewer.Items.Add(item);
            }
        }
        
        private void PrintMenu()
        {
            //Load the main menu
            var t = SelectedContainer.Title;
            if (menuIndex != MenuLocation.Main)
            {
                t = $"{SelectedContainer.Parent.Title} - {t}";
            }
            labelMenu.Content = $"{t}";
            var c = SelectedContainer.GetList();
            foreach (var item in c)
            {
                listViewer.Items.Add(item);
            }
        }

        private GridViewColumn ColumnData = new GridViewColumn{Header = "Data",DisplayMemberBinding = new Binding("Data")};
        private void ColumnSwitch()
        {
            var gridView = (GridView)listViewer.View;
            if (menuIndex == MenuLocation.Box && !gridView.Columns.Contains(ColumnData))
            {
                gridView.Columns.Add(ColumnData);
                
            }
            else if (menuIndex != MenuLocation.Box && gridView.Columns.Contains(ColumnData))
            {
                gridView.Columns.Remove(ColumnData);
            }
        }

    }
}
