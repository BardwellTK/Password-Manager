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
        private List<string> saveLocLines;
        private static string deviceName = Environment.MachineName;

        private void LoadFile()
        {
            try
            {
                saveLocLines = new List<string>();
                if (File.Exists(settingsLoc))
                {
                    //if yes, read data.txt loc and make saveLoc = (string)data.txt
                    using (StreamReader reader = new StreamReader(@settingsLoc))
                    {
                        string line, tempSaveLoc;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains("saveLoc ~") && line.Contains($"{deviceName}"))
                            {
                                int a = line.IndexOf("\"",line.IndexOf("Location = ")) + 1;
                                int b = line.IndexOf("\"", a);
                                tempSaveLoc = line.Substring(a, b - a);
                                //No point in adding line, better saveLoc, because
                                //string will have to be cut at the end anyway
                                saveLocLines.Add(line);
                                if (line.Contains(curdir))
                                {
                                    saveLoc = tempSaveLoc;
                                }
                            }
                        }
                        
                    }
                    
                }
                //More effiecient than an else and this.
                if (string.IsNullOrEmpty(saveLoc))
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

        /// <summary>
        /// CreateSettings: Called when
        ///                         - saveLoc does not exist
        ///                         - no line in file is related to current device
        /// </summary>
        private void CreateSettings()
        {//set saveLoc to default;
            saveLoc = curdir + @"\data.txt";
            //create file open mode
            FileMode fileMode = new FileMode();
            //is saveLocLines empty?
            if (saveLocLines == null || saveLocLines.Count == 0 || string.IsNullOrEmpty(saveLocLines[0]))
            {
                //Create/Overwrite file
                fileMode = FileMode.Create;
                
            }
            else //no?
            {
                //append to file.
                fileMode = FileMode.Append;
            }

            //Write to file...
            using (FileStream fs = File.Open(settingsLoc, fileMode))
            using (StreamWriter writer = new StreamWriter(fs))
            {
                //State saveLoc, then actual data and associated device.
                writer.WriteLine($"saveLoc ~ Location = \"{saveLoc}\" Device = \"{deviceName}\"");
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
                containers.Add(new Container(containers.Count,ExtractData(line,true)));
            }
            else if(line.Contains("Box ~"))
            {
                containers.IntoPerspective(containers.Count - 1);
                SelectedContainer = containers.Perspective;
                SelectedContainer.Add(new Container(SelectedContainer.Count, ExtractData(line, true)));
            }
            else if(line.Contains("Entity ~"))
            {
                containers.Perspective.IntoPerspective(containers.Perspective.Count - 1);
                if (containers.Perspective.Perspective != null)
                {
                    SelectedContainer = containers.Perspective.Perspective;
                    SelectedContainer.Add(new Container(SelectedContainer.Count, ExtractData(line, true), ExtractData(line, false)));
                    
                }
            }

        }

        private string ExtractData(string data, bool title)
        {
            // Container ~ Title = "mytitle"
            int start;
            if (title)
            {
                start = data.IndexOf("\"") + 1;
            }
            else
            {
                start = data.IndexOf("\"", data.IndexOf("Data =")) + 1;
            }
            int end = data.IndexOf("\"", start);
            //difference of start and end - 1
            string output = data.Substring(start, end - start);
            return output;
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
