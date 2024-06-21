using Raylib_cs;
using System.Numerics;
using System.Runtime.Serialization;

namespace HelloWorld;

internal static class BoardRenderer
{
    public static Color gridColorA = new Color(181, 136, 99, 255);
    public static Color gridColorB = new Color(240, 217, 181, 255);

    public static Texture2D pieces;
    public static float pieceTexWidth;
    public static float pieceTexHeight;

    public static void Initialise()
    {
        pieces = Raylib.LoadTexture("..//..//..//Assets/chessPieces.png");
        Raylib.SetTextureFilter(pieces, TextureFilter.Bilinear);
        pieceTexWidth = pieces.Width / 6;
        pieceTexHeight = pieces.Height / 2;
    }

    public static void DrawBoardGrid(Board board, int screenX, int screenY, int cellWidth, int cellHeight)
    {
        for (int y = 0; y < board.boardHeight; y++)
        {
            for (int x = 0; x < board.boardWidth; x++)
            {
                Raylib.DrawRectangle(screenX + (x * cellWidth), screenY + (y * cellHeight), cellWidth, cellHeight, ((x + y) % 2 == 1 ? gridColorA : gridColorB));
            }
        }
    }

    public static void HighlightSquare(int x, int y, int screenX, int screenY, int cellWidth, int cellHeight, Color col)
    {
        Raylib.DrawRectangle(screenX + (x * cellWidth), screenY + (y * cellHeight), cellWidth, cellHeight, col);
    }

    public static void DrawPieces(Board board, int screenX, int screenY, int cellWidth, int cellHeight, VecInt2 hideTile)
    {
        for (int y = 0; y < board.boardHeight; y++)
        {
            for (int x = 0; x < board.boardWidth; x++)
            {
                if (board.GetCell(x, y) == 0 || hideTile.x == x && hideTile.y == y) continue;

                DrawPiece(board.GetCell(x, y), screenX + cellWidth * x, screenY + cellHeight * y, cellWidth, cellHeight);
            }
        }

        Rectangle sourceRectangle = new Rectangle(100, 0, 100, 100);
    }

    public static void DrawPiece(int piece, int screenX, int screenY, int cellWidth, int cellHeight)
    {
        Rectangle srec = GetPiecesSrec(piece);
        Rectangle drec = new Rectangle(screenX, screenY, cellWidth, cellHeight);
        Raylib.DrawTexturePro(pieces, srec, drec, Vector2.Zero, 0, Color.White);
    }

    public static void DrawPiece(int piece, Vector2 center, Vector2 size)
    {
        Rectangle srec = GetPiecesSrec(piece);
        Rectangle drec = new Rectangle(center - (size / 2), size);
        Raylib.DrawTexturePro(pieces, srec, drec, Vector2.Zero, 0, Color.White);
    }

    public static Rectangle GetPiecesSrec(int piece)
    {
        int pieceX = (piece - 1) % 6;
        int pieceY = (piece - 1) / 6;
        return new Rectangle(pieceX * pieceTexWidth, pieceY * pieceTexHeight, pieceTexWidth, pieceTexHeight);
    }
}
