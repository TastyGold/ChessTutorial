﻿using Raylib_cs;
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
    public static Camera2D boardCamera = new Camera2D()
    {
        Target = new Vector2(sc.screenWidth / 2, sc.screenHeight / 2),
        Offset = new Vector2(sc.screenWidth / 2, sc.screenHeight / 2),
        Zoom = 1,
        Rotation = 180,
    };

    internal static void Main()
    {
        Raylib.InitWindow(sc.screenWidth, sc.screenHeight, "Hello World");
        BoardRenderer.Initialise();
        mainBoard.SetupBoard("rnbqkbnr");

        while (!Raylib.WindowShouldClose())
        {
            pieceMovement.Update(mainBoard, sc, boardCamera, playerTurn, out bool moved);
            if (moved) playerTurn = playerTurn == 0 ? 1 : 0;
            boardCamera.Rotation = 180 * playerTurn;

            Raylib.BeginDrawing();
            Raylib.BeginMode2D(boardCamera);
            Raylib.ClearBackground(new Color(22, 21, 18, 255));

            BoardRenderer.DrawBoardGrid(mainBoard, sc.boardScreenOffsetX, sc.boardScreenOffsetY, sc.boardCellWidth, sc.boardCellHeight);
            //BoardRenderer.HighlightSquare(pieceMovement.mouseBoardPosition.x, pieceMovement.mouseBoardPosition.y, sc.boardScreenOffsetX, sc.boardScreenOffsetY, sc.boardCellWidth, sc.boardCellHeight, Color.Lime);
            pieceMovement.HighlightLastMove(sc);

            BoardRenderer.DrawPieces(mainBoard, sc.boardScreenOffsetX, sc.boardScreenOffsetY, sc.boardCellWidth, sc.boardCellHeight, pieceMovement.heldPieceHideTile, boardCamera.Rotation);
            pieceMovement.Draw(sc, boardCamera.Rotation);

            Raylib.EndMode2D();
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}
