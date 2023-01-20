namespace Tetris
{
    public abstract class BaseShape : IShape
    {
        public Point Position { get; set; }
        public int Rotation { get; set; }
        public int MaxRotation { get; set; }
        public List<Point> Pattern { get; set; }
        public Point Pivot { get; set; }
        public Color PenColor { get; set; }
        public Color BrushColor { get; set; }

        public int LeftMargin() => Position.X + Pattern.Min(point => point.X);
        public int RightMargin() => Position.X + Pattern.Max(point => point.X);
        public int TopMargin() => Position.Y + Pattern.Min(point => point.Y);
        public int BottomMargin() => Position.Y + Pattern.Max(point => point.Y);

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

                Pattern = Utilities.RotatePattern(Pattern, Pivot, angle);

            }
        }

    }

}
