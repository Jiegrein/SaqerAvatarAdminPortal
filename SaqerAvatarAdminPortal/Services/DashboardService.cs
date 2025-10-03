using SaqerAvatarAdminPortal.Models.Dashboard;
using SaqerAvatarAdminPortal.Models.Chat;
using SaqerAvatarAdminPortal.Services.Interfaces;
using SaqerAvatarAdminPortal.Exceptions;
using Microsoft.Extensions.Options;

namespace SaqerAvatarAdminPortal.Services;

public class DashboardService : IDashboardService
{
    private readonly IChatService _chatService;
    private readonly ILogger<DashboardService> _logger;
    private readonly DashboardSettings _settings;

    public DashboardService(IChatService chatService, ILogger<DashboardService> logger, IOptions<DashboardSettings> settings)
    {
        _chatService = chatService;
        _logger = logger;
        _settings = settings.Value;
    }

    public async Task<DashboardData> GetDashboardDataAsync(DateTime? fromDate = null, DateTime? toDate = null)
    {
        try
        {
            _logger.LogInformation("Fetching dashboard data for date range: {FromDate} to {ToDate}", 
                fromDate?.ToString("yyyy-MM-dd") ?? "null", 
                toDate?.ToString("yyyy-MM-dd") ?? "null");

            // Validate and set default date range if not provided
            var validatedDates = ValidateDateRange(fromDate, toDate);

            // Fetch all data concurrently for better performance
            var statsTask = GetStatsAsync(validatedDates.FromDate, validatedDates.ToDate);
            var categoriesTask = GetCategoriesAsync();
            var dailyChatsTask = GetDailyChatsAsync(validatedDates.FromDate, validatedDates.ToDate);
            var successFailedTask = GetSuccessFailedDailyAsync(validatedDates.FromDate, validatedDates.ToDate);
            var ratingsTask = GetRatingsDailyAsync(validatedDates.FromDate, validatedDates.ToDate);
            var recentChatsTask = GetRecentChatsAsync();

            await Task.WhenAll(statsTask, categoriesTask, dailyChatsTask, successFailedTask, ratingsTask, recentChatsTask);

            return new DashboardData
            {
                Stats = await statsTask,
                Categories = await categoriesTask,
                DailyChats = await dailyChatsTask,
                SuccessFailedDaily = await successFailedTask,
                RatingsDaily = await ratingsTask,
                RecentChats = await recentChatsTask
            };
        }
        catch (InvalidDateRangeException)
        {
            // Re-throw custom exceptions
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching dashboard data");
            throw new DataServiceException(nameof(DashboardService), "Failed to fetch dashboard data", ex);
        }
    }

    public async Task<Stats> GetStatsAsync(DateTime? fromDate = null, DateTime? toDate = null)
    {
        await Task.Delay(10); // Simulate async operation

        // In a real implementation, this would filter by date range
        return new Stats
        {
            TotalChats = 2847,
            SuccessChats = 2134,
            FailedChats = 713,
            AgentChats = 456
        };
    }

    public async Task<List<ChatDto>> GetRecentChatsAsync(int count = 5)
    {
        // Use configured default if count is not specified properly
        if (count <= 0)
            count = _settings.DefaultRecentChatsCount;
            
        _logger.LogInformation("Getting recent chats, count: {Count}", count);

        // Get recent chats from the shared chat service - no conversion needed now
        var recentChats = await _chatService.GetRecentTopChatsAsync(
            DateTime.Today.AddDays(-7), 
            DateTime.Today, 
            count);

        return recentChats;
    }

