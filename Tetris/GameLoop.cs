namespace Tetris
{
    public class GameLoop
    {
        public const int _playfieldWidth = 10;
        public const int _playfieldHeight = 18;

        public bool Running { get; private set; }

        private int _gameSpeed = 1000;
        private int _curSpeed = 1000;

        private int _lastShape = 0;

        private IShape _fallingShape;

        private List<IShape> _playfield;

        GraphicsService graphicsService;

        public GameLoop(GraphicsService gs)
        {
            graphicsService = gs;
        }

        public async void Start()
        {
            // Clear and draw playfield
            graphicsService.DrawPlayfield(_playfieldWidth, _playfieldHeight);
            _playfield= new List<IShape>();

            // Set gameloop state
            Running = true;

            // Main gameplay loop
            while (Running)
            {
                // Create falling block object if it doesn't exist
                if (_fallingShape == null)
                {
                    var _newshape = RandomShape();
                    _playfield.Add(_newshape);
                    _fallingShape = _newshape;
                    //_fallingShape = RandomShape();
                    graphicsService.DrawShape(_fallingShape);
                }

                await Task.Delay(_curSpeed);

                DropShape(_fallingShape);
            }
        }

        public void KeyDown(KeyEventArgs e)
        {
            if (_fallingShape != null)
            {

                graphicsService.EraseShape(_fallingShape);

                if (e.KeyCode == Keys.Left)
                {
                    _fallingShape.Position.X -= 1;
                    if (!ShapeCanMove(_fallingShape)) { _fallingShape.Position.X += 1; }

                }
                else if (e.KeyCode == Keys.Right)
                {
                    _fallingShape.Position.X += 1;
                    if (!ShapeCanMove(_fallingShape)) { _fallingShape.Position.X -= 1; }
                }
                else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Space)
                {
                    Rotate(_fallingShape);
                    if (!ShapeCanMove(_fallingShape)) { Rotate(_fallingShape, true); }
                }
                else if (e.KeyCode == Keys.Down)
                {
                    // Fasten gamespeed
                    _curSpeed = _gameSpeed / 15;
                }

                graphicsService.DrawShape(_fallingShape);
            }
        }

        public void KeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                // Restore normal gamespeed
                _curSpeed = _gameSpeed;
            }
        }

        private void DropShape(IShape shape)
        {
            graphicsService.EraseShape(shape);
            shape.Position.Y += 1;

            // Check if shape can drop:
            if (ShapeCanMove(shape))
            {
                // Draw block on new position:
                graphicsService.DrawShape(shape);
            }
            else
            {
                // Restore shape on original position:
                shape.Position.Y -= 1;
                graphicsService.DrawShape(shape);
                
                // Check for full line


                // Check for game over


                // Reset falling shape
                _fallingShape = null;
            }

        }

        public bool ShapeCanMove(IShape shape) 
        {
            // Check left margin:
            int left = shape.Position.X + shape.Pattern.Min(point => point.X);
            if (left <= 0) { return false; }

            // Check right margin:
            int right  = shape.Position.X + shape.Pattern.Max(point => point.X);
            if (right > _playfieldWidth) { return false; }

            // Check bottom:
            int bottom = shape.Position.Y + shape.Pattern.Max(point => point.Y);
            if (bottom >= _playfieldHeight) { return false; }

            // Check collision with other shapes on the playfield:
            foreach(IShape s in _playfield)
            {
                // Ignore falling shape
                if (s != shape)
                { 
                
                    foreach(Point pf in s.Pattern)
                    {
                        foreach (Point ps in shape.Pattern) 
                        { 
                            if ((s.Position.X + pf.X) == (shape.Position.X + ps.X))
                            {
                                if ((s.Position.Y + pf.Y) == (shape.Position.Y + ps.Y))
                                    return false;
                            }
                        }
                    }

                }
            }

            return true;
        }

        public IShape RandomShape()
        {
            // Roll random number between 0 and 7
            Random rd = new Random();
            int rand_num = rd.Next(0, 7);

            // Reroll if result == 7 or result == previous result
            if(rand_num == 7 || rand_num == _lastShape)
            {
                rand_num = rd.Next(0, 6);
            }

            _lastShape= rand_num;

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

        public void Rotate(IShape shape, bool backwards = false)
        {
            // Square has no rotation
            if (shape.MaxRotation > 1)
            {
                int angle;

                if (!backwards)
                    { shape.Rotation++; angle = 90; }
                else
                    { shape.Rotation--; angle = -90; }

                if (shape.Rotation >= shape.MaxRotation)
                    shape.Rotation = 0;

                if (shape.Rotation < 0)
                    shape.Rotation = shape.MaxRotation;

                if (shape.MaxRotation > 2)
                {
                    shape.Pattern = RotateShape(shape, angle);
                }
                else
                // Shapes with only 2 rotation positions will be rotated forward once and then backwards
                {
                    if (shape.Rotation == 0)
                        shape.Pattern = RotateShape(shape, -angle);
                    else
                        shape.Pattern = RotateShape(shape, angle);
                }
            }
        }

        public List<Point> RotateShape(IShape shape, int angle)
        {
            var result = new List<Point>();

            foreach(Point p in shape.Pattern)
            {
                result.Add(RotatePoint(p, shape.Pivot, angle));
            }
            return result;
        }

        public Point RotatePoint(Point point, Point pivot, double angleDegree)
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
