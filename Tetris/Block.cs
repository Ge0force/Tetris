using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Block
    {
        public int Xpos { get; set; }   
        public int Ypos { get; set; }
        public int RotationStep { get; set; }
        public int MaxRotationSteps { get; set; }

        public bool[,] Pattern;

        public Color penColor = Color.FromArgb(255, 155, 38, 182);
        public Color brushColor = Color.FromArgb(100, 155, 38, 182);

        public Block() // Needs parameters
        {
            Xpos = 0;
            Ypos = 4;  // This should be pattern.width / 2
            RotationStep = 0;
            
            // For now, block is always a T
            MaxRotationSteps = 4;

            Pattern = new bool[3, 3] { { false, true, false },
                                       { true, true, false },
                                       { false, true, false } };

            //Rotate();
        }
        public void Rotate() // This should be in gameloop with ref parameter pattern
        {
            bool[,] newPattern = new bool[3, 3];
            int z = 3;

            for (int y = 0; y < 3; y++)
            {
                z--;
                
                for (int x = 0; x < 3; x++)
                {
                    newPattern[z, x] = Pattern[x, y];
                }
            }

            Pattern = newPattern;
        }


    }
}
