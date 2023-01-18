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

        // Drop the falling shape
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
                // Restore shape on original position
                _playfield.FallingShape.Position.Y -= 1;
                graphicsService.DrawShape(_playfield.FallingShape);

                // Check for full line

                                
                // Create new random shape
                _playfield.SpawnRandomShape();

                // Draw the falling shape
                graphicsService.DrawShape(_playfield.FallingShape);

                // Check for gameover


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
