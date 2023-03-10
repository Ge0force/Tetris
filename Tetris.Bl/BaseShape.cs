using System.Drawing;

namespace Tetris.BL
{
    public abstract class BaseShape : IShape
    {
        public Point Position { get; set; }
        public int Rotation { get; set; }
        public int MaxRotation { get; set; }
        public Pattern Pattern { get; set; } = new Pattern();
        public Point Pivot { get; set; }
        public Color PenColor { get; set; }
        public Color BrushColor { get; set; }

        public int LeftMargin() => Position.X + Pattern.XMin;
        public int RightMargin() => Position.X + Pattern.XMax;
        public int TopMargin() => Position.Y + Pattern.YMin;
        public int BottomMargin() => Position.Y + Pattern.YMax;

        // Protected constructor only allows inherritance, no instances
        protected BaseShape(Point position, int rotation, int maxRotation, Point pivot, Color penColor, Color brushColor)
        {
            Position = position;
            Rotation = rotation;
            MaxRotation = maxRotation;
            Pivot = pivot;
            PenColor = penColor;
            BrushColor = brushColor;
        }

        public void Rotate(bool backwards = false)
        {
            // Square has no rotation
            if (MaxRotation > 1)
            {
                // determine rotation direction
                int angle;

                if (!backwards)
                { Rotation++; angle = 90; }
                else
                { Rotation--; angle = -90; }

                // Reset rotation when < 0 or > MaxRotation
                if (Rotation >= MaxRotation) 
                    Rotation = 0;

                if (Rotation < 0) 
                    Rotation = MaxRotation;

                // Some shapes can only be rotated once and will be rotated back to the previous position next
                if (MaxRotation == 1 && Rotation == 0)
                    angle = -angle;

                //Pattern = Utilities.RotatePattern(Pattern, Pivot, angle);
                Pattern.RotatePattern(Pivot, angle);

            }
        }

        public void MoveUp() => Position.Y--;
        public void MoveDown() => Position.Y++;
        public void MoveLeft() => Position.X--;
        public void MoveRight() => Position.X++;

    }

}
