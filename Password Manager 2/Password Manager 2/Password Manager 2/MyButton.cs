using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Windows.Forms;

namespace Password_Manager_2
{
    public class MyButton : MyControl
    {
        private ShapeRectangle normalRectangle, mouseRectangle, clickRectangle;
        private int state = 0;
        private string text;
        public MyButton(int x, int y, int width, int height) : base(x,y,width,height)
        {
            normalRectangle = new ShapeRectangle(x,y,width,height,Brushes.CornflowerBlue);
            mouseRectangle = new ShapeRectangle(x, y, width, height, Brushes.BlueViolet);
            clickRectangle = new ShapeRectangle(x, y, width, height, Brushes.DeepPink);
        }

        public void Draw(Graphics g)
        {
            switch (state)
            {
                case 0:
                    normalRectangle.Draw(g);
                    break;
                case 1:
                    mouseRectangle.Draw(g);
                    break;
                case 2:
                    clickRectangle.Draw(g);
                    break;
            }
            g.DrawString(text,new Font("Consalas", 12),Brushes.Black,Location);
        }

        public string Text { get { return text; } set { text = value; } }
        
    }

    public class MyControl
    {
        private Point location;
        private int _width, _height;
        private bool visible;
        public MyControl(int x, int y, int width, int height)
        {
            _width = width;
            _height = height;
            location = new Point(x, y);
        }

        public void MouseClick(Point mousePoint)
        {
            
        }

        public Point Location { get { return location; } }
        public int X { get { return location.X; } }
        public int Y { get { return location.Y; } }
        public int Width { get { return _width; } }
        public int Height { get { return _height; } }
        public bool Visible { get { return visible; } set { visible = value; } }
    }
}