namespace Tetris
{
    public class ShapeStairsUp : IShape
    {
        public Point Position { get; set; }
        public int Rotation { get; set; }
        public int MaxRotation { get; set; }
        public List<Point> Pattern { get; set; }
        public Point Pivot { get; set; }
        public Color PenColor { get; set; }
        public Color BrushColor { get; set; }

        public ShapeStairsUp()
        {
            Position = new Point(4, 0);

            Rotation = 0;
            MaxRotation = 2;
            PenColor = Color.FromArgb(255, 0, 255, 255);
            BrushColor = Color.FromArgb(100, 0, 255, 255);

            Pattern = new List<Point>();
            Pattern.Add(new Point(0, 2));
            Pattern.Add(new Point(1, 1));
            Pattern.Add(new Point(1, 2));
            Pattern.Add(new Point(2, 1));

            Pivot = new Point(1, 1);
        }
    }

}
