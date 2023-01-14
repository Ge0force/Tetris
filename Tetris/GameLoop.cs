namespace Tetris
{
    public class GameLoop
    {
        public bool Running { get; private set; }

        private int _counter = 0;

        private int _gameSpeed = 200;

        private Block _fallingBlock;

        GraphicsService graphicsService;

        public GameLoop(GraphicsService gs)
        {
            graphicsService = gs;
         }

        public async void Start()
        {
            // Clear and draw playfield
            graphicsService.DrawPlayfield();

            // Set gameloop state
            Running = true;

            // Main gameplay loop
            while (Running)
            {
                // Create falling block object if it doesn't exist
                if (_fallingBlock== null)
                {
                    _fallingBlock = new Block();
                    _fallingBlock.Xpos= 4;    // move to constructor; should depend on pattern width
                    _fallingBlock.Ypos = 1;

                    graphicsService.DrawBlock(_fallingBlock, false);
                    _counter= 0;    
                }

                //  Gameloop = 5 times the gamespeed to allow fast dropping
                _counter++;
                
                if (_counter % 5 == 0)
                {
                    // Erase block on old position:
                    graphicsService.DrawBlock(_fallingBlock, true);

                    _counter = 0;
                    _fallingBlock.Ypos += 1;

                    // Draw block on new position:
                    graphicsService.DrawBlock(_fallingBlock, false);
                }

                await Task.Delay(_gameSpeed);
            }
        }
        
        public void ReadKey(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                graphicsService.DrawBlock(_fallingBlock, true);
                _fallingBlock.Xpos -= 1;
                graphicsService.DrawBlock(_fallingBlock, false);
            }
            else if (e.KeyCode == Keys.Right)
            {
                graphicsService.DrawBlock(_fallingBlock, true);
                _fallingBlock.Xpos += 1;
                graphicsService.DrawBlock(_fallingBlock, false);
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Space)
            {
                graphicsService.DrawBlock(_fallingBlock, true);
                _fallingBlock.Rotate();
                graphicsService.DrawBlock(_fallingBlock, false);
            }
            else if (e.KeyCode == Keys.Down)
            {
                graphicsService.DrawBlock(_fallingBlock, true);
                _fallingBlock.Ypos += 1;
                graphicsService.DrawBlock(_fallingBlock, false);

                // Reset auto drop counter:
                _counter = 0;
            }
        }
                
    }
}
