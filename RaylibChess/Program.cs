using Raylib_cs;
using System.Numerics;

namespace HelloWorld;

class Program
{
    public static Board mainBoard = new Board();

    internal static void Main()
    {
        Raylib.InitWindow(1600, 900, "Hello World");
        BoardRenderer.Initialise();
        mainBoard.ResetBoard();

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(new Color(22, 21, 18, 255));

            BoardRenderer.DrawBoardGrid(400, 40, 100, 100);
            BoardRenderer.DrawPieces(mainBoard, 400, 40, 100, 100);

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    internal class Board
    {
        public int[] board = new int[64];

        public void ResetBoard()
        {
            board = new int[64]
            {
                11,10, 9, 8, 7, 9,10,11,
                12,12,12,12,12,12,12,12,
                 0, 0, 0, 0, 0, 0, 0, 0,
                 0, 0, 0, 0, 0, 0, 0, 0,
                 0, 0, 0, 0, 0, 0, 0, 0,
                 0, 0, 0, 0, 0, 0, 0, 0,
                 6, 6, 6, 6, 6, 6, 6, 6,
                 5, 4, 3, 2, 1, 3, 4, 5,
            };
        }
    }

    internal enum Piece
    {
        None,           //0

        WhiteKing,      //1
        WhiteQueen,     //2
        WhiteBishop,    //3
        WhiteKnight,    //4
        WhiteRook,      //5
        WhitePawn,      //6

        BlackKing,      //7
        BlackQueen,     //8
        BlackBishop,    //9
        BlackKnight,    //10
        BlackRook,      //11
        BlackPawn,      //12
    }

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

        public static void DrawBoardGrid(int screenX, int screenY, int cellWidth, int cellHeight)
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Raylib.DrawRectangle(screenX + (x * cellWidth), screenY + (y * cellHeight), cellWidth, cellHeight, ((x + y) % 2 == 1 ? gridColorA : gridColorB));
                }
            }
        }

        public static void DrawPieces(Board board, int screenX, int screenY, int cellWidth, int cellHeight)
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (board.board[x + (y * 8)] == 0) continue;

                    DrawPiece(board.board[x + (y * 8)], screenX + cellWidth * x, screenY + cellHeight * y, cellWidth, cellHeight);
                }
            }

            Rectangle sourceRectangle = new Rectangle(100, 0, 100, 100);
        }

        public static void DrawPiece(int piece, int screenX, int screenY, int cellWidth, int cellHeight)
        {
            int pieceX = (piece - 1) % 6;
            int pieceY = (piece - 1) / 6;
            Rectangle srec = new Rectangle(pieceX * pieceTexWidth, pieceY * pieceTexHeight, pieceTexWidth, pieceTexHeight);
            Rectangle drec = new Rectangle(screenX, screenY, cellWidth, cellHeight);
            Raylib.DrawTexturePro(pieces, srec, drec, Vector2.Zero, 0, Color.White);
        }
    }
}

public class MathClass
{
    //method - do something
    public void PrintNames()
    {
        Console.WriteLine("John");
        Console.WriteLine("Amanda");
        Console.WriteLine("Steve");
    }

    //funciton - do something, and give me somthing back
    public int CalculateTotalAges()
    {
        int steveAge = 17;
        int amandaAge = 19;
        int johnAge = 23;

        int totalAge = steveAge + amandaAge + johnAge;

        Rectangle r = GetSourceRectangle();

        return totalAge;
    }

    public void DrawSOmething()
    {
        Rectangle source = GetSourceRectangle(/*parameters*/);
    }

    public Dog GetMyDog()
    {
        return new Dog() { name = "bob" };
    }

    public Rectangle GetSourceRectangle()
    {
        //do some claculations

        Rectangle myRect = new Rectangle();
        myRect.Width = 4;
        myRect.Height = 3;
        myRect.X = 100;
        myRect.Y = 250;

        Rectangle fancy = new Rectangle
        {
            Width = 3,
            X = 2,
            Y = 5,
            Height = 10,
        };

        Rectangle secondRectangle = new Rectangle(10, 30, 50, 10);

        return fancy;
    }

    public int Add(int firstNumber, int secondNumber)
    {
        return firstNumber + secondNumber;
    }

    public Rectangle GetTotals(Rectangle recA, Rectangle recB, Rectangle recC)
    {
        Rectangle total = new Rectangle();
        total.X = recA.X + recB.X + recC.X;
        total.Y = recA.Y + recB.Y + recC.Y;
        total.Width = recA.Width + recB.Width + recC.Width;
        total.Height = recA.Height + recB.Height + recC.Height;
        return total;
    }
}

public class Dog
{
    public int age;
    public string name;
}