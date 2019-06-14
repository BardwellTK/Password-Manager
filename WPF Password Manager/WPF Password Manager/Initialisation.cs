using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WPF_Password_Manager.DataTypes;
using Brushes = System.Windows.Media.Brushes;
using FontFamily = System.Drawing.FontFamily;

namespace WPF_Password_Manager
{
    public partial class MainWindow : Window
    {
        EventHistory _eventHistory;
        Task saveFile;
        private void InitializeWindow()
        {
            containers = new Container(0,"Main");
            SelectedContainer = containers;
            LoadFile();
            saveFile = new Task(Save);
            panelOptions.Visibility = Visibility.Hidden;
            labelMenuContent = "Main";
            InitializeButtons(); //Initialises buttons
            SelectedContainer = containers;
            InitializeColumns();
            MenuHandler(); //Arranges menu
            labelMenu.MaxWidth = 226;
            _eventHistory = new EventHistory();
            labelDataProtection.Foreground = Brushes.ForestGreen;
            labelEasyRead.Foreground = Brushes.Firebrick;
        }

        private void InitializeButtons()
        {
            //Disable the buttons
            buttonBack.IsEnabled = false;
            buttonDelete.IsEnabled = false;
            buttonAdd.IsEnabled = false;
            buttonEdit.IsEnabled = false;
            buttonReTitle.IsEnabled = false;
            buttonUndo.IsEnabled = false;
            buttonRedo.IsEnabled = false;
            InputData.IsEnabled = false; //disable Data input
        }

        private void InitializeColumns()
        {
            var gridView = new GridView();
            listViewer.View = gridView;
            //gridView.Columns.Add(new GridViewColumn
            //{
            //    Header = "#",
            //    DisplayMemberBinding = new Binding("ID")
            //});
            //gridView.Columns.Add(new GridViewColumn
            //{
            //    Header = "Title",
            //    DisplayMemberBinding = new Binding("Title")
            //});
        }
    }
}
