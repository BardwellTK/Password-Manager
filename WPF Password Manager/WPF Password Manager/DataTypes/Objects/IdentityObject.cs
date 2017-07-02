using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Password_Manager.DataTypes
{
    public class IdentityObject : IIdentity
    {
        //Permanent unique ID
        private static int universalID = 0;
        private int _uniqueID;
        public int UniqueID { get { return _uniqueID; } }
        public void SetUniqueID()
        {
          _uniqueID = universalID++;
        }
        protected void ManualUniqueID(int i)
        {
            _uniqueID = i;
        }

        //Temporary ID used in list
        private int _iD;
        public int ID { get { return _iD; } set { _iD = value; } }
    }
}