    public async Task<Dictionary<string, int>> GetCategoriesAsync()
    {
        await Task.Delay(10); // Simulate async operation

        return new Dictionary<string, int>
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
        };
    }

    public async Task<DailyChats> GetDailyChatsAsync(DateTime fromDate, DateTime toDate)
    {
        await Task.Delay(10); // Simulate async operation

        // In a real implementation, this would generate data based on the actual date range
        return new DailyChats
        {
            Labels = new List<string> { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" },
            Data = new List<int> { 285, 342, 398, 456, 523, 389, 454 }
        };
    }

    public async Task<SuccessFailedDaily> GetSuccessFailedDailyAsync(DateTime fromDate, DateTime toDate)
    {
        await Task.Delay(10); // Simulate async operation

        // In a real implementation, this would generate data based on the actual date range
        return new SuccessFailedDaily
        {
            Labels = new List<string> { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" },
            Success = new List<int> { 215, 289, 321, 378, 423, 312, 376 },
            Failed = new List<int> { 70, 53, 77, 78, 100, 77, 78 }
        };
    }

    public async Task<RatingsDaily> GetRatingsDailyAsync(DateTime fromDate, DateTime toDate)
    {
        await Task.Delay(10); // Simulate async operation

        // In a real implementation, this would generate data based on the actual date range
        return new RatingsDaily
        {
            Labels = new List<string> { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" },
            Data = new List<double> { 4.2, 4.5, 4.1, 4.3, 4.6, 4.4, 4.5 }
        };
    }

    public async Task<Chat?> GetChatByIdAsync(int chatId)
    {
        await Task.Delay(10); // Simulate async operation

        var allChats = GetMockChatData();
        return allChats.FirstOrDefault(c => c.Id == chatId);
    }

    /// <summary>
    /// Validates and sets default date range if not provided
    /// </summary>
    private (DateTime FromDate, DateTime ToDate) ValidateDateRange(DateTime? fromDate, DateTime? toDate)
    {
        var defaultFromDate = DateTime.Today.AddDays(-_settings.DefaultDateRangeDays);
        var defaultToDate = DateTime.Today;

        var validFromDate = fromDate ?? defaultFromDate;
        var validToDate = toDate ?? defaultToDate;

        // Ensure fromDate is not after toDate
        if (validFromDate > validToDate)
        {
            _logger.LogWarning("FromDate {FromDate} is after ToDate {ToDate}, swapping dates", 
                validFromDate, validToDate);
            
            throw new InvalidDateRangeException(validFromDate, validToDate, 
                "From date cannot be after To date");
        }

        // Ensure date range is not too large
        if ((validToDate - validFromDate).TotalDays > _settings.MaxDateRangeDays)
        {
            _logger.LogWarning("Date range is too large: {Days} days, max allowed: {MaxDays}", 
                (validToDate - validFromDate).TotalDays, _settings.MaxDateRangeDays);
                
            throw new InvalidDateRangeException(validFromDate, validToDate, 
                $"Date range cannot exceed {_settings.MaxDateRangeDays} days");
        }

        return (validFromDate, validToDate);
    }

    /// <summary>
    /// Returns mock chat data - in a real implementation, this would come from a database
    /// </summary>
    private List<Chat> GetMockChatData()
    {
        return new List<Chat>
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
                    new Message { Type = "user", Content = "Hi, can you check the status of flight AA123?", Timestamp = "14:32" },
                    new Message { Type = "assistant", Content = "Hello! I'd be happy to help you check the status of flight AA123. Let me look that up for you.", Timestamp = "14:32" },
                    new Message { Type = "assistant", Content = "Flight AA123 from New York to Los Angeles is currently delayed by 45 minutes due to weather conditions. The new estimated departure time is 3:45 PM.", Timestamp = "14:33" },
                    new Message { Type = "user", Content = "Thank you for the update. Will this affect my connecting flight?", Timestamp = "14:33" },
                    new Message { Type = "assistant", Content = "I can help you check your connecting flight. Could you please provide me with your connecting flight number?", Timestamp = "14:34" },
                    new Message { Type = "user", Content = "It's flight AA456 to Chicago", Timestamp = "14:34" },
                    new Message { Type = "assistant", Content = "Good news! Flight AA456 to Chicago departs at 6:30 PM, so you should have enough time to make your connection even with the delay.", Timestamp = "14:35" }
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
                    new Message { Type = "user", Content = "I need to book a flight to Miami for next week", Timestamp = "13:45" },
                    new Message { Type = "assistant", Content = "I'd be happy to help you book a flight to Miami! Could you please tell me your departure city and preferred travel dates?", Timestamp = "13:45" },
                    new Message { Type = "user", Content = "From Boston, departing March 15th and returning March 22nd", Timestamp = "13:46" },
                    new Message { Type = "assistant", Content = "Perfect! I found several options for Boston to Miami. Here are the best available flights for your dates...", Timestamp = "13:46" }
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
                    new Message { Type = "user", Content = "My baggage didn't arrive with my flight", Timestamp = "12:18" },
                    new Message { Type = "assistant", Content = "I'm sorry to hear about your missing baggage. I can help you report this and track it down. Can you provide me with your flight details?", Timestamp = "12:18" },
                    new Message { Type = "user", Content = "Flight BA789 from London to New York yesterday", Timestamp = "12:19" },
                    new Message { Type = "assistant", Content = "Thank you. I've created a baggage claim case for you. Your reference number is BG123456. We'll track your baggage and contact you as soon as we locate it.", Timestamp = "12:20" }
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
                    new Message { Type = "user", Content = "I'm trying to check in online but getting an error", Timestamp = "11:22" },
                    new Message { Type = "assistant", Content = "I'm sorry you're experiencing issues with online check-in. Can you tell me what error message you're seeing?", Timestamp = "11:22" },
                    new Message { Type = "user", Content = "It says \"System temporarily unavailable\"", Timestamp = "11:23" },
                    new Message { Type = "assistant", Content = "I apologize for the technical difficulties. Unfortunately, I'm unable to complete your check-in through this system. Please visit the airport check-in counter or try again later.", Timestamp = "11:24" }
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
                    new Message { Type = "user", Content = "How many miles do I have in my account?", Timestamp = "10:15" },
                    new Message { Type = "assistant", Content = "I can help you check your miles balance. Your current balance is 45,230 miles. Would you like to know about redemption options?", Timestamp = "10:15" },
                    new Message { Type = "user", Content = "Yes, what can I get with those miles?", Timestamp = "10:16" },
                    new Message { Type = "assistant", Content = "With 45,230 miles, you can redeem for a domestic round-trip flight (25,000 miles) or upgrade to business class on your next international flight (40,000 miles).", Timestamp = "10:16" }
                }
            }
        };
    }
}