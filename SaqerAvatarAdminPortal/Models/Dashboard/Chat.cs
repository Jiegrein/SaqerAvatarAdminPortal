using System.ComponentModel.DataAnnotations;

namespace SaqerAvatarAdminPortal.Models.Dashboard;

public class Chat
{
    public int Id { get; set; }

    [Display(Name = "Date/Time")]
    public string DateTime { get; set; } = string.Empty;

    [Display(Name = "Topic")]
    [Required]
    public string Topic { get; set; } = string.Empty;

    [Display(Name = "Summary")]
    public string Summary { get; set; } = string.Empty;

    [Display(Name = "Rating")]
    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
    public int Rating { get; set; }

    [Display(Name = "Failed Chat")]
    public bool IsFailedChat { get; set; }

    public List<Message> Messages { get; set; } = new();

    /// <summary>
    /// Calculated property for status display
    /// </summary>
    public string Status => IsFailedChat ? "Failed" : "Success";

    /// <summary>
    /// Calculated property for status CSS class
    /// </summary>
    public string StatusClass => IsFailedChat ? "failed" : "success";
}

public class Message
{
    [Required]
    public string Type { get; set; } = string.Empty; // "user" or "assistant"

    [Required]
    public string Content { get; set; } = string.Empty;

    [Required]
    public string Timestamp { get; set; } = string.Empty;

    /// <summary>
    /// Calculated property for message CSS class
    /// </summary>
    public string MessageClass => Type.ToLower() switch
    {
        "user" => "user-message",
        "assistant" => "assistant-message",
        _ => "unknown-message"
    };
}