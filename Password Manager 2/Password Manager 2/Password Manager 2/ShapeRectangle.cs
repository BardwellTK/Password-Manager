using System.Drawing;

namespace Password_Manager_2
{
    public class ShapeRectangle
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Brush BrushColor { get; set; }
        private Rectangle _rectangle;

        public ShapeRectangle(int x,int y,int width, int height, Brush brushColor)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            BrushColor = brushColor;
            _rectangle = new Rectangle(X, Y, Width, Height);
        }

        public virtual void Draw(Graphics g)
        {
            g.FillRectangle(BrushColor,_rectangle);
        }
    }
    
}
