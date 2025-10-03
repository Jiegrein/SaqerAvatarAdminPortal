using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

public class IndexModel : PageModel
{
    public DashboardData DashboardData { get; set; }
    public string DashboardDataJson { get; set; }

    public DateTime DateFrom { get; set; } = new DateTime(2025, 9, 1);
    public DateTime DateTo { get; set; } = new DateTime(2025, 10, 2);

    public void OnGet()
    {
        DashboardData = new DashboardData
        {
            Stats = new Stats
            {
                TotalChats = 2847,
                SuccessChats = 2134,
                FailedChats = 713,
                AgentChats = 456
            },
            Categories = new Dictionary<string, int>
                {
                    { "Flight Status", 687 },
                    { "Flight Booking", 542 },
                    { "At the Airport", 398 },
                    { "Vacation Planner", 321 },
                    { "Baggage", 289 },
                    { "Miles", 234 },
                    { "FAQ", 198 },
                    { "Booking", 156 },
                    { "Check-in", 89 },
                    { "Others", 33 }
                },
            DailyChats = new DailyChats
            {
                Labels = new List<string> { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" },
                Data = new List<int> { 285, 342, 398, 456, 523, 389, 454 }
            },
            SuccessFailedDaily = new SuccessFailedDaily
            {
                Labels = new List<string> { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" },
                Success = new List<int> { 215, 289, 321, 378, 423, 312, 376 },
                Failed = new List<int> { 70, 53, 77, 78, 100, 77, 78 }
            },
            RatingsDaily = new RatingsDaily
            {
                Labels = new List<string> { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" },
                Data = new List<double> { 4.2, 4.5, 4.1, 4.3, 4.6, 4.4, 4.5 }
            },
            RecentChats = new List<Chat>
                {
                    new Chat
                    {
                        Id = 1,
                        DateTime = "2024-01-15 14:32",
                        Topic = "Flight Status",
                        Summary = "User inquired about flight AA123 delay status",
                        Rating = 5,
                        IsFailedChat = false,
                        Messages = new List<Message>
                        {
                            new Message { Type="user", Content="Hi, can you check the status of flight AA123?", Timestamp="14:32" },
                            new Message { Type="assistant", Content="Hello! I'd be happy to help you check the status of flight AA123. Let me look that up for you.", Timestamp="14:32" },
                            new Message { Type="assistant", Content="Flight AA123 from New York to Los Angeles is currently delayed by 45 minutes due to weather conditions. The new estimated departure time is 3:45 PM.", Timestamp="14:33" },
                            new Message { Type="user", Content="Thank you for the update. Will this affect my connecting flight?", Timestamp="14:33" },
                            new Message { Type="assistant", Content="I can help you check your connecting flight. Could you please provide me with your connecting flight number?", Timestamp="14:34" },
                            new Message { Type="user", Content="It's flight AA456 to Chicago", Timestamp="14:34" },
                            new Message { Type="assistant", Content="Good news! Flight AA456 to Chicago departs at 6:30 PM, so you should have enough time to make your connection even with the delay.", Timestamp="14:35" }
                        }
                    },
                    new Chat
                    {
                        Id = 2,
                        DateTime = "2024-01-15 13:45",
                        Topic = "Flight Booking",
                        Summary = "User successfully booked a round-trip flight to Miami",
                        Rating = 4,
                        IsFailedChat = false,
                        Messages = new List<Message>
                        {
                            new Message { Type="user", Content="I need to book a flight to Miami for next week", Timestamp="13:45" },
                            new Message { Type="assistant", Content="I'd be happy to help you book a flight to Miami! Could you please tell me your departure city and preferred travel dates?", Timestamp="13:45" },
                            new Message { Type="user", Content="From Boston, departing March 15th and returning March 22nd", Timestamp="13:46" },
                            new Message { Type="assistant", Content="Perfect! I found several options for Boston to Miami. Here are the best available flights for your dates...", Timestamp="13:46" }
                        }
                    },
                    new Chat
                    {
                        Id = 3,
                        DateTime = "2024-01-15 12:18",
                        Topic = "Baggage",
                        Summary = "User reported lost baggage, case created",
                        Rating = 3,
                        IsFailedChat = false,
                        Messages = new List<Message>
                        {
                            new Message { Type="user", Content="My baggage didn't arrive with my flight", Timestamp="12:18" },
                            new Message { Type="assistant", Content="I'm sorry to hear about your missing baggage. I can help you report this and track it down. Can you provide me with your flight details?", Timestamp="12:18" },
                            new Message { Type="user", Content="Flight BA789 from London to New York yesterday", Timestamp="12:19" },
                            new Message { Type="assistant", Content="Thank you. I've created a baggage claim case for you. Your reference number is BG123456. We'll track your baggage and contact you as soon as we locate it.", Timestamp="12:20" }
                        }
                    },
                    new Chat
                    {
                        Id = 4,
                        DateTime = "2024-01-15 11:22",
                        Topic = "Check-in",
                        Summary = "Technical issue prevented online check-in completion",
                        Rating = 2,
                        IsFailedChat = true,
                        Messages = new List<Message>
                        {
                            new Message { Type="user", Content="I'm trying to check in online but getting an error", Timestamp="11:22" },
                            new Message { Type="assistant", Content="I'm sorry you're experiencing issues with online check-in. Can you tell me what error message you're seeing?", Timestamp="11:22" },
                            new Message { Type="user", Content="It says \"System temporarily unavailable\"", Timestamp="11:23" },
                            new Message { Type="assistant", Content="I apologize for the technical difficulties. Unfortunately, I'm unable to complete your check-in through this system. Please visit the airport check-in counter or try again later.", Timestamp="11:24" }
                        }
                    },
                    new Chat
                    {
                        Id = 5,
                        DateTime = "2024-01-15 10:15",
                        Topic = "Miles",
                        Summary = "User inquired about miles balance and redemption",
                        Rating = 5,
                        IsFailedChat = false,
                        Messages = new List<Message>
                        {
                            new Message { Type="user", Content="How many miles do I have in my account?", Timestamp="10:15" },
                            new Message { Type="assistant", Content="I can help you check your miles balance. Your current balance is 45,230 miles. Would you like to know about redemption options?", Timestamp="10:15" },
                            new Message { Type="user", Content="Yes, what can I get with those miles?", Timestamp="10:16" },
                            new Message { Type="assistant", Content="With 45,230 miles, you can redeem for a domestic round-trip flight (25,000 miles) or upgrade to business class on your next international flight (40,000 miles).", Timestamp="10:16" }
                        }
                    }
                }
        };
    }
}

public class ChatViewModel
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Topic { get; set; }
    public string Summary { get; set; }
    public int Rating { get; set; }
    public string Status { get; set; }
}


public class DashboardData
{
    public Stats Stats { get; set; }
    public Dictionary<string, int> Categories { get; set; }
    public DailyChats DailyChats { get; set; }
    public SuccessFailedDaily SuccessFailedDaily { get; set; }
    public RatingsDaily RatingsDaily { get; set; }
    public List<Chat> RecentChats { get; set; }
}

public class Stats
{
    public int TotalChats { get; set; }
    public int SuccessChats { get; set; }
    public int FailedChats { get; set; }
    public int AgentChats { get; set; }
}

public class DailyChats
{
    public List<string> Labels { get; set; }
    public List<int> Data { get; set; }
}

public class SuccessFailedDaily
{
    public List<string> Labels { get; set; }
    public List<int> Success { get; set; }
    public List<int> Failed { get; set; }
}

public class RatingsDaily
{
    public List<string> Labels { get; set; }
    public List<double> Data { get; set; }
}

public class Chat
{
    public int Id { get; set; }
    public string DateTime { get; set; }
    public string Topic { get; set; }
    public string Summary { get; set; }
    public int Rating { get; set; }
    public bool IsFailedChat { get; set; }
    public List<Message> Messages { get; set; }
}

public class Message
{
    public string Type { get; set; }
    public string Content { get; set; }
    public string Timestamp { get; set; }
}

