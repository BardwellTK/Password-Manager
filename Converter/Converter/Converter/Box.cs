using System.Collections.Generic;

namespace Converter
{
    class Box
    {
        public List<Entity> entityList;
        public string title;
        public Box(string inTitle)
        {
            title = inTitle;
            entityList = new List<Entity>();
        }
    }
}
