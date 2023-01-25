namespace Tetris.BL
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }  

        public Point(int x, int y) 
        {
            X = x;
            Y = y;  
        }

        public void RotatePoint(Point pivot, double angleDegree)
        {
            double angle = angleDegree * Math.PI / 180;
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            int dx = X - pivot.X;
            int dy = Y - pivot.Y;
            double x = cos * dx - sin * dy + pivot.X;
            double y = sin * dx + cos * dy + pivot.X;

            X = (int)Math.Round(x);
            Y = (int)Math.Round(y);
        }

    }

}
