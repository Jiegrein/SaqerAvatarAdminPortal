namespace SaqerAvatarAdminPortal.Models.Chat;

/// <summary>
/// Unified chat data transfer object used by both Dashboard and Chat History pages
/// </summary>
public class ChatDto
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public string Topic { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public int Rating { get; set; }
    public bool IsFailedChat { get; set; }
    public string Category { get; set; } = string.Empty;
    
    // Computed properties for display - consistent across all pages
    public string FormattedDateTime => DateTime.ToString("MMM d, h:mm tt");
    public string FormattedDateTimeShort => DateTime.ToString("MMM d, HH:mm");
    public string StatusText => IsFailedChat ? "Failed" : "Success";
    public string StatusClass => IsFailedChat ? "status-failed" : "status-success";
    public string RatingStars => new string('★', Rating) + new string('☆', 5 - Rating);
    
    // Additional properties for compatibility with Dashboard model
    public string Status => StatusText;
    public string FormattedDate => DateTime.ToString("yyyy-MM-dd HH:mm");
}

public class ChatDetailsDto
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public string Topic { get; set; } = string.Empty;
    public int Rating { get; set; }
    public List<ChatMessageDto> Messages { get; set; } = new();
    
    public string FormattedDateTime => DateTime.ToString("MMM d, h:mm tt");
    public string RatingStars => new string('★', Rating) + new string('☆', 5 - Rating);
}

public class ChatMessageDto
{
    public string Type { get; set; } = string.Empty; // "user" or "assistant"
    public string Content { get; set; } = string.Empty;
    public string Timestamp { get; set; } = string.Empty;
}

public class ChatStatsDto
{
    public int TotalChats { get; set; }
    public int SuccessChats { get; set; }
    public int FailedChats { get; set; }
    public double AverageRating { get; set; }
}