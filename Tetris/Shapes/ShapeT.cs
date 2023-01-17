using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Shapes
{
    public class ShapeT : BaseShape
    {
        public ShapeT()
        {
            Position = new Point(4, 1);

            Rotation = 0;
            MaxRotation = 4;
            PenColor = Color.FromArgb(255, 155, 38, 182);
            BrushColor = Color.FromArgb(100, 155, 38, 182);

            Pattern = new List<Point>();
            Pattern.Add(new Point(0, 1));
            Pattern.Add(new Point(1, 0));
            Pattern.Add(new Point(1, 1));
            Pattern.Add(new Point(2, 1));

            Pivot = new Point(1, 1);
        }
    }

}
