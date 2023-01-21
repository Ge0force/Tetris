namespace Tetris.Shapes
{
    public class ShapeSquare : BaseShape
    {
        public ShapeSquare() : base(new Point(5, 1), 0, 0, new Point(0, 0), Color.FromArgb(255, 255, 165, 0), Color.FromArgb(100, 255, 165, 0))
        {
            Pattern.Add(new Point(0, 0));
            Pattern.Add(new Point(0, 1));
            Pattern.Add(new Point(1, 0));
            Pattern.Add(new Point(1, 1));
        }
    }

}
