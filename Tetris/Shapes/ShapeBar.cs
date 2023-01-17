namespace Tetris.Shapes
{
    public class ShapeBar : BaseShape
    {
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
