using Tetris.Shapes;

namespace Tetris
{
    public class PlayField
    {
        public List<IShape> Field { get; set; }

        public IShape FallingShape { get; set; }

        public int FieldWidth { get; set; }
        public int FieldHeight { get; set; }

        private int _lastRandomNumber = 0;

        public PlayField(int width, int height)
        {
            Field = new List<IShape>();

            FieldWidth = width;
            FieldHeight = height;

            SpawnRandomShape();
        }
        public bool CheckCollissons()
        {
            // Check margins
            if (FallingShape.LeftMargin() <= 0 ) { return false; }
            if (FallingShape.RightMargin() > FieldWidth) { return false; }
            if (FallingShape.BottomMargin() >= FieldHeight) { return false; }

            // Check collision with other fallingShapes on the playfield:
            foreach (IShape s in Field)
            {
                // Ignore falling fallingShape
                if (s != FallingShape)
                {

                    foreach (Point pf in s.Pattern)
                    {
                        foreach (Point ps in FallingShape.Pattern)
                        {
                            if ((s.Position.X + pf.X) == (FallingShape.Position.X + ps.X))
                            {
                                if ((s.Position.Y + pf.Y) == (FallingShape.Position.Y + ps.Y))
                                    return false;
                            }
                        }
                    }

                }
            }

            return true;
        }

        public void SpawnRandomShape()
        {
            // Roll random number between 0 and 7
            Random rd = new Random();
            int rand_num = rd.Next(0, 7);

            // Reroll if result == 7 or result == previous result
            if (rand_num == 7 || rand_num == _lastRandomNumber)
            {
                rand_num = rd.Next(0, 6);
            }

            _lastRandomNumber = rand_num;

            // Result of roll determines which shape
            switch (rand_num)
            {
                case 0: FallingShape = new ShapeBar(); break;
                case 1: FallingShape = new ShapeL(); break;
                case 2: FallingShape = new ShapeReverseL(); break;
                case 3: FallingShape = new ShapeStairsUp(); break;
                case 4: FallingShape = new ShapeStairsDown(); break;
                case 5: FallingShape = new ShapeSquare(); break;
                default: FallingShape = new ShapeT(); break;
            }

            Field.Add(FallingShape);
        }

        public List<int> CheckFullLines()
        {
            var fullLines = new List<int>();

            // Check lines from top to bottom            
            //for (int i = (FieldHeight - 1); i > 1; i--)
            for (int i = 0; i < FieldHeight; i++)
            {
                // Count the number of points at line i
                int counter = 0;

                foreach (IShape s in Field)
                {
                    foreach (Point p in s.Pattern)
                    {
                        if (p.Y + s.Position.Y == i) counter++;
                    }
                }

                // Line is full if points = fieldwidth
                if (counter == FieldWidth)
                {
                    // Add to list for graphics animation
                    fullLines.Add(i);

                    // We can't remove object in a foreach loop, so we'll save that for later
                    List<Point> pointsToRemove = new List<Point>();
                    List<IShape> shapesToRemove = new List<IShape>();

                    foreach (IShape s in Field)
                    {
                        foreach (Point p in s.Pattern)
                        {
                            // Remove points at that line
                            if (p.Y + s.Position.Y == i) pointsToRemove.Add(p);

                            // Remove shape if no points left in pattern
                            if (s.Pattern.Count == 0) shapesToRemove.Add(s);

                            // All lines above the removed line should drop
                            if (p.Y + s.Position.Y < i) p.Y++;
                        }

                        // Remove points
                        foreach (Point p in pointsToRemove)
                        {
                            s.Pattern.Remove(p);
                        }
                    }

                    // Remove shapes
                    foreach(IShape s in shapesToRemove)
                    {
                        Field.Remove(s);
                    }

                }
            }

            return fullLines;
        }

    }
}
