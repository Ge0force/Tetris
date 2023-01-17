﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class ShapeT: IShape
    {
        public Point Position { get; set; }
        public int Rotation { get; set; }
        public int MaxRotation { get; set; }
        public List<Point> Pattern { get; set; }
        public Point Pivot { get; set; }
        public Color PenColor { get; set; }
        public Color BrushColor { get; set; }

        public ShapeT()
        {
            Position = new Point(4, 1);

            Rotation= 0;
            MaxRotation= 4;
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
