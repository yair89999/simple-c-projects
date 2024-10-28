public class Game
{
    public string[] words = {"hellot", "test", "sport"};
    public string word;
    public char[] guessedWord;
    public string guessedWordString;
    public List<char> guessedChars;
    public int guessesLeft = 5;

    public Game()
    {
        Random rnd = new Random();
        this.word = words[rnd.Next(words.Length)];
        this.guessedWord = new char[this.word.Length];
        for (int i =0; i < this.word.Length; i++)
        {
            this.guessedWord[i] = '_';
            this.guessedWordString += '_';
        }
        this.guessedChars = new List<char>();
    }

    public void print()
    {
        Console.Write("\nWord: ");
        foreach (char c in this.guessedWord)
        {
            Console.Write(c);
        }
        Console.WriteLine();
        Console.WriteLine("Guesses left: " + this.guessesLeft);
        Console.Write("Guessed Letters: ");
        foreach (char c in this.guessedChars)
        {
            Console.Write(c + ", ");
        }
        Console.WriteLine();
    }

    public void handleInput()
    {
        Console.Write("Your guess: "); // for the input to be right after it
        char guessedChar = Console.ReadLine()[0];

        foreach(char c in this.guessedChars)
        {
            if (c == guessedChar)
                return;
        }

        bool added = false;
        for (int i =0; i < this.word.Length; i++)
        {
            if (this.word[i] == guessedChar)
            {
                this.guessedWord[i] = this.word[i];
                added = true;
            }
        }
        this.guessedWordString = "";
        foreach(char c in this.guessedWord)
        {
            guessedWordString += c;
        }
        if (added == false)
        {
            this.guessedChars.Add(guessedChar);
            this.guessesLeft--;
        }
    }
}

namespace Hangman
{
    class Program
    {
        public static void Main(string[] args)
        {
            Game game = new Game();
            
            while (game.word != game.guessedWordString && game.guessesLeft != 0)
            {
                game.print();
                game.handleInput();
            }

            if (game.word == game.guessedWordString)
            {
                Console.WriteLine("You are right!");
                Console.WriteLine("The word is: " + game.word);
            } else // meaning he lost
            {
                Console.WriteLine("You lost :(");
                Console.WriteLine("The word was: " + game.word);
            }

            Console.Write("Would you like to play again(y/n)? ");
            char answer = Console.ReadLine()[0];
            if (answer == 'y')
            {
                Main(args);
            }
            else
            {
                Console.WriteLine("Thanks for playing my hangman game :)");
            }
        }
    }
}
