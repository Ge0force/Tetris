namespace Tetris
{
    public partial class Form1 : Form
    {
        
        private GraphicsService _graphicsService;       // Class to handle the graphics
        private TetrisGame _gameLoop;                   // Class to handle the gameloop
                
        private Bitmap _drawingArea;                    // Area to draw on

        public Form1()
        {
            InitializeComponent();

            // Create framebuffer bitmap
            _drawingArea = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // Create graphics object that GraphicsService needs to draw on
            Graphics oGraphics;
            oGraphics = Graphics.FromImage(_drawingArea);

            // GraphicsService needs a reference to the form to be able to Invalidate the form
            _graphicsService = new GraphicsService(this, oGraphics);

            // Create and start game loop
            _gameLoop = new TetrisGame(_graphicsService);
            _gameLoop.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle keys pressed
            _gameLoop.KeyDown(e);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (_gameLoop != null)
            {
                e.Graphics.DrawImage(_drawingArea, 0, 0, _drawingArea.Width, _drawingArea.Height);
            }

        }

    }
}

