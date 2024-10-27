

class Game
{
    public char[,] board = new char[3,3];
    public char WhoWon = ' ';
    public char currentTurn = 'X';
    int numOfTurns = 0;
    public Game()
    {
        for (int i = 0; i < this.board.GetLength(0); i++)
        {
            for (int j = 0; j < this.board.GetLength(1); j++)
            {
                this.board[i,j] = ' ';
            }
        }
    }
    public void print()
    {
        for (int i =1; i <= 3; i++)
        {
            Console.Write(" " + i + "  ");
        }
        Console.WriteLine();
        for (int i =0; i < this.board.GetLength(0); i++)
        {
            for (int j = 0; j < this.board.GetLength(1); j++)
            {

                    Console.Write(" " + this.board[i, j] + " |");
            }
            if (i != this.board.GetLength(0)-1)
            {
                Console.WriteLine("  " + (i +1) + "\n------------");
            } else
            {
                Console.WriteLine("  " + (i + 1));
            }
        }
    }
    public void checkForWin()
    {
        List<char[]> ls = new List<char[]>();
        for (int i =0; i < this.board.GetLength(0); i++)
        {
            // defining new array and placing it in the list
            ls.Add(new char[] { this.board[i, 0], this.board[i, 1], this.board[i, 2] });
        }
        for (int j = 0; j < this.board.GetLength(1); j++)
        {
            // defining new array and placing it in the list
            ls.Add(new char[] { this.board[0, j], this.board[1, j], this.board[2, j] });
        }
        ls.Add(new char[] { this.board[0, 0], this.board[1, 1], this.board[2, 2] });
        ls.Add(new char[] { this.board[2, 0], this.board[1, 1], this.board[0, 2] });

        for (int i =0; i < ls.Count; i++)
        {
            if (ls[i][0] == ls[i][1] && ls[i][1] == ls[i][2])
            {
                this.WhoWon = ls[i][0]; // all of them are the same so doesn't matter which index so we choose 0
                return;
            }
        }

        if (this.numOfTurns == 9) // means the board is full and didn't find a winner
        {
            this.WhoWon = '-';
        }
    }
    public void handleInput()
    {
        Console.WriteLine("Current turn: " + this.currentTurn);
        int i, j;
        do
        {
            Console.Write("Enter row num: ");
            i = Convert.ToInt32(Console.ReadLine()) - 1;
            Console.Write("Enter column num: ");
            j = Convert.ToInt32(Console.ReadLine()) - 1;
            if ((i < 0 || i > 2 || j < 0 || j > 2) || this.board[i, j] != ' ')
            {
                Console.WriteLine("You entered invalid position or one which is filled\nPlease fill again");
            }
        } while ((i < 0 || i > 2 || j < 0 || j > 2) ||  this.board[i,j] != ' ');
        this.board[i, j] = this.currentTurn;
        this.numOfTurns++;
        this.checkForWin();
        if (this.currentTurn == 'X') {
            this.currentTurn = 'O';
        } else
        {
            this.currentTurn = 'X';
        }
    }
    public void endGamePrints()
    {
        this.print();
        if (this.WhoWon == '-')
        {
            Console.WriteLine("No one won the game :(");
        } else
        {
            Console.WriteLine(this.WhoWon + " won the game :)");
        }
    }
}

namespace TicTacToe
{
    class Program
    {
        public static void Main(string[] args)
        {
            Game game = new Game();
            while (game.WhoWon == ' ')
            {
                game.print();
                game.handleInput();
            }
            game.endGamePrints();
            Console.Write("Would you like to play again(y/n)? ");
            char answer = Console.ReadLine()[0];
            if (answer == 'y')
            {
                Main(args);
            } else
            {
                Console.WriteLine("Thanks for playing TicTacToe");
            }
        }
    }
}