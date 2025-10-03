using SaqerAvatarAdminPortal.Models.Chat;
using SaqerAvatarAdminPortal.Services.Interfaces;

namespace SaqerAvatarAdminPortal.Services;

public class ChatService : IChatService
{
    private readonly ILogger<ChatService> _logger;
    
    // Centralized sample data - single source of truth
    private static readonly List<ChatDto> _sampleChats = new()
    {
        new() 
        { 
            Id = 1, 
            DateTime = DateTime.Today.AddHours(-2).AddMinutes(-30), 
            Topic = "Flight Status", 
            Summary = "User inquired about flight AA123 delay status", 
            Rating = 5, 
            IsFailedChat = false, 
            Category = "Flight Status"
        },
        new() 
        { 
            Id = 2, 
            DateTime = DateTime.Today.AddHours(-3).AddMinutes(-15), 
            Topic = "Flight Booking", 
            Summary = "User successfully booked a round-trip flight to Miami", 
            Rating = 4, 
            IsFailedChat = false, 
            Category = "Flight Booking"
        },
        new() 
        { 
            Id = 3, 
            DateTime = DateTime.Today.AddHours(-4), 
            Topic = "Check-in", 
            Summary = "Technical issue prevented online check-in completion", 
            Rating = 2, 
            IsFailedChat = true, 
            Category = "Check-in"
        },
        new() 
        { 
            Id = 4, 
            DateTime = DateTime.Today.AddHours(-5), 
            Topic = "Baggage", 
            Summary = "User reported lost baggage, case created", 
            Rating = 3, 
            IsFailedChat = false, 
            Category = "Baggage"
        },
        new() 
        { 
            Id = 5, 
            DateTime = DateTime.Today.AddHours(-6), 
            Topic = "Miles", 
            Summary = "User inquired about miles balance and redemption", 
            Rating = 5, 
            IsFailedChat = false, 
            Category = "Miles"
        },
        new() 
        { 
            Id = 6, 
            DateTime = DateTime.Today.AddHours(-7), 
            Topic = "At the Airport", 
            Summary = "User asked for directions to gate and amenities", 
            Rating = 4, 
            IsFailedChat = false, 
            Category = "At the Airport"
        },
        new() 
        { 
            Id = 7, 
            DateTime = DateTime.Today.AddHours(-8), 
            Topic = "Vacation Planner", 
            Summary = "User planned a vacation package to Hawaii", 
            Rating = 5, 
            IsFailedChat = false, 
            Category = "Vacation Planner"
        },
        // Yesterday's data
        new() 
        { 
            Id = 8, 
            DateTime = DateTime.Today.AddDays(-1).AddHours(-5), 
            Topic = "Flight Status", 
            Summary = "User checked flight status for international flight", 
            Rating = 4, 
            IsFailedChat = false, 
            Category = "Flight Status"
        },
        new() 
        { 
            Id = 9, 
            DateTime = DateTime.Today.AddDays(-1).AddHours(-6), 
            Topic = "FAQ", 
            Summary = "User asked about baggage allowance policies", 
            Rating = 3, 
            IsFailedChat = false, 
            Category = "FAQ"
        },
        new() 
        { 
            Id = 10, 
            DateTime = DateTime.Today.AddDays(-1).AddHours(-7), 
            Topic = "Booking", 
            Summary = "User modified existing booking dates", 
            Rating = 4, 
            IsFailedChat = false, 
            Category = "Booking"
        },
        // Additional variety data for past 7 days
        new() 
        { 
            Id = 11, 
            DateTime = DateTime.Today.AddDays(-2).AddHours(-8), 
            Topic = "Flight Booking", 
            Summary = "User booked last-minute business trip to Chicago", 
            Rating = 5, 
            IsFailedChat = false, 
            Category = "Flight Booking"
        },
        new() 
        { 
            Id = 12, 
            DateTime = DateTime.Today.AddDays(-3).AddHours(-9), 
            Topic = "Others", 
            Summary = "User inquired about pet travel policies", 
            Rating = 3, 
            IsFailedChat = false, 
            Category = "Others"
        },
        new() 
        { 
            Id = 13, 
            DateTime = DateTime.Today.AddDays(-4).AddHours(-10), 
            Topic = "Miles", 
            Summary = "User redeemed miles for upgrade", 
            Rating = 5, 
            IsFailedChat = false, 
            Category = "Miles"
        },
        new() 
        { 
            Id = 14, 
            DateTime = DateTime.Today.AddDays(-5).AddHours(-11), 
            Topic = "Check-in", 
            Summary = "User successfully checked in online", 
            Rating = 4, 
            IsFailedChat = false, 
            Category = "Check-in"
        },
        new() 
        { 
            Id = 15, 
            DateTime = DateTime.Today.AddDays(-6).AddHours(-12), 
            Topic = "Baggage", 
            Summary = "User reported damaged luggage", 
            Rating = 2, 
            IsFailedChat = true, 
            Category = "Baggage"
        }
    };

