using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Password_Manager.DataTypes
{
    public class TitleObject : IdentityObject, ITitle
    {
        private string _title;
        public string Title { get { return _title; } set { _title = value; } }
        
        
    }
}
