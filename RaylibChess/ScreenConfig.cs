namespace HelloWorld;

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
