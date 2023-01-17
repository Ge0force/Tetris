namespace Tetris.Shapes
{
    public class ShapeReverseL : BaseShape
    {
        public ShapeReverseL()
        {
            Position = new Point(4, 0);

            Rotation = 0;
            MaxRotation = 4;
            PenColor = Color.FromArgb(255, 139, 69, 19);
            BrushColor = Color.FromArgb(100, 139, 69, 19);

            Pattern = new List<Point>();
            Pattern.Add(new Point(0, 1));
            Pattern.Add(new Point(0, 2));
            Pattern.Add(new Point(1, 2));
            Pattern.Add(new Point(2, 2));

            Pivot = new Point(1, 1);
        }
    }

}