    public ChatService(ILogger<ChatService> logger)
    {
        _logger = logger;
    }

    public async Task<List<ChatDto>> GetAllChatsAsync()
    {
        _logger.LogInformation("Getting all chat data from centralized source");
        return await Task.FromResult(_sampleChats.ToList());
    }

    public async Task<IQueryable<ChatDto>> GetChatsInDateRangeAsync(DateTime dateFrom, DateTime dateTo)
    {
        _logger.LogInformation("Getting chats in date range: {DateFrom} to {DateTo}", dateFrom, dateTo);
        
        var chats = _sampleChats
            .Where(c => c.DateTime.Date >= dateFrom.Date && c.DateTime.Date <= dateTo.Date)
            .AsQueryable();

        return await Task.FromResult(chats);
    }

    public async Task<ChatDetailsDto?> GetChatDetailsAsync(int chatId)
    {
        _logger.LogInformation("Getting chat details for ID: {ChatId}", chatId);

        var chat = _sampleChats.FirstOrDefault(c => c.Id == chatId);
        if (chat == null)
        {
            _logger.LogWarning("Chat not found for ID: {ChatId}", chatId);
            return null;
        }

        return await Task.FromResult(new ChatDetailsDto
        {
            Id = chat.Id,
            DateTime = chat.DateTime,
            Topic = chat.Topic,
            Rating = chat.Rating,
            Messages = GetSampleMessages(chatId)
        });
    }

    public async Task<IQueryable<ChatDto>> GetFilteredChatsAsync(DateTime selectedDate, string? searchQuery = null, string? category = null)
    {
        _logger.LogInformation("Getting filtered chats for date: {Date}, search: {Search}, category: {Category}", 
            selectedDate.Date, searchQuery, category);

        var chats = _sampleChats
            .Where(c => c.DateTime.Date == selectedDate.Date)
            .AsQueryable();

        // Apply search filter
        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            var searchLower = searchQuery.ToLower();
            chats = chats.Where(c => 
                c.Topic.ToLower().Contains(searchLower) ||
                c.Summary.ToLower().Contains(searchLower));
        }

        // Apply category filter
        if (!string.IsNullOrWhiteSpace(category))
        {
            chats = chats.Where(c => c.Category == category);
        }

