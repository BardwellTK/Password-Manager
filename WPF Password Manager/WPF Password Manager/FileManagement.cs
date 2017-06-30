using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Password_Manager.DataTypes;

namespace WPF_Password_Manager
{
    public partial class MainWindow : Window
    {
        //Field Init
        private static string curdir = Directory.GetCurrentDirectory();
        private static string settingsLoc = curdir + @"\settings.txt";
        private static string saveLoc;

        private void LoadFile()
        {
            try
            {
                if (File.Exists(settingsLoc))
                {
                    //if yes, read data.txt loc and make saveLoc = (string)data.txt
                    using (StreamReader reader = new StreamReader(@settingsLoc))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains("saveLoc ~"))
                            {
                                int a = line.IndexOf("\"") + 1;
                                int b = line.IndexOf("\"", a);
                                saveLoc = line.Substring(a, b - a);
                            }
                        }
                    }
                    if (saveLoc == null)
                    {
                        CreateSettings();
                    }
                }
                else
                {
                    CreateSettings();
                }
                if (File.Exists(saveLoc)) //check just in case error occurred
                {
                    using (StreamReader reader = new StreamReader(@saveLoc))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            LoadLine(line);
                        }
                    }
                }
                else
                {
                    //create data file
                    File.Create(saveLoc);

                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"settingsLoc: {settingsLoc} \nsaveLoc: {saveLoc}");
                Console.WriteLine(exception.ToString());
                //TODO
            }
        }

        private void CreateSettings()
        {
            //creating writing mechanism
            saveLoc = curdir + @"\data.txt";
            using (StreamWriter writer = new StreamWriter(@settingsLoc))
            {
                //set saveLoc
                //write data location
                writer.WriteLine(saveLoc);
            }
        }
        
        private void LoadLine(string line)
        {
            /* Container ~ Title = "mytitle"
             * Box ~ Title = "mytitle"
             * Entity ~ Title = "entitle" Data = "mydata"
             * Entity ~ Title = "othertitle" Data = "neotherdata"
             * Box ~ Title = "otherbox"
             * Entity ~ Title = "otherboxentity" Data = "otherboxdata"
             * Entity ~ Title = "yep" Data = "yeep"
             * Container ~ Title = "othercontainer"
             * 
             * 
             */
            if (line.Contains("Container ~"))
            {
                containers.Add(new Container(containers.Count,TitleCheck(line)));
            }
            else if(line.Contains("Box ~"))
            {
                containers.IntoPerspective(containers.Count - 1);
                SelectedContainer = containers.Perspective;
                SelectedContainer.Add(new Container(SelectedContainer.Count, TitleCheck(line)));
            }
            else if(line.Contains("Entity ~"))
            {
                containers.Perspective.IntoPerspective(containers.Perspective.Count - 1);
                if (containers.Perspective.Perspective != null)
                {
                    SelectedContainer = containers.Perspective.Perspective;
                    SelectedContainer.Add(new Container(SelectedContainer.Count, TitleCheck(line), DataCheck(line)));
                    
                }
            }

        }

        private string TitleCheck(string data)
        {
            // Container ~ Title = "mytitle"
            int start = data.IndexOf("\"") + 1 ;
            int end = data.IndexOf("\"", start);
            //difference of start and end - 1
            string output = data.Substring(start, end - start);
            return output;
        }

        private string DataCheck(string data)
        {
            try
            {
                int start = data.IndexOf("\"", data.IndexOf("Data =")) + 1;
                int end = data.IndexOf("\"", start + 1);
                string output = data.Substring(start, end - start);
                return output;
            } catch (ArgumentOutOfRangeException e)
            {
                return "";
            }
        }

        private void Save()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(saveLoc))
                {
                    var list1 = containers.GetList();
                    foreach (var item1 in list1)
                    {
                        writer.WriteLine($"Container ~ Title = \"{item1.Title}\"");
                        var list2 = item1.GetList();
                        foreach (var item2 in list2)
                        {
                            writer.WriteLine($"Box ~ Title = \"{item2.Title}\"");
                            var list3 = item2.GetList();
                            foreach (var item3 in list3)
                            {
                                writer.WriteLine($"Entity ~ Title = \"{item3.Title}\" Data = \"{item3.Data}\"");
                            }
                        }
                    }
                }
                    
            }
            catch (Exception e)
            {
                Console.WriteLine("here");
                Console.WriteLine(e.ToString());
            }
        }

        private void SaveCheck()
        {
            //If the task has been completed, reset the task
            //saveFile is empty after completion
            if (saveFile.Status == TaskStatus.RanToCompletion)
            {
                saveFile = new Task(Save);
            }

            if (saveFile.Status != TaskStatus.Running)
            {
                saveFile.Start();
            }
        }
    }
}
