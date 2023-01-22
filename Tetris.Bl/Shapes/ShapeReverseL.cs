using System.Drawing;

namespace Tetris.BL.Shapes
{
    public class ShapeReverseL : BaseShape
    {
        public ShapeReverseL() : base(new Point(4, 0), 0, 4, new Point(1, 1), Color.FromArgb(255, 139, 69, 19), Color.FromArgb(100, 139, 69, 19))
        {
            Pattern.Add(new Point(0, 1));
            Pattern.Add(new Point(0, 2));
            Pattern.Add(new Point(1, 2));
            Pattern.Add(new Point(2, 2));
        }
    }

}
