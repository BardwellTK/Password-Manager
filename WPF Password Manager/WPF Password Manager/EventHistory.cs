using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Password_Manager.DataTypes;
using static WPF_Password_Manager.MainWindow;

namespace WPF_Password_Manager
{
    public class EventHistory
    {
        private List<Deed> EventList;
        private byte _selectedEvent;
        private Deed _selectedObject;
        public EventHistory()
        {
            EventList = new List<Deed>();
        }

        public void NewEvent(MenuLocation menu,EventType eve,Container before,Container after)
        {
            Deed newDeed = new Deed(menu,eve,before,after);
            if (EventList.Count > 0 && _selectedEvent < EventList.Count - 1)
            {
                //if _selectedEvent < EventList.Count
                //then delete all Deeds after _selected event
                do
                {
                    EventList.RemoveAt(EventList.Count - 1);
                } while (_selectedEvent < EventList.Count - 1);
            }
            else if (EventList.Count < 15) //limit list to 15
            {
                //fill list
                EventList.Add(newDeed);
                
            }
            else
            {
                //balance list
                EventList.RemoveAt(0);
                EventList.Add(newDeed);
            }
            _selectedEvent = (byte)(EventList.Count - 1);

        }

        public bool Back()
        {
            if (_selectedEvent > 0)
            {
                _selectedEvent--;
                _selectedObject = EventList[_selectedEvent];
                return true;
            }
            return false;
        }

        public bool Forward()
        {
            if (_selectedEvent < 14 && _selectedEvent < EventList.Count)
            {
                _selectedEvent++;
                _selectedObject = EventList[_selectedEvent];
                return true;
            }
            return false;
        }

        public Deed SelectedItem { get { return _selectedObject; } }
        public int EventCount { get { return EventList.Count; } }

        
    }
}
