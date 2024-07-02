using Raylib_cs;
using System.Numerics;
using System.Runtime.Serialization;

namespace HelloWorld;

internal static class BoardRenderer
{
    public static readonly Color gridColorA = new(181, 136, 99, 255);
    public static readonly Color gridColorB = new(240, 217, 181, 255);
    public static readonly Color lastMoveA = new(171, 162, 58, 255);
    public static readonly Color lastMoveB = new(206, 210, 107, 255);

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

    public static void HighlightMove(VecInt2 start, VecInt2 end, int screenX, int screenY, int cellWidth, int cellHeight)
    {
        HighlightSquare(start.x, start.y, screenX, screenY, cellWidth, cellHeight, (start.x + start.y) % 2 == 1 ? lastMoveA : lastMoveB);
        HighlightSquare(end.x, end.y, screenX, screenY, cellWidth, cellHeight, (end.x + end.y) % 2 == 1 ? lastMoveA : lastMoveB);
    }

    public static void DrawPieces(Board board, int screenX, int screenY, int cellWidth, int cellHeight, VecInt2 hideTile, float rotation)
    {
        for (int y = 0; y < board.boardHeight; y++)
        {
            for (int x = 0; x < board.boardWidth; x++)
            {
                if (board.GetCell(x, y) == 0 || hideTile.x == x && hideTile.y == y) continue;

                DrawPiece(board.GetCell(x, y), screenX + cellWidth * x, screenY + cellHeight * y, cellWidth, cellHeight, rotation);
            }
        }
    }

    public static void DrawPiece(int piece, int screenX, int screenY, int cellWidth, int cellHeight, float rotation)
    {
        Rectangle srec = GetPiecesSrec(piece);
        Rectangle drec = new(screenX + cellWidth / 2, screenY + cellHeight / 2, cellWidth, cellHeight);
        Raylib.DrawTexturePro(pieces, srec, drec, new Vector2(cellWidth / 2f, cellHeight / 2f), rotation, Color.White);
    }

    public static void DrawPiece(int piece, Vector2 center, Vector2 size, float rotation)
    {
        Rectangle srec = GetPiecesSrec(piece);
        Rectangle drec = new(center, size);
        Raylib.DrawTexturePro(pieces, srec, drec, size / 2f, rotation, Color.White);
    }

    public static Rectangle GetPiecesSrec(int piece)
    {
        int pieceX = (piece - 1) % 6;
        int pieceY = (piece - 1) / 6;
        return new Rectangle(pieceX * pieceTexWidth, pieceY * pieceTexHeight, pieceTexWidth, pieceTexHeight);
    }
}
