using System;
using System.Collections.Generic;

namespace Converter
{
    class Container
    {
        public List<Box> boxList;
        public string title;
        public string domain;
        public Container(string inTitle)
        {
            title = inTitle;
            boxList = new List<Box>();
        }


        public override string ToString()
        {
            string output;
            output = $"Container ~ Title = \"{title}\"\n";
            foreach (Box b in boxList)
            {
                output += $"Box ~ Title = \"{b.title}\"\n";
                foreach (Entity e in b.entityList)
                {
                    output += $"Entity ~ Title = \"{e.title}\" Data = \"{e.data}\"\n";
                }
            }
            return output;
        }

        public Container Copy()
        {
            //Performs a full copy, so the ejected object is unrelated to this in memory.
            Container copy = new Container("title");
            int counter = 0;
            foreach (Box b in boxList)
            {
                copy.boxList.Add(new Box(b.title));
                foreach (Entity e in b.entityList)
                {
                    copy.boxList[counter].entityList.Add(new Entity(e.title, e.data));
                }
                counter++;
            }
            return copy;
        }

        public List<String> Save()
        {
            List<string> output = new List<string>();
            output.Add($"Container ~ Title = \"{title}\"");
            foreach (Box b in boxList)
            {
                output.Add($"Box ~ Title = \"{b.title}\"");
                foreach (Entity e in b.entityList)
                {
                    output.Add($"Entity ~ Title = \"{e.title}\" Data = \"{e.data}\"");
                }
            }
            return output;
        }
    }
}
