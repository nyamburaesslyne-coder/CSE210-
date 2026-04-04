using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        // Exceeds requirements: Track the number of activities completed in this session
        int activitiesCompleted = 0;
        string choice = "";

        while (choice != "5")
        {
            Console.Clear();
            Console.WriteLine($"Mindfulness App - Activities Completed: {activitiesCompleted}");
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Start breathing activity");
            Console.WriteLine("  2. Start reflection activity");
            Console.WriteLine("  3. Start listing activity");
            Console.WriteLine("  4. Start nature connection activity"); // Exceeds requirements
            Console.WriteLine("  5. Quit");
            Console.Write("Select a choice from the menu: ");
            
            choice = Console.ReadLine();

            if (choice == "1")
            {
                BreathingActivity breathing = new BreathingActivity();
                breathing.Run();
                activitiesCompleted++;
            }
            else if (choice == "2")
            {
                ReflectionActivity reflection = new ReflectionActivity();
                reflection.Run();
                activitiesCompleted++;
            }
            else if (choice == "3")
            {
                ListingActivity listing = new ListingActivity();
                listing.Run();
                activitiesCompleted++;
            }
            else if (choice == "4")
            {
                NatureActivity nature = new NatureActivity();
                nature.Run();
                activitiesCompleted++;
            }
        }
    }
}

public class Activity
{
    protected string _name;
    protected string _description;
    protected int _duration;

    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
        _duration = 0;
    }

    public void DisplayStartingMessage()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {_name}.\n");
        Console.WriteLine($"{_description}\n");
        Console.Write("How long, in seconds, would you like for your session? ");
        
        string durationString = Console.ReadLine();
        _duration = int.Parse(durationString);

        Console.Clear();
        Console.WriteLine("Get ready...");
        ShowSpinner(5);
    }

    public void DisplayEndingMessage()
    {
        Console.WriteLine();
        Console.WriteLine("Well done!!");
        ShowSpinner(3);
        Console.WriteLine($"\nYou have completed another {_duration} seconds of the {_name}.");
        ShowSpinner(5);
    }

    public void ShowSpinner(int seconds)
    {
        List<string> animationStrings = new List<string> { "|", "/", "-", "\\" };
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(seconds);
        int i = 0;

        while (DateTime.Now < endTime)
        {
            string s = animationStrings[i];
            Console.Write(s);
            Thread.Sleep(250);
            Console.Write("\b \b");
            i++;

            if (i >= animationStrings.Count)
            {
                i = 0;
            }
        }
    }

    public void ShowCountDown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            
            // Handles backspacing gracefully even if the number is double digits
            int numLength = i.ToString().Length;
            Console.Write(new string('\b', numLength));
            Console.Write(new string(' ', numLength));
            Console.Write(new string('\b', numLength));
        }
    }
}

public class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing Activity", "This activity will help you relax by walking your through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    public void Run()
    {
        DisplayStartingMessage();

        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(_duration);

        while (DateTime.Now < endTime)
        {
            Console.Write("\nBreathe in...");
            ShowCountDown(4);
            Console.Write("\nNow breathe out...");
            ShowCountDown(6);
            Console.WriteLine();
        }

        DisplayEndingMessage();
    }
}

public class ReflectionActivity : Activity
{
    private List<string> _prompts;
    private List<string> _questions;
    private List<string> _availablePrompts;
    private List<string> _availableQuestions;
    private Random _random;

