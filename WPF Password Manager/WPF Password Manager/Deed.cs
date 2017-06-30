using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Password_Manager.DataTypes;
using static WPF_Password_Manager.MainWindow;

namespace WPF_Password_Manager
{
    public class Deed
    {
        /// <summary>
        /// Deed is the data type used to hold information for EventHistory
        /// A Deed will hold the event that occurred, before state, and after state
        /// </summary>
        private MenuLocation _menuIndex;
        private EventType _action;
        private Container _before, _after;

        public Deed(MenuLocation menuIndex, EventType action, Container before)
        {
            _menuIndex = menuIndex;
            _action = action;
            _before = before;
        }

        public Deed(MenuLocation menuIndex, EventType action, Container before, Container after)
        {
            _menuIndex = menuIndex;
            _action = action;
            _before = before;
            _after = after;
        }

        
        
        public MenuLocation Menu { get { return _menuIndex; } }
        public EventType Action { get { return _action; } }
        public Container Before { get { return _before; } }
        public Container After { get { return _after; } }

    }
}
