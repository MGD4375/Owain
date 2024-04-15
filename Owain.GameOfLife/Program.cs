// See https://aka.ms/new-console-template for more information


const int dimension = 20;
var board = new bool[dimension, dimension];
var rand = new Random();
board = Randomise(board);
Print(board);

while (true)
{
    board = Tick(board);
    Print(board);
    Thread.Sleep(400);

}

bool[,] Randomise(bool[,] board)
{
    var shadowBoard = new bool[dimension, dimension];
    for (var rowIndex = 0; rowIndex < board.GetLength(0); rowIndex++)
    {
        for (var colIndex = 0; colIndex < board.GetLength(1); colIndex++)
        {
            var foo = rand.Next(0, 10);
            Console.WriteLine(foo);

            shadowBoard[rowIndex, colIndex] = foo > 5;
        }
    }
    return shadowBoard;
}
static void Print(bool[,] board)
{
    Console.Clear();

    for (var rowIndex = 0; rowIndex < board.GetLength(0); rowIndex++)
    {
        for (var colIndex = 0; colIndex < board.GetLength(1); colIndex++)
        {
            Console.Write(board[rowIndex, colIndex] ? " X " : " . ");
        }
        Console.WriteLine();
    }
}
static bool[,] Tick(bool[,] board)
{
    bool[,] nextBoard = new bool[board.GetLength(0), board.GetLength(1)]; //  TODO: make this work for any grid size

    for (var rowIndex = 0; rowIndex < board.GetLength(0); rowIndex++)
    {
        for (var colIndex = 0; colIndex < board.GetLength(1); colIndex++)
        {
            var currentCell = board[rowIndex, colIndex];
            var liveNeighbourCount = GetLiveNeighbourCount(board, rowIndex, colIndex);
            var nextState = IsAlive(currentCell, liveNeighbourCount);

            nextBoard[rowIndex, colIndex] = nextState;
        }

    }

    return nextBoard;
}

static bool IsAlive(bool currentCell, int liveNeighbourCount)
{
    bool nextState = false;

    if (liveNeighbourCount < 2) { nextState = false; }
    else if (currentCell && liveNeighbourCount > 1 && liveNeighbourCount < 4) { nextState = true; }
    else if (!currentCell && liveNeighbourCount == 3) { nextState = true; }

    return nextState;
}

static int GetLiveNeighbourCount(bool[,] board, int rowIndex, int colIndex)
{
    var count = 0;

    //  Ternary operator ? : 
    //  TODO: Explore grid wrapping
    count += rowIndex != 0 && colIndex != 0 && board[rowIndex - 1, colIndex - 1] ? 1 : 0;
    count += rowIndex != 0 && board[rowIndex - 1, colIndex + 0] ? 1 : 0;
    count += rowIndex != 0 && colIndex != board.GetLength(0) - 1 && board[rowIndex - 1, colIndex + 1] ? 1 : 0;

    count += colIndex != 0 && board[rowIndex + 0, colIndex - 1] ? 1 : 0;
    count += colIndex != board.GetLength(0) - 1 && board[rowIndex + 0, colIndex + 1] ? 1 : 0;

    count += rowIndex != board.GetLength(1) - 1 && colIndex != 0 && board[rowIndex + 1, colIndex - 1] ? 1 : 0;
    count += rowIndex != board.GetLength(1) - 1 && board[rowIndex + 1, colIndex - 0] ? 1 : 0;
    count += rowIndex != board.GetLength(1) - 1 && colIndex != board.GetLength(0) - 1 && board[rowIndex + 1, colIndex + 1] ? 1 : 0;

    return count;
}

