namespace Tetris
{
    public class ShapeBar : IShape
    {
        public Point Position { get; set; }
        public int Rotation { get; set; }
        public int MaxRotation { get; set; }
        public List<Point> Pattern { get; set; }
        public Point Pivot { get; set; }
        public Color PenColor { get; set; }
        public Color BrushColor { get; set; }

        public ShapeBar()
        {
            Position = new Point(4, -1);

            Rotation = 0;
            MaxRotation = 2;
            PenColor = Color.FromArgb(255, 255, 255, 0);
            BrushColor = Color.FromArgb(100, 255, 255, 0);

            Pattern = new List<Point>();
            Pattern.Add(new Point(0, 2));
            Pattern.Add(new Point(1, 2));
            Pattern.Add(new Point(2, 2));
            Pattern.Add(new Point(3, 2));

            Pivot = new Point(2, 2);
        }
    }
}
