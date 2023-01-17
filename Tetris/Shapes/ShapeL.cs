namespace Tetris.Shapes
{
    public class ShapeL : BaseShape
    {
        public ShapeL()
        {
            Position = new Point(4, 0);

            Rotation = 0;
            MaxRotation = 4;
            PenColor = Color.FromArgb(255, 0, 40, 255);
            BrushColor = Color.FromArgb(100, 0, 40, 255);

            Pattern = new List<Point>();
            Pattern.Add(new Point(0, 1));
            Pattern.Add(new Point(0, 2));
            Pattern.Add(new Point(1, 1));
            Pattern.Add(new Point(2, 1));

            Pivot = new Point(1, 1);
        }
    }

}
