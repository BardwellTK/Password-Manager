using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Password_Manager.DataTypes
{
    interface IIdentity
    {
        int UniqueID {get;}
        int ID { get; set; }
    }
}
