using System.Text;

namespace Games.Bagels
{
    public class Game
    {
        public string CurrentGuess { get; private set; } = "";
        private int GuessCount;
        private readonly int secretNumber = 0;
        private const int MaxGuesses = 10;

        public Game()
        {
            secretNumber = GetSecretNumber();
        }

        private static int GetSecretNumber()
        {
            int secretNumber = new Random().Next(100, 999);
            return secretNumber;
        }

        public void SetNumber(string number)
        {
            if (CurrentGuess.Length < 3)
                CurrentGuess += number;
        }

        public void DeleteNumber()
        {
            // remove the last digit from the CurrentGuess
            // if the CurrentGuess is empty, do nothing
            if (CurrentGuess.Length > 0)
                CurrentGuess = CurrentGuess[..^1];
        }

        public string GetClues()
        {
            // increment the guess count
            GuessCount++;

            // if the user has guessed too many times
            if (GuessCount > MaxGuesses)
            {
                // the computer responds with "You lose!"
                return $"You ran out of guesses: {secretNumber}!";
            }

            var clues = new StringBuilder();

            // if the user guessed the correct number in the correct position
            if (CurrentGuess == secretNumber.ToString())
            {
                // the computer responds with "You got it!"
                clues.Append("You got it!");
            }
            else
            {
                // the computer responds with "Fermi" if the user guessed the correct number in the correct position
                if (CurrentGuess[0] == secretNumber.ToString()[0])
                {
                    clues.Append("Fermi ");
                }
                else if (CurrentGuess[1] == secretNumber.ToString()[1])
                {
                    clues.Append("Fermi ");
                }
                else if (CurrentGuess[2] == secretNumber.ToString()[2])
                {
                    clues.Append("Fermi ");
                }
                // the computer responds with "Pico" if the user guessed the correct number in the wrong position
                if (CurrentGuess[0] == secretNumber.ToString()[1] || CurrentGuess[0] == secretNumber.ToString()[2])
                {
                    clues.Append("Pico ");
                }
                else if (CurrentGuess[1] == secretNumber.ToString()[0] || CurrentGuess[1] == secretNumber.ToString()[2])
                {
                    clues.Append("Pico ");
                }
                else if (CurrentGuess[2] == secretNumber.ToString()[0] || CurrentGuess[2] == secretNumber.ToString()[1])
                {
                    clues.Append("Pico ");
                }
                // the computer responds with "Bagels" if the user guessed the wrong number
                if (CurrentGuess[0] != secretNumber.ToString()[0] && CurrentGuess[0] != secretNumber.ToString()[1] && CurrentGuess[0] != secretNumber.ToString()[2] &&
                                       CurrentGuess[1] != secretNumber.ToString()[0] && CurrentGuess[1] != secretNumber.ToString()[1] && CurrentGuess[1] != secretNumber.ToString()[2] &&
                                                          CurrentGuess[2] != secretNumber.ToString()[0] && CurrentGuess[2] != secretNumber.ToString()[1] && CurrentGuess[2] != secretNumber.ToString()[2])
                {
                    clues.Append("Bagels ");
                }
            }   
            return clues.ToString();
        }
    }
}
