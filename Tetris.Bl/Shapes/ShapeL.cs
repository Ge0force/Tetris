using System.Drawing;

namespace Tetris.BL.Shapes
{
    public class ShapeL : BaseShape
    {
        public ShapeL() : base(new Point(4, 0), 0, 4, new Point(1, 1), Color.FromArgb(255, 0, 40, 255), Color.FromArgb(100, 0, 40, 255))
        {
            Pattern.Add(new Point(0, 1));
            Pattern.Add(new Point(0, 2));
            Pattern.Add(new Point(1, 1));
            Pattern.Add(new Point(2, 1));
        }
    }

}
