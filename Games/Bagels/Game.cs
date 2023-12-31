using System.Text;

namespace Games.Bagels
{
    public class Game
    {
        public string CurrentGuess { get; set; } = "";
        public string Clues { get; set; } = "";
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

        public void ClearNumber()
        {
            CurrentGuess = "";
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

        public void GetClues()
        {
            var clues = new List<string>();

            // increment the guess count
            GuessCount++;

            // if the user has guessed too many times
            if (GuessCount > MaxGuesses)
            {
                // the computer responds with "You lose!"
                clues.Add($"You ran out of guesses: {secretNumber}!");
            }      

            // if the user guessed the correct number in the correct position
            if (CurrentGuess == secretNumber.ToString())
            {
                // the computer responds with "You got it!"
                clues.Add("You got it!");
            }
            else
            {
                // the computer responds with "Fermi" if the user guessed the correct number in the correct position
                if (CurrentGuess[0] == secretNumber.ToString()[0])
                {
                    clues.Add("Fermi ");
                }
                else if (CurrentGuess[1] == secretNumber.ToString()[1])
                {
                    clues.Add("Fermi ");
                }
                else if (CurrentGuess[2] == secretNumber.ToString()[2])
                {
                    clues.Add("Fermi ");
                }
                // the computer responds with "Pico" if the user guessed the correct number in the wrong position
                if (CurrentGuess[0] == secretNumber.ToString()[1] || CurrentGuess[0] == secretNumber.ToString()[2])
                {
                    clues.Add("Pico ");
                }
                else if (CurrentGuess[1] == secretNumber.ToString()[0] || CurrentGuess[1] == secretNumber.ToString()[2])
                {
                    clues.Add("Pico ");
                }
                else if (CurrentGuess[2] == secretNumber.ToString()[0] || CurrentGuess[2] == secretNumber.ToString()[1])
                {
                    clues.Add("Pico ");
                }
                // the computer responds with "Bagels" if the user guessed the wrong number
                if (CurrentGuess[0] != secretNumber.ToString()[0] && CurrentGuess[0] != secretNumber.ToString()[1] && CurrentGuess[0] != secretNumber.ToString()[2] &&
                                       CurrentGuess[1] != secretNumber.ToString()[0] && CurrentGuess[1] != secretNumber.ToString()[1] && CurrentGuess[1] != secretNumber.ToString()[2] &&
                                                          CurrentGuess[2] != secretNumber.ToString()[0] && CurrentGuess[2] != secretNumber.ToString()[1] && CurrentGuess[2] != secretNumber.ToString()[2])
                {
                    clues.Add("Bagels ");
                }
            }
            // the computer responds with the clues in alphabetical order
            clues.Sort();
            Clues = clues.Aggregate((a, b) => $"{a} {b}");
        }
    }
}
