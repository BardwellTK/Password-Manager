using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Password_Manager.DataTypes
{
    public class DataObject : TitleObject, IData
    {
        private string _data;
        public string Data { get { return _data; } set { _data = value; } }
    }
}
