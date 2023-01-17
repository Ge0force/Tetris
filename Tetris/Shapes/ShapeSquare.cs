namespace Tetris.Shapes
{
    public class ShapeSquare : BaseShape
    {
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
