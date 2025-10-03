using SaqerAvatarAdminPortal.Models.Chat;

namespace SaqerAvatarAdminPortal.Services.Interfaces;

public interface IChatService
{
    /// <summary>
    /// Gets chat details by ID for modal display
    /// </summary>
    Task<ChatDetailsDto?> GetChatDetailsAsync(int chatId);
    
    /// <summary>
    /// Gets filtered chats for the chats history page
    /// </summary>
    Task<IQueryable<ChatDto>> GetFilteredChatsAsync(DateTime selectedDate, string? searchQuery = null, string? category = null);
    
    /// <summary>
    /// Gets recent top chats for dashboard (uses the same data source as filtered chats)
    /// </summary>
    Task<List<ChatDto>> GetRecentTopChatsAsync(DateTime dateFrom, DateTime dateTo, int count = 5);
    
    /// <summary>
    /// Calculates chat statistics for a date range
    /// </summary>
    Task<ChatStatsDto> GetChatStatsAsync(DateTime dateFrom, DateTime dateTo);
    
    /// <summary>
    /// Gets available chat categories
    /// </summary>
    List<string> GetChatCategories();
    
    /// <summary>
    /// Gets all chats in date range (unified method for both pages)
    /// </summary>
    Task<IQueryable<ChatDto>> GetChatsInDateRangeAsync(DateTime dateFrom, DateTime dateTo);
    
    /// <summary>
    /// Gets all sample chat data (centralized data source)
    /// </summary>
    Task<List<ChatDto>> GetAllChatsAsync();
}