    public ReflectionActivity() : base("Reflection Activity", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    {
        _random = new Random();
        
        _prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        _questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        ResetAvailableItems();
    }

    private void ResetAvailableItems()
    {
        _availablePrompts = new List<string>(_prompts);
        _availableQuestions = new List<string>(_questions);
    }

    public string GetRandomPrompt()
    {
        // Exceeds requirements: Ensures prompts are not repeated until all are used
        if (_availablePrompts.Count == 0)
        {
            _availablePrompts = new List<string>(_prompts);
        }

        int index = _random.Next(_availablePrompts.Count);
        string prompt = _availablePrompts[index];
        _availablePrompts.RemoveAt(index);
        return prompt;
    }

    public string GetRandomQuestion()
    {
        // Exceeds requirements: Ensures questions are not repeated until all are used
        if (_availableQuestions.Count == 0)
        {
            _availableQuestions = new List<string>(_questions);
        }

        int index = _random.Next(_availableQuestions.Count);
        string question = _availableQuestions[index];
        _availableQuestions.RemoveAt(index);
        return question;
    }

    public void DisplayPrompt()
    {
        Console.WriteLine("Consider the following prompt:\n");
        Console.WriteLine($" --- {GetRandomPrompt()} --- \n");
        Console.WriteLine("When you have something in mind, press enter to continue.");
        Console.ReadLine();
        Console.WriteLine("Now ponder on each of the following questions as they related to this experience.");
        Console.Write("You may begin in: ");
        ShowCountDown(5);
        Console.Clear();
    }

    public void DisplayQuestions()
    {
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(_duration);

        while (DateTime.Now < endTime)
        {
            Console.Write($"> {GetRandomQuestion()} ");
            ShowSpinner(8); // Pauses to let them ponder
            Console.WriteLine();
        }
    }

    public void Run()
    {
        DisplayStartingMessage();
        DisplayPrompt();
        DisplayQuestions();
        DisplayEndingMessage();
    }
}

public class ListingActivity : Activity
{
    private int _count;
    private List<string> _prompts;
    private List<string> _availablePrompts;
    private Random _random;

    public ListingActivity() : base("Listing Activity", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
        _random = new Random();
        _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };
        _availablePrompts = new List<string>(_prompts);
    }

    public void GetRandomPrompt()
    {
        // Exceeds requirements: Ensures prompts are not repeated until all are used
        if (_availablePrompts.Count == 0)
        {
            _availablePrompts = new List<string>(_prompts);
        }

        int index = _random.Next(_availablePrompts.Count);
        string prompt = _availablePrompts[index];
        _availablePrompts.RemoveAt(index);

        Console.WriteLine("List as many responses you can to the following prompt:");
        Console.WriteLine($" --- {prompt} --- ");
    }

    public List<string> GetListFromUser()
    {
        List<string> userList = new List<string>();
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(_duration);

        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                userList.Add(input);
            }
        }

        return userList;
    }

    public void Run()
    {
        DisplayStartingMessage();
        GetRandomPrompt();
        
        Console.Write("You may begin in: ");
        ShowCountDown(5);
        Console.WriteLine();

        List<string> responses = GetListFromUser();
        _count = responses.Count;

        Console.WriteLine($"You listed {_count} items!");
        DisplayEndingMessage();
    }
}

public class NatureActivity : Activity
{
    private List<string> _environments;
    private Random _random;

    public NatureActivity() : base("Nature Connection Activity", "This activity guides you through a sensory visualization of a natural environment to ground your thoughts and connect you to the broader ecosystem.")
    {
        _random = new Random();
        _environments = new List<string>
        {
            "a dense, ancient forest with sunlight filtering through the canopy",
            "a quiet, rocky coastline with waves gently crashing",
            "a high mountain meadow filled with wildflowers and a cool breeze",
            "a vibrant coral reef teeming with silent, colorful life"
        };
    }

    private string GetRandomEnvironment()
    {
        int index = _random.Next(_environments.Count);
        return _environments[index];
    }

    public void Run()
    {
        DisplayStartingMessage();

        string environment = GetRandomEnvironment();
        Console.WriteLine($"Picture yourself standing in {environment}.");
        Console.Write("Close your eyes and visualize this space in: ");
        ShowCountDown(5);
        Console.Clear();

        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(_duration);

        // Sensory prompts for the visualization
        List<string> senses = new List<string>
        {
            "What sounds do you hear around you?",
            "Focus on the physical sensations. What does the air feel like?",
            "Look closely at the ground. What complex life is thriving there?",
            "Take a deep breath. What organic scents are present?",
            "Observe the relationship between the different elements here. How do they support each other?"
        };

        int senseIndex = 0;

        while (DateTime.Now < endTime)
        {
            Console.Write($"\n> {senses[senseIndex]} ");
            ShowSpinner(8);
            
            senseIndex++;
            if (senseIndex >= senses.Count)
            {
                senseIndex = 0;
            }
        }

        Console.WriteLine();
        DisplayEndingMessage();
    }
}