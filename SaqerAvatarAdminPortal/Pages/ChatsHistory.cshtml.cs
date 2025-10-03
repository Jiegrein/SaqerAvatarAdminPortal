using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

public class ChatsHistoryModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public DateTime? SelectedDate { get; set; } = DateTime.UtcNow;

    public TodayStats TodayStats { get; set; }
    public List<ChatData> AllChats { get; set; }
    public List<ChatData> FilteredChats { get; set; }

    [BindProperty(SupportsGet = true)]
    public string SearchQuery { get; set; }

    [BindProperty(SupportsGet = true)]
    public string SelectedCategory { get; set; }

    [BindProperty(SupportsGet = true)]
    public int Page { get; set; } = 1;

    public int TotalPages { get; set; }

    public List<string> Categories { get; set; } = new List<string>
    {
        "Flight Status", "Flight Booking", "At the Airport", "Vacation Planner",
        "Baggage", "Miles", "FAQ", "Booking", "Check-in", "Others"
    };

    private const int PageSize = 10;

    public void OnGet(bool setToday = false, bool setYesterday = false)
    {
        // Sample Data
        TodayStats = new TodayStats { TotalChats = 127, SuccessChats = 95, FailedChats = 32, AverageRating = 4.3 };
        AllChats = ChatSampleData.GetSampleChats();

        if (setToday)
            SelectedDate = DateTime.Today;

        if (setYesterday)
            SelectedDate = DateTime.Today.AddDays(-1);

        if (SelectedDate.HasValue)
            FilteredChats = AllChats.Where(c => DateTime.Parse(c.DateTime).Date == SelectedDate.Value.Date).ToList();
        else
            FilteredChats = AllChats;
    }
}

public class TodayStats
{
    public int TotalChats { get; set; }
    public int SuccessChats { get; set; }
    public int FailedChats { get; set; }
    public double AverageRating { get; set; }
}

public class ChatData
{
    public int Id { get; set; }
    public string DateTime { get; set; }
    public string Topic { get; set; }
    public string Summary { get; set; }
    public int Rating { get; set; }
    public bool IsFailedChat { get; set; }
    public string Category { get; set; }
}

public static class ChatSampleData
{
    public static List<ChatData> GetSampleChats()
    {
        return new List<ChatData>
        {
            new ChatData { Id = 101, DateTime = "2024-01-15 14:32", Topic = "Flight Status", Summary = "User inquired about flight AA123", Rating = 5, IsFailedChat = false, Category = "Flight Status" },
            new ChatData { Id = 102, DateTime = "2024-01-15 13:45", Topic = "Flight Booking", Summary = "User booked flight", Rating = 4, IsFailedChat = false, Category = "Flight Booking" }
            // Add more sample chats
        };
    }
}
