using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Shapes
{
    public class ShapeT : BaseShape
    {
        public ShapeT(): base(new Point(4, 1), 0, 4, new Point(1, 1), Color.FromArgb(255, 155, 38, 182), Color.FromArgb(100, 155, 38, 182))
        {
            Pattern.Add(new Point(0, 1));
            Pattern.Add(new Point(1, 0));
            Pattern.Add(new Point(1, 1));
            Pattern.Add(new Point(2, 1));
        }
    }

}
