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

    }
}
