namespace Tetris.Shapes
{
    public class ShapeStairsDown : BaseShape
    {
        public ShapeStairsDown()
        {
            Position = new Point(4, 0);

            Rotation = 0;
            MaxRotation = 2;
            PenColor = Color.FromArgb(255, 0, 250, 154);
            BrushColor = Color.FromArgb(100, 0, 250, 154);

            Pattern = new List<Point>();
            Pattern.Add(new Point(0, 1));
            Pattern.Add(new Point(1, 1));
            Pattern.Add(new Point(1, 2));
            Pattern.Add(new Point(2, 2));

            Pivot = new Point(1, 1);
        }
    }

}
