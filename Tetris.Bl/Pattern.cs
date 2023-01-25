namespace Tetris.BL
{
    public class Pattern
    {
        private List<Point> Points { get; set; } = new List<Point>();
        
        public void Add(Point p)
        {
            Points.Add(p);
        }

        public void Remove(Point p)
        {
            Points.Remove(p);
        }

        public IEnumerator<Point> GetEnumerator()
        {
            return Points.GetEnumerator();
        }

        public int Count => Points.Count;

        public int XMin => Points.Min(point => point.X);

        public int XMax => Points.Max(point => point.X);

        public int YMin => Points.Min(point => point.Y);

        public int YMax => Points.Max(point => point.Y);

        public void RotatePattern(Point pivot, int angle)
        {
            foreach (Point p in Points)
            {
                p.RotatePoint(pivot, angle);
            }
        }

    }

}
