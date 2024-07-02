using System.Linq.Expressions;
using System.Numerics;
using Raylib_cs;

namespace HelloWorld;

internal class PieceMovementManager
{
    public int heldPieceId = 0;
    public VecInt2 heldPieceHideTile = new VecInt2(-1, -1);
    public Vector2 heldPiecePosition;
    public VecInt2 mouseBoardPosition;
    public VecInt2 lastMoveStart = new VecInt2(-1, -1);
    public VecInt2 lastMoveEnd = new VecInt2(-1, -1);

    public void Update(Board board, ScreenConfig screen, Camera2D camera, int playerTurn, out bool moveMade)
    {
        heldPiecePosition = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), camera);
        mouseBoardPosition = GetScreenToBoardPosition(screen, heldPiecePosition);

        bool mouseOnBoard = IsMouseOnBoard(board, screen, heldPiecePosition);

        if (mouseOnBoard && heldPieceId <= 0)
        {
            int hoveredPiece = board.GetCell(mouseBoardPosition.x, mouseBoardPosition.y);
            if (hoveredPiece > 0 && (hoveredPiece - 1) / 6 == playerTurn && Raylib.IsMouseButtonPressed(0)) 
            {
                heldPieceId = hoveredPiece;
                heldPieceHideTile = new VecInt2(mouseBoardPosition.x, mouseBoardPosition.y);
            }
        }

        moveMade = false;
        if (heldPieceId > 0 && Raylib.IsMouseButtonReleased(0))
        {
            if (mouseOnBoard && mouseBoardPosition != heldPieceHideTile)
            {
                board.SetCell(mouseBoardPosition.x, mouseBoardPosition.y, heldPieceId);
                board.SetCell(heldPieceHideTile.x, heldPieceHideTile.y, 0);

                lastMoveStart = heldPieceHideTile;
                lastMoveEnd = mouseBoardPosition;
                moveMade = true;
            }
            heldPieceHideTile = new VecInt2(-1, -1);
            heldPieceId = 0;
        }
    }

    public void HighlightLastMove(ScreenConfig sc)
    {
        if (lastMoveStart.x != -1) BoardRenderer.HighlightMove(lastMoveStart, lastMoveEnd, sc.boardScreenOffsetX, sc.boardScreenOffsetY, sc.boardCellWidth, sc.boardCellHeight);
    }

    public void Draw(ScreenConfig sc, float rotation)
    {
        if (heldPieceId > 0) BoardRenderer.DrawPiece(heldPieceId, heldPiecePosition, new Vector2(sc.boardCellWidth, sc.boardCellHeight), rotation);
    }

    public static VecInt2 GetScreenToBoardPosition(ScreenConfig screen, Vector2 mousePos)
    {
        int x = (int)((mousePos.X - screen.boardScreenOffsetX) / screen.boardCellWidth);
        int y = (int)((mousePos.Y - screen.boardScreenOffsetY) / screen.boardCellHeight);
        return new VecInt2(x, y);
    }

    public static bool IsMouseOnBoard(Board board, ScreenConfig screen, Vector2 mousePos)
    {
        return mousePos.X >= screen.boardScreenOffsetX
            && mousePos.Y >= screen.boardScreenOffsetY
            && mousePos.X < screen.boardScreenOffsetX + screen.boardCellWidth * board.boardWidth
            && mousePos.Y < screen.boardScreenOffsetY + screen.boardCellHeight * board.boardHeight;
    }

}