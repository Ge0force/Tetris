namespace Tetris.Shapes
{
    public class ShapeBar : BaseShape
    {
        public ShapeBar(): base(new Point(4, -1), 0, 2, new Point(2, 2), Color.FromArgb(255, 255, 255, 0), Color.FromArgb(100, 255, 255, 0))
        {
            Pattern.Add(new Point(0, 2));
            Pattern.Add(new Point(1, 2));
            Pattern.Add(new Point(2, 2));
            Pattern.Add(new Point(3, 2));
        }
    }
}
