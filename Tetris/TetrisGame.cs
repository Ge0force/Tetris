using System.Timers;
using Tetris.Shapes;

namespace Tetris
{
    public class TetrisGame
    {
        System.Timers.Timer gameTimer;

        public const int _playfieldWidth = 10;
        public const int _playfieldHeight = 18;

        public bool Running { get; private set; }

        private int _gameSpeed = 1000;

        private PlayField _playfield;

        GraphicsService graphicsService;

        public TetrisGame(GraphicsService gs)
        {
            graphicsService = gs;
        }

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

        public void gameTimerTick(object source, ElapsedEventArgs e)
        {
            DropFallingShape();
        }

        public void KeyDown(KeyEventArgs e)
        {
            //if (_fallingShape != null)
            //{

            //    graphicsService.EraseShape(_fallingShape);

            //    if (e.KeyCode == Keys.Left)
            //    {
            //        _fallingShape.Position.X -= 1;
            //        if (!ShapeCanMove(_fallingShape)) { _fallingShape.Position.X += 1; }

            //    }
            //    else if (e.KeyCode == Keys.Right)
            //    {
            //        _fallingShape.Position.X += 1;
            //        if (!ShapeCanMove(_fallingShape)) { _fallingShape.Position.X -= 1; }
            //    }
            //    else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Space)
            //    {
            //        _fallingShape.Rotate();
            //        if (!ShapeCanMove(_fallingShape)) { _fallingShape.Rotate(true); }
            //    }
            //    else if (e.KeyCode == Keys.Down)
            //    {
            //        // Fasten gamespeed
            //        _curSpeed = _gameSpeed / 15;
            //    }

            //    graphicsService.DrawShape(_fallingShape);
            //}
        }

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
            }

        }

    }
}
