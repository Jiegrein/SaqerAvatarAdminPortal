using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using SaqerAvatarAdminPortal.Services.Interfaces;
using SaqerAvatarAdminPortal.Models.Chat;

namespace SaqerAvatarAdminPortal.Pages;

public class ChatsHistoryModel : PageModel
{
    private readonly IChatService _chatService;
    private readonly ILogger<ChatsHistoryModel> _logger;
    private const int DefaultPageSize = 10;

    public ChatsHistoryModel(IChatService chatService, ILogger<ChatsHistoryModel> logger)
    {
        _chatService = chatService;
        _logger = logger;
    }

    // Filter properties with proper binding
    [BindProperty(SupportsGet = true)]
    [DisplayName("Selected Date")]
    [DataType(DataType.Date)]
    public DateTime SelectedDate { get; set; } = DateTime.Today;

    [BindProperty(SupportsGet = true)]
    [StringLength(100, ErrorMessage = "Search query cannot be longer than 100 characters.")]
    public string? SearchQuery { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? SelectedCategory { get; set; }

    [BindProperty(SupportsGet = true)]
    [Range(1, int.MaxValue, ErrorMessage = "Page must be greater than 0")]
    public int PageNumber { get; set; } = 1;

    // Read-only properties for display - using unified data from ChatService
    public ChatStatsDto Stats { get; private set; } = new();
    public PaginatedList<ChatDto> Chats { get; private set; } = new(new List<ChatDto>(), 0, 1, DefaultPageSize);
    public IReadOnlyList<string> Categories => _chatService.GetChatCategories();

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            _logger.LogInformation("Loading chats history page with filters - Date: {SelectedDate}, Search: {SearchQuery}, Category: {SelectedCategory}, Page: {PageNumber}", 
                SelectedDate, SearchQuery, SelectedCategory, PageNumber);

            // Validate model state
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state, resetting to defaults");
                ResetToDefaults();
            }

            // Get filtered data using the unified ChatService
            var filteredChats = await _chatService.GetFilteredChatsAsync(SelectedDate, SearchQuery, SelectedCategory);
            
            // Calculate stats for the selected date using the same service
            Stats = await _chatService.GetChatStatsAsync(SelectedDate, SelectedDate);
            
            // Apply pagination using the filtered results
            Chats = PaginatedList<ChatDto>.Create(filteredChats, PageNumber, DefaultPageSize);

            _logger.LogInformation("Successfully loaded {TotalChats} chats for date {SelectedDate}, showing page {PageNumber} of {TotalPages}", 
                Chats.TotalCount, SelectedDate, Chats.PageIndex, Chats.TotalPages);

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading chats history page");
            
            // Provide empty data and show error state
            Stats = new ChatStatsDto();
            Chats = new PaginatedList<ChatDto>(new List<ChatDto>(), 0, 1, DefaultPageSize);
            
            ModelState.AddModelError(string.Empty, "Unable to load chat history. Please try again later.");
            return Page();
        }
    }

    public IActionResult OnPostSetDate(string dateType)
    {
        _logger.LogInformation("Setting date to: {DateType}", dateType);
        
        SelectedDate = dateType.ToLower() switch
        {
            "today" => DateTime.Today,
            "yesterday" => DateTime.Today.AddDays(-1),
            _ => DateTime.Today
        };

        // Reset pagination and filters when changing date
        PageNumber = 1;
        
        return RedirectToPage(new { SelectedDate, SearchQuery, SelectedCategory, PageNumber });
    }

    private void ResetToDefaults()
    {
        SelectedDate = DateTime.Today;
        PageNumber = 1;
        SearchQuery = null;
        SelectedCategory = null;
    }
}

// Pagination helper class
public class PaginatedList<T> : List<T>
{
    public int PageIndex { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }

    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        AddRange(items);
    }

    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;

    public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
    {
        var count = source.Count();
        var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }
}
