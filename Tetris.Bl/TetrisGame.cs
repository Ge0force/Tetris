using System.Timers;

namespace Tetris.BL
{
    public class TetrisGame
    {
        public bool Running { get; private set; }

        private System.Timers.Timer _gameTimer;

        private IGraphicsService _graphicsService;

        private PlayField _playfield;

        public const int _playfieldWidth = 10;
        public const int _playfieldHeight = 18;

        private int _gameSpeed;

        public TetrisGame(IGraphicsService gs)
        {
            // Class that provides the graphics
            _graphicsService = gs;

            // Create playfield
            _playfield = new PlayField(_playfieldWidth, _playfieldHeight);

            // Initalize game timer
            _gameTimer = new System.Timers.Timer();
            _gameTimer.Elapsed += new ElapsedEventHandler(GameTimerTick);
        }

        // Start new game
        public void Start()
        {
            // Clear and draw playfield on the form
            _graphicsService.DrawPlayfield(_playfieldWidth, _playfieldHeight);

            // Draw the falling shape
            _graphicsService.DrawShape(_playfield.FallingShape);

            // Set gameloop state
            Running = true;

            // Reset gamespeed:
            _gameSpeed= 1000;

            // Start timer
            _gameTimer.Interval = _gameSpeed;
            _gameTimer.Start();
        }

        // Drop the falling shape each timer tick
        public void GameTimerTick(object? source, ElapsedEventArgs e)
        {
            DropFallingShape();
        }

        // Drop the falling shape and check for collisions
        private void DropFallingShape()
        {
            // Erase the shape 
            _graphicsService.EraseShape(_playfield.FallingShape);

            // Drop the shape
            _playfield.FallingShape.MoveDown();

            // Check for collisions with other shapes and the playfield borders
            if (_playfield.NoCollisions())
            {
                _graphicsService.DrawShape(_playfield.FallingShape);
            }
            else
            {
                _gameTimer.Stop();

                // Restore shape on original position
                _playfield.FallingShape.MoveUp();
                _graphicsService.DrawShape(_playfield.FallingShape);

                // When the falling shape is stuck at the top line -> Game Over!
                if(_playfield.FallingShape.TopMargin() <= 1)
                {
                    Running = false;
                    _graphicsService.DisplayGameOver();
                }
                else
                {
                    // Check for full lines
                    List<int> fullLines = _playfield.CheckFullLines();

                    // Instead removing the affected lines with animation, just redraw all shapes when lines are removed
                    if (fullLines.Count > 0)
                    { 
                        _graphicsService.RedrawShapes(_playfield.Field, _playfieldWidth, _playfieldHeight);
                    }

                    // Create new random shape
                    _playfield.SpawnRandomShape();
                    _graphicsService.DrawShape(_playfield.FallingShape);

                    _gameTimer.Start();
                }

            }

        }

        // Keyboard input
        public void KeyLeft()
        {
            _graphicsService.EraseShape(_playfield.FallingShape);
            _playfield.FallingShape.MoveLeft();
            if (!_playfield.NoCollisions()) { _playfield.FallingShape.MoveRight(); }
            _graphicsService.DrawShape(_playfield.FallingShape);
        }

        public void KeyRight()
        {
            _graphicsService.EraseShape(_playfield.FallingShape);
            _playfield.FallingShape.MoveRight();
            if (!_playfield.NoCollisions()) { _playfield.FallingShape.MoveLeft(); }
            _graphicsService.DrawShape(_playfield.FallingShape);
        }

        public void KeyDrop()
        {
            // Drop the falling shape immediately
            // Falling shape is redrawn in the DropFallingShape method!
            DropFallingShape();

            // Reset _gameTimer
            _gameTimer.Stop();
            _gameTimer.Start();
        }

        public void KeyRotate()
        {
            _graphicsService.EraseShape(_playfield.FallingShape);
            _playfield.FallingShape.Rotate();
            if (!_playfield.NoCollisions()) { _playfield.FallingShape.Rotate(true); }
            _graphicsService.DrawShape(_playfield.FallingShape);
        }
       
    }
}
