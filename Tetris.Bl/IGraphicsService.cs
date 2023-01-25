using System.Drawing;
using Tetris.BL;

namespace Tetris
{
    public interface IGraphicsService
    {
        void DisplayGameOver();
        void DrawPlayfield(int playfieldWidth, int playfieldHeight);
        void DrawShape(IShape shape);
        void DrawNextShape(IShape shape);
        void DrawSingleBlock(int xPos, int yPos, Color penColor, Color brushColor);
        void EraseShape(IShape shape);
        void RedrawShapes(List<IShape> shapes, int playfieldWidth, int playfieldHeight);
    }
}