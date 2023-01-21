namespace Tetris.Shapes
{
    public class ShapeStairsDown : BaseShape
    {
        public ShapeStairsDown() : base(new Point(4, 0), 0, 2, new Point(1, 1), Color.FromArgb(255, 0, 250, 154), Color.FromArgb(100, 0, 250, 154))
        {
            Pattern.Add(new Point(0, 1));
            Pattern.Add(new Point(1, 1));
            Pattern.Add(new Point(1, 2));
            Pattern.Add(new Point(2, 2));
        }
    }

}
