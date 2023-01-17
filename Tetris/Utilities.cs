using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Tetris
{
    public static class Utilities
    {
        public static List<Point> RotatePattern(List<Point> pattern, Point pivot, int angle)
        {
            var result = new List<Point>();

            foreach (Point p in pattern)
            {
                result.Add(Utilities.RotatePoint(p, pivot, angle));
            }
            return result;
        }

        public static Point RotatePoint(Point point, Point pivot, double angleDegree)
        {
            double angle = angleDegree * Math.PI / 180;
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            int dx = point.X - pivot.X;
            int dy = point.Y - pivot.Y;
            double x = cos * dx - sin * dy + pivot.X;
            double y = sin * dx + cos * dy + pivot.X;

            Point rotated = new Point((int)Math.Round(x), (int)Math.Round(y));
            return rotated;
        }

    }
}
