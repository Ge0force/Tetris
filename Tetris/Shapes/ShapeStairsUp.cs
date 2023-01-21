namespace Tetris.Shapes
{
    public class ShapeStairsUp : BaseShape
    {
        public ShapeStairsUp() : base(new Point(4, 0), 0, 2, new Point(1, 1), Color.FromArgb(255, 0, 255, 255), Color.FromArgb(100, 0, 255, 255))
        {
            Pattern.Add(new Point(0, 2));
            Pattern.Add(new Point(1, 1));
            Pattern.Add(new Point(1, 2));
            Pattern.Add(new Point(2, 1));
        }
    }

}
