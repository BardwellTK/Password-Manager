using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Password_Manager.DataTypes
{
    public class IdentityObject : IIdentity
    {
        private int _iD;
        public int ID { get { return _iD; } set { _iD = value; } }
    }
}
