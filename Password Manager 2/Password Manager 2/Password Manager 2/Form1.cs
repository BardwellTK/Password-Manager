using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Deployment.Internal;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Password_Manager_2
{
    public partial class HomeWindow : Form
    {
        public HomeWindow()
        {
            InitializeComponent();
            InitializeForm();
        }



        public void InitializeForm()
        {
            //Set form size
            Width = 400;
            Height = 550;
            //Create GUI
            InitializeGUI();
        }


        private List<MyButton> _buttons;
        
        public void InitializeGUI()
        {
            label_FPS.Location = new Point(0,0);

            //Set GUI size && GUI position
            GUI.Size = Size;
            GUI.Location = new Point(0, 0);
            //Create new bitmap for GUI
            Bitmap bmpGUI = new Bitmap(Width, Height);
            //Set background colour of bitmap
            using (Graphics graphics = Graphics.FromImage(bmpGUI))
            {
                graphics.Clear(Color.Crimson);
            }
            //place bitmap in GUI
            GUI.Image = bmpGUI;


            timerGUI.Enabled = true;
            timerSecond.Enabled = true;

            _buttons = new List<MyButton>();
            _buttons.Add(new MyButton(Width/2,Height/2,120,40));
            _buttons[0].Text = "Hello";
        }

        private void Processing()
        {
            Point buttonPoint = new Point(0,0);
            Point mousePoint = Cursor.Position;
            foreach (MyButton b in _buttons)
            {
                if (b.Visible) //other buttons may be in same position and not visible
                {
                    b.MouseClick(Cursor.Position);

                    //Mouse detection
                    buttonPoint = b.Location;

                    //mouseX must be greater than buttonX min; less than buttonX max
                    //mouseY must be greater than buttonY min; less than buttonY max
                    if (mousePoint.X >= buttonPoint.X && mousePoint.X <= buttonPoint.X + b.Width)
                    {
                        if (mousePoint.Y >= buttonPoint.Y && mousePoint.Y <= buttonPoint.Y + b.Height)
                        {
                            
                        }
                    }
                }
            }
        }

        private void HomeWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                Application.Exit();
            }
        }

        private void timerGUI_Tick(object sender, EventArgs e)
        {
            //Run GUI.Invalidate
            Draw();
        }

        private int currentFrameCount;
        private int currentFPS;
        private const int fps = 25;
        private void Draw()
        {
            try
            {
                if (currentFrameCount < fps)
                {
                    using (Graphics g = Graphics.FromImage(GUI.Image))
                    {
                        g.Clear(Color.Crimson);
                        //Draw all objects in current panel
                        //Draw all buttons
                        foreach (MyButton b in _buttons)
                        {
                            b.Draw(g);
                        }

                        //increment fps counter
                        currentFrameCount++;
                        label_FPS.Text = currentFPS.ToString();
                        //refresh the screen
                        GUI.Invalidate();
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }

        private void timerSecond_Tick(object sender, EventArgs e)
        {
            //set current fps each second
            currentFPS = currentFrameCount;
            //reset current frame count
            currentFrameCount = 0;
        }

        private void HomeWindow_MouseClick(object sender, MouseEventArgs e)
        {
            //call the MouseClick method of the currently hovered control
        }

        private void HomeWindow_MouseMove(object sender, MouseEventArgs e)
        {
            //for each control where control.visible
            //if control.contains(mouse)
            //
        }
    }
}
