using Raylib_cs;
using System.Security.Cryptography.X509Certificates;
using System.Numerics;

namespace HelloWorld;

internal class Program
{
    public static Board mainBoard = new(8, 8);
    public static ScreenConfig sc = new(
        screenWidth: 1920,
        screenHeight: 1080,
        boardScreenWidth: 1000,
        boardScreenHeight: 1000,
        board: mainBoard
        );
    public static PieceMovementManager pieceMovement = new();
    public static int playerTurn = 0;

    internal static void Main()
    {
        Raylib.InitWindow(sc.screenWidth, sc.screenHeight, "Hello World");
        BoardRenderer.Initialise();
        mainBoard.SetupBoard("rnbnrnbkqbnrnbnr");

        while (!Raylib.WindowShouldClose())
        {
            pieceMovement.Update(mainBoard, sc, playerTurn, out bool moved);
            if (moved) playerTurn = playerTurn == 0 ? 1 : 0;

            Raylib.BeginDrawing();
            Raylib.ClearBackground(new Color(22, 21, 18, 255));

            BoardRenderer.DrawBoardGrid(mainBoard, sc.boardScreenOffsetX, sc.boardScreenOffsetY, sc.boardCellWidth, sc.boardCellHeight);
            
            BoardRenderer.DrawPieces(mainBoard, sc.boardScreenOffsetX, sc.boardScreenOffsetY, sc.boardCellWidth, sc.boardCellHeight, pieceMovement.heldPieceHideTile);
            pieceMovement.Draw(sc);

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}

internal class ScreenConfig
{
    public int screenWidth;
    public int screenHeight;
    public int boardScreenOffsetX;
    public int boardScreenOffsetY;
    public int boardCellWidth;
    public int boardCellHeight;

    public ScreenConfig(int screenWidth, int screenHeight, int boardScreenOffsetX, int boardScreenOffsetY, int boardCellWidth, int boardCellHeight)
    {
        this.screenWidth = screenWidth;
        this.screenHeight = screenHeight;
        this.boardScreenOffsetX = boardScreenOffsetX;
        this.boardScreenOffsetY = boardScreenOffsetY;
        this.boardCellWidth = boardCellWidth;
        this.boardCellHeight = boardCellHeight;
    }

    public ScreenConfig(int screenWidth, int screenHeight, int boardScreenWidth, int boardScreenHeight, Board board)
    {
        this.screenWidth = screenWidth;
        this.screenHeight = screenHeight;
        boardScreenOffsetX = (screenWidth - boardScreenWidth) / 2;
        boardScreenOffsetY = (screenHeight - boardScreenHeight) / 2;
        boardCellWidth = boardScreenWidth / board.boardWidth;
        boardCellHeight = boardScreenHeight / board.boardHeight;
    }
}
