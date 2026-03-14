using System;
using System.Collections.Generic;
using System.IO;

// EXCEEDING REQUIREMENTS:
// 1. Added a "Mood" tracker: When adding a new entry, the user is prompted to record their current mood, which is saved alongside the entry.
// 2. Exact Timestamps: Instead of just saving the date, the program captures the exact time (HH:mm) the entry was written.
// 3. Graceful Error Handling: When loading a file, the program checks if the file actually exists before attempting to read it, preventing a crash.

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();
        bool isRunning = true;

        Console.WriteLine("Welcome to the Journal Program!");

        while (isRunning)
        {
            Console.WriteLine("\nPlease select one of the following choices:");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Load");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Quit");
            Console.Write("What would you like to do? ");
            
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    string prompt = promptGenerator.GetRandomPrompt();
                    Console.WriteLine($"\nPrompt: {prompt}");
                    Console.Write("> ");
                    string response = Console.ReadLine();

                    Console.Write("How are you feeling right now? (Mood): ");
                    string mood = Console.ReadLine();

                    Entry newEntry = new Entry();
                    newEntry._date = DateTime.Now.ToString("MM/dd/yyyy HH:mm"); 
                    newEntry._promptText = prompt;
                    newEntry._entryText = response;
                    newEntry._mood = mood;

                    journal.AddEntry(newEntry);
                    break;

                case "2":
                    Console.WriteLine("\n--- Your Journal Entries ---");
                    journal.DisplayAll();
                    break;

                case "3":
                    Console.Write("\nWhat is the filename to load? ");
                    string loadFile = Console.ReadLine();
                    journal.LoadFromFile(loadFile);
                    break;

                case "4":
                    Console.Write("\nWhat is the filename to save as? ");
                    string saveFile = Console.ReadLine();
                    journal.SaveToFile(saveFile);
                    break;

                case "5":
                    isRunning = false;
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid option. Please choose a number between 1 and 5.");
                    break;
            }
        }
    }
}

public class Journal
{
    public List<Entry> _entries;

    public Journal()
    {
        _entries = new List<Entry>();
    }

    public void AddEntry(Entry newEntry)
    {
        _entries.Add(newEntry);
    }

    public void DisplayAll()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("The journal is currently empty.");
            return;
        }

        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }

    public void SaveToFile(string file)
    {
        using (StreamWriter outputFile = new StreamWriter(file))
        {
            foreach (Entry entry in _entries)
            {
                // Using "~~" as a unique separator to avoid issues with commas in standard sentences
                outputFile.WriteLine($"{entry._date}~~{entry._promptText}~~{entry._mood}~~{entry._entryText}");
            }
        }
        Console.WriteLine("Journal saved successfully.");
    }

    public void LoadFromFile(string file)
    {
        if (!File.Exists(file))
        {
            Console.WriteLine("Error: File not found. Please check the name and try again.");
            return;
        }

        _entries.Clear(); // Clears current entries to replace with the loaded ones
        string[] lines = File.ReadAllLines(file);

        foreach (string line in lines)
        {
            string[] parts = line.Split("~~");
            
            // Ensure the line was split correctly before trying to build an entry
            if (parts.Length == 4)
            {
                Entry loadedEntry = new Entry();
                loadedEntry._date = parts[0];
                loadedEntry._promptText = parts[1];
                loadedEntry._mood = parts[2];
                loadedEntry._entryText = parts[3];
                
                _entries.Add(loadedEntry);
            }
        }
        Console.WriteLine("Journal loaded successfully.");
    }
}

public class Entry
{
    public string _date;
    public string _promptText;
    public string _entryText;
    public string _mood; 

    public void Display()
    {
        Console.WriteLine($"Date: {_date} - Prompt: {_promptText}");
        Console.WriteLine($"Mood: {_mood}");
        Console.WriteLine($"{_entryText}\n");
    }
}

public class PromptGenerator
{
    public List<string> _prompts;

    public PromptGenerator()
    {
        _prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?",
            "What is a new idea I had today?",
            "What made me smile today?"
        };
    }

    public string GetRandomPrompt()
    {
        Random random = new Random();
        int index = random.Next(_prompts.Count);
        return _prompts[index];
    }
}