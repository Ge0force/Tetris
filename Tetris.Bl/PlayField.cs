using Tetris.BL.Shapes;

namespace Tetris.BL
{
    public class PlayField
    {
        public List<IShape> Field { get; set; }
        public IShape FallingShape { get; set; }
        public IShape NextShape { get; set; }
        private int _fieldWidth { get; set; }
        private int _fieldHeight { get; set; }

        private int _lastRandomNumber = 0;

        private Random _rd = new Random();      // Important to re-use the same "random" object

        public PlayField(int width, int height)
        {
            Field = new List<IShape>();

            _fieldWidth = width;
            _fieldHeight = height;

            NextShape = CreateRandomShape();
            FallingShape = CreateRandomShape();
            Field.Add(FallingShape);
        }
        public bool NoCollisions()
        {
            // Check margins
            if (FallingShape.LeftMargin() <= 0 ) { return false; }
            if (FallingShape.RightMargin() > _fieldWidth) { return false; }
            if (FallingShape.BottomMargin() >= _fieldHeight) { return false; }

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
            FallingShape = NextShape;
            Field.Add(FallingShape);
            NextShape = CreateRandomShape();
        }
        private IShape CreateRandomShape()
        {
            // Roll random number between 0 and 7
            int rand_num = _rd.Next(0, 7);

            // Reroll if result == 7 or result == previous result
            if (rand_num == 7 || rand_num == _lastRandomNumber)
            {
                rand_num = _rd.Next(0, 6);
            }

            _lastRandomNumber = rand_num;

            // Result of roll determines which shape
            switch (rand_num)
            {
                case 0: return new ShapeBar(); 
                case 1: return new ShapeL(); 
                case 2: return new ShapeReverseL(); 
                case 3: return new ShapeStairsUp();
                case 4: return new ShapeStairsDown(); 
                case 5: return new ShapeSquare(); 
                default: return new ShapeT(); 
            }
        }


        public List<int> CheckFullLines()
        {
            var fullLines = new List<int>();

            // Check lines from top to bottom            
            //for (int i = (_fieldHeight - 1); i > 1; i--)
            for (int i = 0; i < _fieldHeight; i++)
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

                // Line is full if points = _fieldWidth
                if (counter == _fieldWidth)
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