        return await Task.FromResult(chats);
    }

    public async Task<List<ChatDto>> GetRecentTopChatsAsync(DateTime dateFrom, DateTime dateTo, int count = 5)
    {
        _logger.LogInformation("Getting recent top chats from {DateFrom} to {DateTo}, count: {Count}", 
            dateFrom, dateTo, count);

        var recentChats = _sampleChats
            .Where(c => c.DateTime.Date >= dateFrom.Date && c.DateTime.Date <= dateTo.Date)
            .OrderByDescending(c => c.DateTime)
            .Take(count)
            .ToList();

        return await Task.FromResult(recentChats);
    }

    public async Task<ChatStatsDto> GetChatStatsAsync(DateTime dateFrom, DateTime dateTo)
    {
        _logger.LogInformation("Getting chat stats from {DateFrom} to {DateTo}", dateFrom, dateTo);

        var chats = _sampleChats
            .Where(c => c.DateTime.Date >= dateFrom.Date && c.DateTime.Date <= dateTo.Date)
            .ToList();

        var stats = new ChatStatsDto
        {
            TotalChats = chats.Count,
            SuccessChats = chats.Count(c => !c.IsFailedChat),
            FailedChats = chats.Count(c => c.IsFailedChat),
            AverageRating = chats.Any() ? Math.Round(chats.Average(c => c.Rating), 1) : 0
        };

        return await Task.FromResult(stats);
    }

    public List<string> GetChatCategories()
    {
        return new List<string>
        {
            "Flight Status", "Flight Booking", "At the Airport", "Vacation Planner",
            "Baggage", "Miles", "FAQ", "Booking", "Check-in", "Others"
        };
    }

    private static List<ChatMessageDto> GetSampleMessages(int chatId)
    {
        return chatId switch
        {
            1 => new List<ChatMessageDto>
            {
                new() { Type = "user", Content = "Hi, can you check the status of flight AA123?", Timestamp = "14:32" },
                new() { Type = "assistant", Content = "Hello! I'd be happy to help you check the status of flight AA123. Let me look that up for you.", Timestamp = "14:32" },
                new() { Type = "assistant", Content = "Flight AA123 from New York to Los Angeles is currently delayed by 45 minutes due to weather conditions. The new estimated departure time is 3:45 PM.", Timestamp = "14:33" },
                new() { Type = "user", Content = "Thank you for the update. Will this affect my connecting flight?", Timestamp = "14:33" },
                new() { Type = "assistant", Content = "I can help you check your connecting flight. Could you please provide me with your connecting flight number?", Timestamp = "14:34" },
                new() { Type = "user", Content = "It's flight AA456 to Chicago", Timestamp = "14:34" },
                new() { Type = "assistant", Content = "Good news! Flight AA456 to Chicago departs at 6:30 PM, so you should have enough time to make your connection even with the delay.", Timestamp = "14:35" }
            },
            2 => new List<ChatMessageDto>
            {
                new() { Type = "user", Content = "I need to book a flight to Miami for next week", Timestamp = "13:45" },
                new() { Type = "assistant", Content = "I'd be happy to help you book a flight to Miami! Could you please tell me your departure city and preferred travel dates?", Timestamp = "13:45" },
                new() { Type = "user", Content = "From Boston, departing March 15th and returning March 22nd", Timestamp = "13:46" },
                new() { Type = "assistant", Content = "Perfect! I found several options for Boston to Miami. Here are the best available flights for your dates...", Timestamp = "13:46" }
            },
            3 => new List<ChatMessageDto>
            {
                new() { Type = "user", Content = "I'm trying to check in online but getting an error", Timestamp = "11:22" },
                new() { Type = "assistant", Content = "I'm sorry you're experiencing issues with online check-in. Can you tell me what error message you're seeing?", Timestamp = "11:22" },
                new() { Type = "user", Content = "It says \"System temporarily unavailable\"", Timestamp = "11:23" },
                new() { Type = "assistant", Content = "I apologize for the technical difficulties. Unfortunately, I'm unable to complete your check-in through this system. Please visit the airport check-in counter or try again later.", Timestamp = "11:24" }
            },
            4 => new List<ChatMessageDto>
            {
                new() { Type = "user", Content = "My baggage didn't arrive with my flight", Timestamp = "12:18" },
                new() { Type = "assistant", Content = "I'm sorry to hear about your missing baggage. I can help you report this and track it down. Can you provide me with your flight details?", Timestamp = "12:18" },
                new() { Type = "user", Content = "Flight BA789 from London to New York yesterday", Timestamp = "12:19" },
                new() { Type = "assistant", Content = "Thank you. I've created a baggage claim case for you. Your reference number is BG123456. We'll track your baggage and contact you as soon as we locate it.", Timestamp = "12:20" }
            },
            5 => new List<ChatMessageDto>
            {
                new() { Type = "user", Content = "How many miles do I have in my account?", Timestamp = "10:15" },
                new() { Type = "assistant", Content = "I can help you check your miles balance. Your current balance is 45,230 miles. Would you like to know about redemption options?", Timestamp = "10:15" },
                new() { Type = "user", Content = "Yes, what can I get with those miles?", Timestamp = "10:16" },
                new() { Type = "assistant", Content = "With 45,230 miles, you can redeem for a domestic round-trip flight (25,000 miles) or upgrade to business class on your next international flight (40,000 miles).", Timestamp = "10:16" }
            },
            6 => new List<ChatMessageDto>
            {
                new() { Type = "user", Content = "I'm at JFK airport, how do I get to gate B12?", Timestamp = "09:30" },
                new() { Type = "assistant", Content = "From your current location, take the AirTrain to Terminal 5, then follow signs to gates B10-B15. It should take about 10 minutes.", Timestamp = "09:30" },
                new() { Type = "user", Content = "Are there any restaurants near that gate?", Timestamp = "09:31" },
                new() { Type = "assistant", Content = "Yes! Near gate B12 you'll find Shake Shack, Starbucks, and a Hudson newsstand with snacks.", Timestamp = "09:31" }
            },
            7 => new List<ChatMessageDto>
            {
                new() { Type = "user", Content = "I want to plan a vacation to Hawaii", Timestamp = "08:45" },
                new() { Type = "assistant", Content = "Hawaii sounds wonderful! What time of year are you thinking of traveling, and how many people will be going?", Timestamp = "08:45" },
                new() { Type = "user", Content = "Two people, sometime in April", Timestamp = "08:46" },
                new() { Type = "assistant", Content = "April is a great time to visit Hawaii! I can help you find flight and hotel packages. Would you prefer Oahu, Maui, or the Big Island?", Timestamp = "08:46" }
            },
            _ => new List<ChatMessageDto>
            {
                new() { Type = "assistant", Content = "Sample chat message for demo purposes.", Timestamp = "10:00" },
                new() { Type = "user", Content = "Thank you for the assistance!", Timestamp = "10:01" }
            }
        };
    }
}