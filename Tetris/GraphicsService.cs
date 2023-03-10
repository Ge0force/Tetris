using System.Drawing.Text;
using Tetris.BL;
using static System.Windows.Forms.DataFormats;
using Point = Tetris.BL.Point;

namespace Tetris
{
    public class GraphicsService : IGraphicsService
    {
        private const int _blockSize = 35;
        private const int _leftMargin = 100;

        private Graphics _graphicsObj;
        private Form _formObj;

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

        public void DrawNextShape(IShape shape)
        {
            // Clear previous shape            
            _graphicsObj.FillRectangle(Brushes.Black, new Rectangle(_leftMargin + (_blockSize * 15) , (_blockSize * 1) ,  170, 150));

            _graphicsObj.DrawString("NEXT :", new Font("Tahoma", 14), Brushes.White, _leftMargin + (_blockSize * 16), (int)(_blockSize * 1.7));

            int extraMarginTop = 0; 
            int extraMarginLeft = 0;

            if(shape.Pattern.YMax < 2) extraMarginTop = 1;
            if(shape.Pattern.XMax < 2) extraMarginLeft = 1;

            foreach (Point p in shape.Pattern)
            {
                DrawSingleBlock(p.X + 15 + extraMarginLeft, p.Y + 2 + extraMarginTop, shape.PenColor, shape.BrushColor);
            }

        }

        public void DrawSingleBlock(int xPos, int yPos, Color penColor, Color brushColor)
        {
            // Calculate position on the bitmap
            xPos = _leftMargin + ((xPos) * (_blockSize + 1));
            yPos = (yPos * (_blockSize + 1));

            // Be sure to clean up disposable objects 
            using (Pen _myPen = new Pen(penColor))
            using (SolidBrush _myBrush = new SolidBrush(brushColor))
            {
                Rectangle rect = new Rectangle(xPos, yPos, _blockSize, _blockSize);
                _graphicsObj.DrawRectangle(_myPen, rect);
                _graphicsObj.FillRectangle(_myBrush, rect);
            }

            _formObj.Invalidate();
        }

        public void DrawPlayfield(int playfieldWidth, int playfieldHeight)
        {
            // Clear the screen            
            _graphicsObj.FillRectangle(Brushes.Black, new Rectangle(-1, -1, 1000, 1000));

            // Draw bricks
            for (int i = 0; i < playfieldHeight; i++)
            {
                DrawBrick(_leftMargin, (i * (_blockSize + 1)), (i % 2) == 0);
                DrawBrick(_leftMargin + ((_blockSize + 1) * (playfieldWidth + 1)), (i * (_blockSize + 1)), (i % 2) == 0);
            }

            _formObj.Invalidate();
        }

        private void DrawBrick(int xPos, int yPos, bool odd)
        {
            // Be sure to clean up disposable objects 
            using (Pen grayPen = new Pen(Color.Gray))
            { 
                grayPen.Width = 2;

                Rectangle rect = new Rectangle(xPos, yPos, _blockSize, _blockSize);
                Rectangle rect2 = new Rectangle(xPos + 1, yPos + 1, _blockSize - 2, _blockSize - 2);

                _graphicsObj.DrawRectangle(Pens.DarkRed, rect);
                _graphicsObj.FillRectangle(Brushes.DarkRed, rect);

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
        public void DisplayGameOver()
        {
            _graphicsObj.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            Rectangle rect = new Rectangle(_leftMargin + (3 * _blockSize), 7 * _blockSize, 7 * _blockSize, 2 * _blockSize);

            _graphicsObj.DrawRectangle(Pens.Yellow, rect);
            _graphicsObj.FillRectangle(Brushes.Yellow, rect);

            StringFormat format = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            // Draw the text onto the image
            _graphicsObj.DrawString("GAME OVER", new Font("Tahoma", 14), Brushes.Black, rect, format);

            _formObj.Invalidate();
        }

        public void RedrawShapes(List<IShape> shapes, int playfieldWidth, int playfieldHeight)
        {
            // Clear inside of the playfield
            Rectangle rect = new Rectangle(_leftMargin + _blockSize + 1, 0, playfieldWidth * (_blockSize + 1), playfieldHeight * (_blockSize + 1));
            _graphicsObj.DrawRectangle(Pens.Black, rect);
            _graphicsObj.FillRectangle(Brushes.Black, rect);

            // Draw all shapes
            foreach (var s in shapes)
            {
                DrawShape(s);
            }

            _formObj.Invalidate();
        }

    }
}
