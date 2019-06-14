using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    static class Compare
    {
        

        public static List<Container> ListCompare(List<Container> desktop, List<Container> laptop)
        {
            //Both containers will have an index of 2, so [0] and [1]
            //[0] == Favourites
            //[1] == General
            List<Container> newList = new List<Container>();
            foreach (Container c in laptop)
            {
                newList.Add(c);
            }


            bool ContainerMatch = false, BoxMatch = false;


            foreach (Container desktopContainer in desktop)
            {
                ContainerMatch = false; //Reset ContainerMatch
                foreach (Container laptopContainer in laptop)
                {
                    if (desktopContainer.title == laptopContainer.title)
                    {
                        //Match == true
                        ContainerMatch = true;
                        //Check Boxes
                        foreach (Box desktopBox in desktopContainer.boxList)
                        {
                            BoxMatch = false; //Reset Box Match
                            foreach (Box laptopBox in laptopContainer.boxList)
                            {
                                if (desktopBox.title == laptopBox.title)
                                {
                                    //Match == true
                                    BoxMatch = true;
                                    //Check Entities
                                    foreach (Entity desktopEntity in desktopBox.entityList)
                                    {
                                        foreach (Entity laptopEntity in laptopBox.entityList)
                                        {
                                            if (desktopEntity.title == laptopEntity.title)
                                            {
                                                //Match(title) == true
                                                //Match(data)?
                                                if (desktopEntity.data != laptopEntity.data)
                                                {
                                                    //Add desktopEntity to laptopEntity list
                                                    newList[newList.IndexOf(laptopContainer)].
                                                        boxList[newList[newList.IndexOf(laptopContainer)].
                                                        boxList.IndexOf(laptopBox)].
                                                        entityList.Add(desktopEntity);
                                                }
                                                break;
                                            }
                                        }
                                    }
                                    //Break loop
                                    break;
                                }
                            }

                            //If no match, add the desktopBox to the laptopContainer
                            if (!BoxMatch)
                            {
                                newList[newList.IndexOf(laptopContainer)].boxList.Add(desktopBox);
                            }
                        }
                        //Break loop
                        break;
                    }
                }

                //If no match,
                //add desktopContainer to newList
                if (!ContainerMatch)
                {
                    newList.Add(desktopContainer);
                }
            }

            return newList;
        }

        public static List<Container> LoadFile(string location)
        {
            List<Container> output = new List<Container>();
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(location);
                string line, title, data;
                //Read the first line of text
                line = sr.ReadLine();
                int i = 0, q1, q2;
                
                //Continue to read until you reach end of file
                while (!string.IsNullOrEmpty(line))
                {
                    q1 = 0;
                    q2 = 0;
                    title = "";
                    if (line.Contains("Container ~ Title = \""))
                    {
                        q1 = line.IndexOf("\"");
                        q2 = line.IndexOf("\"", q1 + 1);
                        title = line.Substring(q1+1,q2-q1-1);
                        output.Add(new Container(title));
                        //entityStr = line.Split(':');
                        //entityStr[0].Trim();
                        //entityStr[1] = entityStr[1].Substring(1);
                        //lastBox.entityList.Add(new Entity(entityStr[0], entityStr[1]));
                    }
                    else if (line.Contains("Box ~ Title = \""))
                    {
                        q1 = line.IndexOf("\"");
                        q2 = line.IndexOf("\"", q1+1);
                        title = line.Substring(q1 + 1, q2 - q1-1);
                        output[output.Count-1].boxList.Add(new Box(title));
                    }
                    else if (line.Contains("Entity ~ Title = \""))
                    {
                        q1 = line.IndexOf("\"");
                        q2 = line.IndexOf("\"", q1 + 1);
                        title = line.Substring(q1 + 1, q2 - q1-1);
                        line = line.Substring(q2+1);
                        q1 = line.IndexOf("\"");
                        q2 = line.IndexOf("\"", q1+1);
                        data = line.Substring(q1 + 1, q2 - q1-1);
                        output[output.Count - 1].
                            boxList[output[output.Count-1].boxList.Count-1].
                            entityList.Add(new Entity(title,data));
                    }
                    //Read the next line
                    line = sr.ReadLine();
                }

                //close the file
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
            return output;
        }
    }
}
