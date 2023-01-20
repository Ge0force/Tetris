using System.Timers;

namespace Tetris
{
    public class TetrisGame
    {
        public bool Running { get; private set; }

        System.Timers.Timer gameTimer;

        GraphicsService graphicsService;

        private PlayField _playfield;

        public const int _playfieldWidth = 10;
        public const int _playfieldHeight = 18;

        private int _gameSpeed = 1000;

        public TetrisGame(GraphicsService gs)
        {
            // Class that provides the graphics
            graphicsService = gs;
        }

        // Start new game
        public void Start()
        {
            // Create playfield
            _playfield = new PlayField(_playfieldWidth, _playfieldHeight);
            
            // Clear and draw playfield on the form
            graphicsService.DrawPlayfield(_playfieldWidth, _playfieldHeight);

            // Draw the falling shape
            graphicsService.DrawShape(_playfield.FallingShape);

            // Set gameloop state
            Running = true;

            // Start timer
            gameTimer = new System.Timers.Timer();
            gameTimer.Elapsed += new ElapsedEventHandler(gameTimerTick);
            gameTimer.Interval = _gameSpeed;
            gameTimer.Start();
        }

        // Drop the falling shape each timer tick
        public void gameTimerTick(object source, ElapsedEventArgs e)
        {
            DropFallingShape();
        }

        // Drop the falling shape and check for collisions
        private void DropFallingShape()
        {
            // Erase the shape 
            graphicsService.EraseShape(_playfield.FallingShape);

            // Drop the shape
            _playfield.FallingShape.Position.Y += 1;

            // Check for collisions with other shapes and the playfield borders
            if (_playfield.CheckCollissons())
            {
                graphicsService.DrawShape(_playfield.FallingShape);
            }
            else
            {
                gameTimer.Stop();

                // Restore shape on original position
                _playfield.FallingShape.Position.Y -= 1;
                graphicsService.DrawShape(_playfield.FallingShape);

                // When the falling shape is stuck at the top line -> Game Over!
                if(_playfield.FallingShape.TopMargin() <= 1)
                {
                    Running = false;
                    graphicsService.DisplayGameOver();
                }
                else
                {
                    // Check for full lines
                    List<int> fullLines = _playfield.CheckFullLines();

                    // Instead removing the affected lines with animation, just redraw all shapes when lines are removed
                    if (fullLines.Count > 0)
                    { 
                        graphicsService.RedrawShapes(_playfield.Field, _playfieldWidth, _playfieldHeight);
                    }

                    // Create new random shape
                    _playfield.SpawnRandomShape();
                    graphicsService.DrawShape(_playfield.FallingShape);
                }

                gameTimer.Start();

            }

        }

        // Keyboard input
        public void KeyDown(KeyEventArgs e)
        {
            if (Running)
            {
                if (e.KeyCode == Keys.Down)
                {
                    // Drop the falling shape immediately
                    // Falling shape is redrawn in the DropFallingShape method!
                    DropFallingShape();

                    // Reset gametimer
                    gameTimer.Stop();
                    gameTimer.Start();
                }
                else
                {
                    // Erase the shape 
                    graphicsService.EraseShape(_playfield.FallingShape);

                    if (e.KeyCode == Keys.Left)
                    {
                        _playfield.FallingShape.Position.X -= 1;
                        if (!_playfield.CheckCollissons()) { _playfield.FallingShape.Position.X += 1; }

                    }
                    else if (e.KeyCode == Keys.Right)
                    {
                        _playfield.FallingShape.Position.X += 1;
                        if (!_playfield.CheckCollissons()) { _playfield.FallingShape.Position.X -= 1; }
                    }
                    else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Space)
                    {
                        _playfield.FallingShape.Rotate();
                        if (!_playfield.CheckCollissons()) { _playfield.FallingShape.Rotate(true); }
                    }

                    // Draw the falling shape
                    graphicsService.DrawShape(_playfield.FallingShape);
                }
            }
        }
    }
}
