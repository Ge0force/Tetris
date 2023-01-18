namespace Tetris
{
    public class GraphicsService
    {
        private const int _blockSize = 35;
        private const int _leftMargin = 100;

        private Graphics _graphicsObj;
        private Form _formObj;

        private SolidBrush _myBrush = new SolidBrush(Color.Black);
        private Pen _myPen = new Pen(Color.Black);

        public GraphicsService(Form parentForm, Graphics gfx)
        {
            _formObj = parentForm;
            _graphicsObj = gfx;
        }

        public void EraseShape(IShape shape)
        {
            foreach (Point p in shape.Pattern)
            {
                DrawSingleBlock(p.X + shape.Position.X, p.Y + shape.Position.Y, Color.Black, Color.Black);
            }

        }

        public void DrawShape(IShape shape)
        {
            foreach (Point p in shape.Pattern)
            {
                DrawSingleBlock(p.X + shape.Position.X, p.Y + shape.Position.Y, shape.PenColor, shape.BrushColor);
            }
        }

        public void DrawSingleBlock(int xPos, int yPos, Color penColor, Color brushColor)
        {
            xPos = _leftMargin + ((xPos) * (_blockSize + 1));
            yPos = (yPos * (_blockSize + 1));

            _myPen.Color = penColor;
            _myBrush.Color = brushColor;

            Rectangle rect = new Rectangle(xPos, yPos, _blockSize, _blockSize);
            _graphicsObj.DrawRectangle(_myPen, rect);
            _graphicsObj.FillRectangle(_myBrush, rect);

            _formObj.Invalidate();
        }

        public void DrawPlayfield(int playfieldWidth, int playfieldHeight)
        {
            _graphicsObj.FillRectangle(_myBrush, new Rectangle(- 1, - 1, 1000, 1000));

            for (int i = 0; i < playfieldHeight; i++) 
            {
                DrawBrick(_leftMargin, (i * (_blockSize + 1)), (i % 2) == 0);
                DrawBrick(_leftMargin + ((_blockSize + 1) * (playfieldWidth + 1)), (i * (_blockSize + 1)), (i % 2) == 0);
            }

            _formObj.Invalidate();
        }

        private void DrawBrick( int xPos, int yPos, bool odd)
        {
            Pen grayPen = new Pen(Color.Gray);
            Pen darkRedPen = new Pen(Color.DarkRed);

            grayPen.Width = 2;

            SolidBrush darkRedBrush = new SolidBrush(Color.DarkRed);

            Rectangle rect = new Rectangle(xPos, yPos, _blockSize, _blockSize);
            Rectangle rect2 = new Rectangle(xPos + 1, yPos + 1, _blockSize - 2, _blockSize - 2);

            _graphicsObj.DrawRectangle(darkRedPen, rect);
            _graphicsObj.FillRectangle(darkRedBrush, rect);

            _graphicsObj.DrawLine(grayPen, xPos, yPos, xPos + _blockSize, yPos);

            if (odd)
            {
                _graphicsObj.DrawLine(grayPen, xPos + (_blockSize / 2), yPos, xPos + (_blockSize / 2), yPos + _blockSize);
            }
            else
            {
                _graphicsObj.DrawLine(grayPen, xPos + (_blockSize / 5), yPos, xPos + (_blockSize / 5), yPos + _blockSize);
                _graphicsObj.DrawLine(grayPen, xPos + (_blockSize / 5 * 4), yPos, xPos + (_blockSize / 5 * 4), yPos + _blockSize);
            }

        }

    }
}
