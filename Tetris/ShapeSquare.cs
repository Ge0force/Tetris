namespace Tetris
{
    public class ShapeSquare : IShape
    {
        public Point Position { get; set; }
        public int Rotation { get; set; }
        public int MaxRotation { get; set; }
        public List<Point> Pattern { get; set; }
        public Point Pivot { get; set; }
        public Color PenColor { get; set; }
        public Color BrushColor { get; set; }

        public ShapeSquare()
        {
            Position = new Point(5, 1);

            Rotation = 0;
            MaxRotation = 0;
            PenColor = Color.FromArgb(255, 255, 165, 0);
            BrushColor = Color.FromArgb(100, 255, 165, 0);

            Pattern = new List<Point>();
            Pattern.Add(new Point(0, 0));
            Pattern.Add(new Point(0, 1));
            Pattern.Add(new Point(1, 0));
            Pattern.Add(new Point(1, 1));

            Pivot = new Point(0, 0);
        }
    }

}
