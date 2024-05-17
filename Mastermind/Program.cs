using System;

namespace Mastermind
{
    class Program
    {
        static void Main()
        {
            bool retry = true;

            while (retry)
            {
                Console.Clear();
                PlayMastermind();

                Console.WriteLine();
                Console.Write("Do you want to play again? (y/n): ");
                retry = (Console.ReadLine().ToLower() == "y");
            }
        }

        static void PlayMastermind()
        {
            Random random = new Random();
            int[] answer = new int[4];
            for (int i = 0; i < 4; i++)
            {
                answer[i] = random.Next(1, 7);
            }

            int attempts = 10;

            Console.WriteLine("Let's play Mastermind!");
            Console.WriteLine("Try to guess the 4-digit number. Each digit is between 1 and 6.");

            while (attempts > 0)
            {
                Console.WriteLine($"Attempts remaining: {attempts}");
                Console.Write("Enter your guess: ");
                string input = Console.ReadLine();

                int[] guess = new int[4];
                string validationMessage = Validate(input, guess);

                if (string.IsNullOrWhiteSpace(validationMessage))
                {
                    if (CheckGuess(answer, guess))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Congratulations! You guessed the number correctly.");
                    }
                }
                else
                {
                    Console.WriteLine(validationMessage);
                }

                attempts--;
            }

            Console.WriteLine();
            Console.WriteLine("You've run out of attempts. You lose!");
            Console.WriteLine("The correct number was: " + string.Join("", answer));
        }

        static string Validate(string input, int[] guess)
        {
            if (input.Length != 4 || !int.TryParse(input, out _))
            {
                return "Try again! The number is only 4 digits in length.";
            }

            for (int i = 0; i < 4; i++)
            {
                guess[i] = int.Parse(input[i].ToString());
                if (guess[i] < 1 || guess[i] > 6)
                {
                    return "Try again! Each digit must be between 1 and 6.";
                }
            }

            return string.Empty;
        }

        static bool CheckGuess(int[] answer, int[] guess)
        {
            int plusCount = 0;
            int minusCount = 0;
            bool[] answerUsed = new bool[4];
            bool[] guessUsed = new bool[4];

            for (int i = 0; i < 4; i++)
            {
                if (guess[i] == answer[i])
                {
                    plusCount++;
                    answerUsed[i] = true;
                    guessUsed[i] = true;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (guessUsed[i]) continue;
                for (int j = 0; j < 4; j++)
                {
                    if (!answerUsed[j] && guess[i] == answer[j])
                    {
                        minusCount++;
                        answerUsed[j] = true;
                        break;
                    }
                }
            }

            Console.WriteLine(new string('+', plusCount) + new string('-', minusCount));

            return plusCount == 4;
        }
    }

}
