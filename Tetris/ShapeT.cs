using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class ShapeT: Block
    {
        public ShapeT() 
        {
            Pattern = new bool[3, 3] { { false, false, false },
                                       { false, true, false },
                                       { true, true, true }};

            
        }
    }
}
