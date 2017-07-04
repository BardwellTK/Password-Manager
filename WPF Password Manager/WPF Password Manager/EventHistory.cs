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
        private LinkedList<Deed> _undoList; //Need to remove first and add last
        private Stack<Deed> _redoList; //Need to push and pop and clear
        //private int _selectedEvent;
        //private Deed _selectedObject;
        public EventHistory()
        {
            _undoList = new LinkedList<Deed>();
            _redoList = new Stack<Deed>();
        }

        public void NewEvent(MenuLocation menu, EventType eve, Container before, Container after)
        {
            Deed newDeed = new Deed(menu, eve, before, after);
            if (RedoCount > 0)
            {
                //If Redo.Count > 0, and NewEvent
                //Clear Redo
                _redoList.Clear();
            }

            if (UndoCount < 30) //limit list to 30
            {
                //Add Deed to list
                _undoList.AddLast(newDeed);
            }
            else
            {
                //balance list
                _undoList.RemoveFirst();
                _undoList.AddLast(newDeed);
            }
            //_selectedEvent = (_undoList.Count - 1);
            //_selectedObject = _undoList[_selectedEvent];

        }

        public Deed Undo()
        {
            if (UndoCount > 0)
            {
                var deed = _undoList.Last();
                _undoList.RemoveLast();
                _redoList.Push(deed);
                return deed;
            }
            return null;
        }

        public Deed Redo()
        {
            if (RedoCount > 0)
            {
                var deed = _redoList.Pop();
                _undoList.AddLast(deed);
                return deed;
            }
            return null;
        }

        //public Deed SelectedItem { get { return _selectedObject; } }
        public int UndoCount { get { return _undoList.Count; } }
        public int RedoCount { get { return _redoList.Count; } }
        //public int SelectedEvent { get { return _selectedEvent; } }


    }
}
