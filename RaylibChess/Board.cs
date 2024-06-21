namespace HelloWorld;

internal class Board
{
    public int boardWidth;
    public int boardHeight;

    public int[] board;

    public void SetupBoard(string backRow, bool pawns = true)
    {
        int piecesStart = (boardWidth - backRow.Length ) / 2;
        for (int i = 0; i < boardWidth; i++)
        {
            //set pawns
            if (pawns)
            {
                SetCell(i, 1, (int)Piece.BlackPawn);
                SetCell(i, boardHeight - 2, (int)Piece.WhitePawn);
            }

            //set pieces
            int j = i - piecesStart;
            if (j >= 0 && j < backRow.Length)
            {
                SetCell(i, 0, GetPieceFromString(backRow, j, white: false));
                SetCell(i, boardHeight - 1, GetPieceFromString(backRow, j, white: true));
            }
        }
    }

    public int GetPieceFromString(string str, int index, bool white)
    {
        return (white ? 0 : 6) + str.ToLower()[index] switch
        {
            'k' => 1,
            'q' => 2,
            'b' => 3,
            'n' => 4,
            'r' => 5,
            'p' => 6,
            _ => (white ? 0 : -6)
        };
    }

    public int GetCell(int x, int y)
    {
        return board[(x % boardWidth) + (y * boardWidth)];
    }
    
    public void SetCell(int x, int y, int value)
    {
        board[x + (y * boardWidth)] = value;
    }

    public Board(int boardWidth, int boardHeight)
    {
        this.boardWidth = boardWidth;
        this.boardHeight = boardHeight;
        this.board = new int[boardWidth * boardHeight];
    }
}
