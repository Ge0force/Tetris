using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class GraphicsService
    {
        private Graphics _graphicsObj;
        private Form _formObj;

        private int _blockSize;

        private SolidBrush _myBrush = new SolidBrush(Color.Black);
        private Pen _myPen = new Pen(Color.White);

        public GraphicsService(Form parentForm, Graphics gfx)
        {
            _formObj = parentForm;
            _graphicsObj = gfx;
            
            // Calculate size of a single block based on the form height
            _blockSize = _formObj.ClientRectangle.Height / 18;
        }

        public void DrawBlock(Block block, bool eraseBlock)
        {
            Color penColor = eraseBlock ? Color.Black : block.penColor;
            Color brushColor = eraseBlock ? Color.Black : block.brushColor;

            for (int y = 0; y < 3; y++)   // Must be block.size
            {
                for (int x = 0; x < 3; x++)
                {
                    if (block.Pattern[x, y] == true)
                    {
                        DrawSingleBlock(block.Xpos + x, block.Ypos + y, penColor, brushColor);
                    }
                }
            }
        }

        public void DrawSingleBlock(int _left, int _top, Color penColor, Color brushColor)
        {
            int xPos = 100 + ((_left +1) * _blockSize);
            int yPos = (_top * _blockSize) - 1;

            _myPen.Color = penColor;
            _myBrush.Color = brushColor;

            Rectangle rect = new Rectangle(xPos, yPos, _blockSize, _blockSize);
            _graphicsObj.DrawRectangle(_myPen, rect);
            _graphicsObj.FillRectangle(_myBrush, rect);

            _formObj.Invalidate();
        }


        public void DrawPlayfield()
        {
            _graphicsObj.FillRectangle(_myBrush, new Rectangle(- 1, - 1, 1000, 1000));

            for (int i = 0; i < 18; i++) 
            {
                DrawBrick(100, (i * _blockSize) - 1, (i % 2) == 0);
                DrawBrick(100 + (_blockSize * 12), (i * _blockSize) - 1, (i % 2) == 0);
            }

            _formObj.Invalidate();
        }

        private void DrawBrick( int xpos, int ypos, bool odd)
        {
            Pen grayPen = new Pen(Color.Gray);
            Pen darkRedPen = new Pen(Color.DarkRed);

            grayPen.Width = 2;

            SolidBrush darkRedBrush = new SolidBrush(Color.DarkRed);

            Rectangle rect = new Rectangle(xpos, ypos, _blockSize, _blockSize);
            Rectangle rect2 = new Rectangle(xpos + 1, ypos + 1, _blockSize - 2, _blockSize - 2);

            _graphicsObj.DrawRectangle(darkRedPen, rect);
            _graphicsObj.FillRectangle(darkRedBrush, rect);

            _graphicsObj.DrawLine(grayPen, xpos, ypos, xpos + _blockSize, ypos);

            if (odd)
            {
                _graphicsObj.DrawLine(grayPen, xpos + (_blockSize / 2), ypos, xpos + (_blockSize / 2), ypos + _blockSize);
            }
            else
            {
                _graphicsObj.DrawLine(grayPen, xpos + (_blockSize / 5), ypos, xpos + (_blockSize / 5), ypos + _blockSize);
                _graphicsObj.DrawLine(grayPen, xpos + (_blockSize / 5 * 4), ypos, xpos + (_blockSize / 5 * 4), ypos + _blockSize);
            }

        }

    }
}
