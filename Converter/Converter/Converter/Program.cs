using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    class Program
    {
        static void Main(string[] args)
        {
            string oldloc;
            string input;
            string data;

            do
            {
                input = "";
                Console.WriteLine("1. Convert\n2. Merge\n");
                input = Console.ReadLine();

                if (input == "1" || input == "2")
                {
                    break;
                }

            } while (input != "1" || input != "2");


            if (input == "2")
            {
                string[] loc = new string[2];
                int locnum = 0;
                //Merge
                do
                {
                    loc[locnum] = "";
                    input = "";
                    Console.WriteLine($"Enter file {locnum+1} location:");
                    loc[locnum] = Console.ReadLine();
                    Console.WriteLine("Confirm (y/n):");
                    input = Console.ReadLine();

                    if (input == "y")
                    {
                        if (locnum < 1)
                        {
                            locnum++;
                        }
                        else
                        {
                            break;
                        }
                        
                    }

                } while (true);


                //open loc[0] and create list
                List<Container> desktopList = Compare.LoadFile(loc[0]);
                //open loc[1] and create list
                List<Container> laptopList = Compare.LoadFile(loc[1]);
                //compare the lists
                List<Container> outputList = Compare.ListCompare(desktopList, laptopList);
                //output the new list
                Console.WriteLine("OUTPUT LIST ====================");
                //save the new list.
                string path = @"C:\Users\BardzyBear\Desktop\Merge\Merge.txt";
                if (!File.Exists(path))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        List<string> outputStringList = new List<string>();
                        foreach (Container outContainer in outputList)
                        {
                            Console.WriteLine(outContainer.ToString());
                            outputStringList = outContainer.Save();
                            foreach (string s in outputStringList)
                            {
                                sw.WriteLine(s);
                            }
                        }

                        Console.WriteLine("Data Written to file.");
                        Console.ReadLine();
                    }
                }





            }
            else
            {
                //Convert








                do
                {
                    oldloc = "";
                    input = "";
                    Console.WriteLine("Enter Location of old file: ");
                    oldloc = Console.ReadLine();
                    Console.WriteLine("Confirm (y/n)");
                    input = Console.ReadLine();

                    if (input != "n" || input != "y")
                    {
                        //error
                        //return
                    }
                    else if (input == "y")
                    {
                        break;
                    }

                } while (input != "y");

                Container general = new Container("General");
                Container favourites = new Container("Favourites");
                Box lastBox = new Box("");
                string[] entityStr;
                //try to open old file
                String line;
                try
                {
                    //Pass the file path and file name to the StreamReader constructor
                    StreamReader sr = new StreamReader(oldloc);

                    //Read the first line of text
                    line = sr.ReadLine();
                    int i = 0;
                    //Continue to read until you reach end of file
                    while (line != "/END OF FILE")
                    {
                        if (line.Contains(":"))
                        {
                            entityStr = line.Split(':');
                            entityStr[0].Trim();
                            entityStr[1] = entityStr[1].Substring(1);
                            lastBox.entityList.Add(new Entity(entityStr[0], entityStr[1]));
                        }
                        else if (line.Contains("|"))
                        {
                            if (line.Contains("/*|"))
                            {
                                line = line.Substring(0, line.Length - 3);
                                lastBox = new Box(line);
                                favourites.boxList.Add(lastBox);
                            }
                            else
                            {
                                line = line.Substring(0, line.Length - 1);
                                lastBox = new Box(line);
                                general.boxList.Add(lastBox);
                            }
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

                Console.WriteLine("\n\n\n\n");
                Console.WriteLine(general.ToString());
                Console.WriteLine("\n\n\n===========================================\n\n\n");
                Console.WriteLine(favourites.ToString());
                Console.ReadLine();

                string path = @"C:\Users\BardzyBear\Desktop\newData.txt";
                if (!File.Exists(path))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        List<string> favList = new List<string>();
                        favList = favourites.Save();
                        foreach (string s in favList)
                        {
                            sw.WriteLine(s);
                        }

                        List<string> genList = new List<string>();
                        genList = general.Save();
                        foreach (string s in genList)
                        {
                            sw.WriteLine(s);
                        }

                        Console.WriteLine("Data Written to file.");
                        Console.ReadLine();
                    }
                }

            }
        }
    }
}
