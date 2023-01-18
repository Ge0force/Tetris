namespace Tetris
{
    public interface IShape
    {
        Point Position { get; set; }        // Position on the playfield
        Point Pivot { get; set; }           // Pattern rotation point

        int Rotation { get; set; }          // Current rotation 
        int MaxRotation { get; set; }       // Times the shape can be rotated before resetting to original postion

        List<Point> Pattern { get; set; }  // Pattern of the shape containing Points

        Color PenColor { get; set; }        // Line color
        Color BrushColor { get; set; }      // Fill color

        void Rotate(bool backwards = false);

        int LeftMargin();
        int RightMargin();
        int BottomMargin();
    }
}
