using System;

class Program
{
    static void Main(string[] args)
    {
        // 1. Call DisplayWelcome
        DisplayWelcome();

        // 2. Call PromptUserName and save the return value
        string userName = PromptUserName();

        // 3. Call PromptUserNumber and save the return value
        int favoriteNumber = PromptUserNumber();

        // 4. Call SquareNumber, passing the favorite number as a parameter
        int squaredNumber = SquareNumber(favoriteNumber);

        // 5. Call DisplayResult, passing the name and the squared result
        DisplayResult(userName, squaredNumber);
    }

    // Displays the welcome message
    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the program!");
    }

    // Asks for and returns the user's name
    static string PromptUserName()
    {
        Console.Write("Please enter your name: ");
        string name = Console.ReadLine();
        return name;
    }

    // Asks for and returns the user's favorite number
    static int PromptUserNumber()
    {
        Console.Write("Please enter your favorite number: ");
        int number = int.Parse(Console.ReadLine());
        return number;
    }

    // Accepts an integer and returns that number squared
    static int SquareNumber(int number)
    {
        int square = number * number;
        return square;
    }

    // Displays the final message
    static void DisplayResult(string name, int square)
    {
        Console.WriteLine($"{name}, the square of your number is {square}");
    }
}