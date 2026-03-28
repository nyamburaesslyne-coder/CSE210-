using System;
using System.Collections.Generic;

// ---------------------------------------------------------------------------
// Comment.cs
// ---------------------------------------------------------------------------
public class Comment
{
    private string _name;
    private string _text;

    public Comment(string name, string text)
    {
        _name = name;
        _text = text;
    }

    public string GetName()
    {
        return _name;
    }

    public string GetText()
    {
        return _text;
    }
}

// ---------------------------------------------------------------------------
// Video.cs
// ---------------------------------------------------------------------------
public class Video
{
    private string _title;
    private string _author;
    private int _lengthInSeconds;
    private List<Comment> _comments;

    public Video(string title, string author, int length)
    {
        _title = title;
        _author = author;
        _lengthInSeconds = length;
        _comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    // Abstraction: The caller doesn't need to know we use a List.
    // They just call this method to get the count.
    public int GetCommentCount()
    {
        return _comments.Count;
    }

    public List<Comment> GetComments()
    {
        return _comments;
    }

    public string GetVideoTitle() => _title;
    public string GetVideoAuthor() => _author;
    public int GetVideoLength() => _lengthInSeconds;
}

// ---------------------------------------------------------------------------
// Program.cs
// ---------------------------------------------------------------------------
class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        // Video 1: Programming Context
        Video video1 = new Video("C# Classes and Objects", "TechWithTim", 720);
        video1.AddComment(new Comment("User123", "Finally, a clear explanation of abstraction!"));
        video1.AddComment(new Comment("CodeNewbie", "This helped me with my homework."));
        video1.AddComment(new Comment("DevGuru", "Great use of private member variables."));
        videos.Add(video1);

        // Video 2: Environmental Context
        Video video2 = new Video("Restoring Kenya's Biodiversity", "EcoAction", 1500);
        video2.AddComment(new Comment("GreenStudent", "This is so relevant to my studies in Kenya."));
        video2.AddComment(new Comment("NatureLover", "The footage of the conservancies was beautiful."));
        video2.AddComment(new Comment("EcoAdvocate", "We need more science-backed NGOs like this."));
        videos.Add(video2);

        // Video 3: General Interest
        Video video3 = new Video("Top 10 National Parks", "WorldTraveler", 600);
        video3.AddComment(new Comment("TravelBug", "I want to visit the Maasai Mara next year!"));
        video3.AddComment(new Comment("HikerDan", "Great list, but you forgot Zion."));
        video3.AddComment(new Comment("PassportPolly", "Subscribed! Thanks for the tips."));
        video3.AddComment(new Comment("GlobalCitizen", "The commentary was excellent."));
        videos.Add(video3);

        // Displaying the information
        foreach (Video video in videos)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"Title: {video.GetVideoTitle()}");
            Console.WriteLine($"Author: {video.GetVideoAuthor()}");
            Console.WriteLine($"Length: {video.GetVideoLength()} seconds");
            Console.WriteLine($"Number of Comments: {video.GetCommentCount()}");
            Console.WriteLine("Comments:");

            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($"- {comment.GetName()}: {comment.GetText()}");
            }
            Console.WriteLine();
        }
    }
}