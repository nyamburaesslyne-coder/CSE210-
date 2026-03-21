using System;
using System.Collections.Generic;

// 1. Scripture Library: The program loads a list of multiple scriptures and selects one 
//    at random to present to the user, providing a varied experience.
// 2. Smart Hiding Algorithm: The Scripture.HideRandomWords method was designed to 
//    only select from words that are not already hidden. This solves the stretch challenge 
//    and provides a better user experience by ensuring steady progression.

public class Word
{
    private string _text;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    public void Hide()
    {
        _isHidden = true;
    }

    public void Show()
    {
        _isHidden = false;
    }

    public bool IsHidden()
    {
        return _isHidden;
    }

    public string GetDisplayText()
    {
        if (_isHidden)
        {
            // Creates a string of underscores exactly the same length as the word
            return new string('_', _text.Length);
        }
        else
        {
            return _text;
        }
    }
}

public class Reference
{
    private string _book;
    private int _chapter;
    private int _verse;
    private int _endVerse;

    // Constructor for a single verse
    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _verse = verse;
        _endVerse = verse;
    }

    // Constructor for multiple verses
    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _verse = startVerse;
        _endVerse = endVerse;
    }

    public string GetDisplayText()
    {
        if (_verse == _endVerse)
        {
            return $"{_book} {_chapter}:{_verse}";
        }
        else
        {
            return $"{_book} {_chapter}:{_verse}-{_endVerse}";
        }
    }
}

public class Scripture
{
    private Reference _reference;
    
    private List<Word> _words;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();
        
        // Split the scripture text by spaces and create Word objects
        string[] splitText = text.Split(' ');
        foreach (string word in splitText)
        {
            _words.Add(new Word(word));
        }
    }

    public void HideRandomWords(int numberToHide)
    {
        Random random = new Random();
        
        // Filter to only words that are NOT hidden yet
        List<Word> unhiddenWords = _words.FindAll(w => !w.IsHidden());

        // If there are fewer unhidden words left than requested, just hide the rest
        if (unhiddenWords.Count <= numberToHide)
        {
            foreach (Word word in unhiddenWords)
            {
                word.Hide();
            }
            return;
        }

        // Otherwise, randomly pick unhidden words to hide
        int hiddenCount = 0;
        while (hiddenCount < numberToHide)
        {
            int index = random.Next(_words.Count);
            if (!_words[index].IsHidden())
            {
                _words[index].Hide();
                hiddenCount++;
            }
        }
    }

    public string GetDisplayText()
    {
        string scriptureText = "";
        foreach (Word word in _words)
        {
            scriptureText += word.GetDisplayText() + " ";
        }
        
        return $"{_reference.GetDisplayText()} {scriptureText.TrimEnd()}";
    }

    public bool IsCompletelyHidden()
    {
        foreach (Word word in _words)
        {
            if (!word.IsHidden())
            {
                return false;
            }
        }
        return true;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Set up the library of scriptures
        List<Scripture> library = new List<Scripture>
        {
            new Scripture(new Reference("John", 3, 16), "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life."),
            new Scripture(new Reference("Proverbs", 3, 5, 6), "Trust in the Lord with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths."),
            new Scripture(new Reference("Philippians", 4, 13), "I can do all things through Christ which strengtheneth me.")
        };

        // Select a random scripture from the library
        Random random = new Random();
        int index = random.Next(library.Count);
        Scripture currentScripture = library[index];

        string userInput = "";

        // Main program loop
        while (userInput != "quit" && !currentScripture.IsCompletelyHidden())
        {
            Console.Clear();
            Console.WriteLine(currentScripture.GetDisplayText());
            Console.WriteLine();
            Console.WriteLine("Press enter to continue or type 'quit' to finish:");
            
            userInput = Console.ReadLine();

            if (userInput != "quit")
            {
                // Hide 3 words at a time
                currentScripture.HideRandomWords(3);
            }
        }

        // Final display to show all underscores before exiting
        if (currentScripture.IsCompletelyHidden())
        {
            Console.Clear();
            Console.WriteLine(currentScripture.GetDisplayText());
            Console.WriteLine("\nAll words hidden. Memorization complete!");
        }
    }
}