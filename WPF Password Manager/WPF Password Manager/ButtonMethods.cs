using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPF_Password_Manager.DataTypes;

namespace WPF_Password_Manager
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Delete: Deletes selected item from listView.
        /// - Event
        /// </summary>
        private void Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                //define two strings for event output

                var c = (Container)listViewer.SelectedItem;
                StaticDelete(c);
                _eventHistory.NewEvent(menuIndex, EventType.Delete, c.Copy(), null);
                EventButtonHandler();

            }
            catch (ArgumentOutOfRangeException)
            {
                Error("deleting item [no item selected]");
            }
            catch (Exception)
            {
                Error("deleting item [unknown]");
            }

        }



        /// <summary>
        /// Select: Selects selected item from listView.
        /// - Event
        /// </summary>
        private void Select(object sender, RoutedEventArgs e)
        {
            try
            {
                if (listViewer.SelectedItem != null)
                {
                    if (menuIndex == MenuLocation.Box)
                    {
                      //Call the copy argument in the event that user double clicks to copy data
                      Copy(sender, e);
                    }
                    else
                    {

                        if (menuIndex == MenuLocation.Main)
                        {
                          SelectedContainer = (Container)listViewer.SelectedItem;
                            StaticSelect(SelectedContainer);
                            //_eventHistory.Reset();
                            _eventHistory.NewEvent(MenuLocation.Main, EventType.Select, SelectedContainer, null);
                            EventButtonHandler();
                        }
                        else if (menuIndex == MenuLocation.Container)
                        {
                            SelectedContainer = (Container)listViewer.SelectedItem;
                            StaticSelect(SelectedContainer);
                            //_eventHistory.Reset();
                            _eventHistory.NewEvent(MenuLocation.Container, EventType.Select, SelectedContainer, null);
                            EventButtonHandler();
                        }
                    }
                }
            }
            catch (Exception)
            {
                Error("selecting item");
                MenuHandler();
            }
        }

        /// <summary>
        /// Copy: Copies selected item data from listView to clipboard.
        /// - Only applicable on MenuLocation.Box
        /// </summary>
        private void Copy(object sender, RoutedEventArgs e)
        {
            //Copy function here!
            try
            {
                //Get selected item from listview
                var c = (Container)listViewer.SelectedItem;
                //Match it to container from menu
                foreach (Container container in SelectedContainer.GetList())
                {
                    if (c.ID == container.ID)
                    {
                        c = container;
                        break;
                    }
                }
                //TODO

                //Copy data from matched item.
                Clipboard.SetText(c.Data);
                labelRecent.Text = $"Successfully copied '{c.Title}' from '{c.Parent.Title}'";
            }
            catch (Exception)
            {
                //Report error in Recent box
                Error("copying");
            }
        }

        /// <summary>
        /// Edit: Changes selected item data from listView.
        /// - Only applicable on MenuLocation.Box
        /// - Event
        /// </summary>
        private void Edit(object sender, RoutedEventArgs e)
        {
            try
            {
                if (menuIndex == MenuLocation.Box) //You are in the box, looking for the entities....
                {
                    //NOTE: NULL VALUES OF INPUT DATA ALLOWED HERE
                    var c = (Container)listViewer.SelectedItem;

                    //update label data change
                    labelRecent.Text = $"'{c.Data}' " +
                                       $"in '{c.Title}'" +
                                       $" was changed to '{InputData.Text}'";
                    //change data to textbox.text
                    var copy = c.Copy();
                    c.Data = InputData.Text;
                    c = c.Copy();
                    _eventHistory.NewEvent(menuIndex,EventType.Edit,copy,c);
                    EventButtonHandler();
                    //clear textbox
                    InputData.Clear();
                    //save
                    SaveCheck();
                    //refresh listViewer
                    MenuHandler();

                }
            }
            catch (NullReferenceException)
            {
                Error(ErrorCode.NullReferenceException);
            }
            catch (Exception)
            {
                Error("editing data"); //error editing data
            }
        }

        /// <summary>
        /// ReTitle: Changes selected item Title from listView.
        /// - Event
        /// </summary>
        private void ReTitle(object sender, RoutedEventArgs e)
        {
            try
            {
                //NOTE: NULL VALUES NOT ALLOWED!!!!
                if (InputTitle.Text != "")
                {
                    //get selected container
                    var c = (Container)listViewer.SelectedItem;
                    //get input title
                    string t = InputTitle.Text;
                    //clear textbox
                    InputTitle.Clear();
                    if (c.Parent.TitleCheck(t))
                    {
                        //update label
                        labelRecent.Text = $"'{c.Title}' in '{c.Parent.Title}' was changed to '{t}'";
                        var temp = c.Copy();

                        //update title
                        c.Title = t;
                        c = c.Copy();
                        _eventHistory.NewEvent(menuIndex, EventType.ReTitle, temp, c);
                        EventButtonHandler();
                    }
                    else
                    {
                        throw new Exception("Title already in use.");
                    }

                    //save
                    SaveCheck();
                    //refresh menu
                    MenuHandler();
                }
                else
                {
                    throw new Exception("");
                }
            }
            catch (NullReferenceException)
            {
                Error(ErrorCode.NullReferenceException);
            }
            catch (Exception)
            {
                Error("changing title");
            }
        }

        /// <summary>
        /// Add: Adds new item to listView.
        /// - Event
        /// </summary>
        private void Add(object sender, RoutedEventArgs e)
        {
            try
            {
                if (InputTitle.Text != "")
                {
                    //set title and data
                    string t = InputTitle.Text;
                    string d = InputData.Text;
                    //clear textboxes
                    InputTitle.Clear();
                    InputData.Clear();
                    StaticAdd(t,d);
                    var c = SelectedContainer.GetList();
                    var b = c[SelectedContainer.Count - 1].Copy();
                    _eventHistory.NewEvent(menuIndex, EventType.Add, b, null);
                    EventButtonHandler();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception f)
            {
                Console.WriteLine(f.ToString());
            }
        }

        /// <summary>
        /// Back: Returns to parent item in listView.
        /// - Event
        /// </summary>
        private void Back(object sender, RoutedEventArgs e)
        {
            _eventHistory.NewEvent(menuIndex, EventType.Back, SelectedContainer.Parent, null);
            //_eventHistory.Reset();
            StaticBack();
            EventButtonHandler();
        }

        /// <summary>
        /// Search: Filters visible items in listView
        /// </summary>
        bool SearchMode;
        private void Search(object sender, RoutedEventArgs e)
        {
            //find text in textbox
            string search = InputSearch.Text.ToLower();

            if (!(String.IsNullOrEmpty(search) || String.IsNullOrWhiteSpace(search)))
            {
                //for each item...

                List<Container> containerList = SelectedContainer.GetList();
                if (menuIndex == MenuLocation.Main)
                {
                    containerList = containers.GetList();
                }
                List<Container> outList = new List<Container>();
                SearchMode = true;

                foreach(var item in containerList)
                {
                    string t = item.Title.ToLower();
                    if (t.Contains(search))
                    {
                        outList.Add(item);
                    }
                }

                SearchMenu(outList);

            }
            else
            {
                MenuHandler();
                SearchMode = false;
            }
        }


        

        private void OptionsTabControl(bool options)
        {
            if (options)
            {
                InputData.IsTabStop = false;
                InputSearch.IsTabStop = false;
                InputTitle.IsTabStop = false;
                listViewer.IsTabStop = false;
            }
            else
            {
                InputData.IsTabStop = true;
                InputSearch.IsTabStop = true;
                InputTitle.IsTabStop = true;
                listViewer.IsTabStop = true;
            }
        }


        /// <summary>
        /// Button Handler: Manages which buttons are enabled or disabled.
        /// </summary>
        private void ButtonHandler()
        {
            switch (menuIndex)
            {
                case MenuLocation.Main:
                    //Enable and disable appropriate buttons
                    //ON
                    buttonReTitle.IsEnabled = true;
                    buttonAdd.IsEnabled = true;
                    buttonSelect.Content = "Select";
                    buttonDelete.IsEnabled = true;
                    //OFF
                    buttonBack.IsEnabled = false;
                    buttonEdit.IsEnabled = false;
                    InputData.IsEnabled = false;
                    break;
                case MenuLocation.Container:
                    //Enable and disable appropriate buttons
                    //ON
                    buttonReTitle.IsEnabled = true;
                    buttonAdd.IsEnabled = true;
                    buttonSelect.Content = "Select";
                    buttonDelete.IsEnabled = true;
                    buttonBack.IsEnabled = true;
                    //OFF
                    buttonEdit.IsEnabled = false;
                    InputData.IsEnabled = false;
                    break;
                case MenuLocation.Box:
                    //Enable and disable appropriate buttons
                    //ON
                    buttonReTitle.IsEnabled = true;
                    buttonAdd.IsEnabled = true;
                    buttonDelete.IsEnabled = true;
                    buttonBack.IsEnabled = true;
                    buttonEdit.IsEnabled = true; //Re-Data
                    InputData.IsEnabled = true;
                    buttonSelect.Content = "Copy";
                    //OFF
                    //--none
                    break;
                default:
                    //Report error and continue and return to main menu
                    Error("switching menu", true);
                    break;
            }
        }

        /// <summary>
        /// Undo: Scrolls back through events
        /// </summary>
        private void Undo(object sender, RoutedEventArgs e)
        {
            //decrypt deed;
            DecryptDeed(true);
            EventButtonHandler();
        }

        /// <summary>
        /// Redo: Scrolls forward through events
        /// </summary>
        private void Redo(object sender, RoutedEventArgs e)
        {
            DecryptDeed(false);
            EventButtonHandler();
        }

        private void EventButtonHandler()
        {
            
                if (_eventHistory.UndoCount > 0)
                {
                    buttonUndo.IsEnabled = true;
                }
                else
                {
                    buttonUndo.IsEnabled = false;
                }

                if (_eventHistory.RedoCount > 0)
                {
                    buttonRedo.IsEnabled = true;
                }
                else
                {
                    buttonRedo.IsEnabled = false;
                }
            
        }
    }
}
