using System;
class Program
{
    static void Main(string[] args)
    {
        // Stretch Challenge: Loop to play the whole game again
        string playAgain = "yes";

        while (playAgain.ToLower() == "yes")
        {
            // Core Requirement 3: Generate a random magic number
            Random randomGenerator = new Random();
            int magicNumber = randomGenerator.Next(1, 101);

            int guess = -1;
            int guessCount = 0;

            Console.WriteLine("I have picked a magic number between 1 and 100.");

            // Core Requirement 2: Loop until the guess matches the magic number
            while (guess != magicNumber)
            {
                Console.Write("What is your guess? ");
                // Convert string input to integer
                guess = int.Parse(Console.ReadLine());
                guessCount++;

                // Core Requirement 1: If statements for higher/lower
                if (magicNumber > guess)
                {
                    Console.WriteLine("Higher");
                }
                else if (magicNumber < guess)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("You guessed it!");
                }
            }

            // Stretch Challenge: Display guess count
            Console.WriteLine($"It took you {guessCount} guesses.");

            // Stretch Challenge: Ask to play again
            Console.Write("Do you want to play again (yes/no)? ");
            playAgain = Console.ReadLine();
        }

        Console.WriteLine("Thanks for playing!");
    }
}