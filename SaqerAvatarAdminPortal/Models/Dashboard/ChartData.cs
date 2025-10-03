using System.ComponentModel.DataAnnotations;

namespace SaqerAvatarAdminPortal.Models.Dashboard;

public class DailyChats
{
    public List<string> Labels { get; set; } = new();
    public List<int> Data { get; set; } = new();

    /// <summary>
    /// Validates that Labels and Data arrays have the same length
    /// </summary>
    public bool IsValid => Labels.Count == Data.Count && Labels.Count > 0;
}

public class SuccessFailedDaily
{
    public List<string> Labels { get; set; } = new();
    public List<int> Success { get; set; } = new();
    public List<int> Failed { get; set; } = new();

    /// <summary>
    /// Validates that all arrays have the same length
    /// </summary>
    public bool IsValid => Labels.Count == Success.Count && 
                          Labels.Count == Failed.Count && 
                          Labels.Count > 0;

    /// <summary>
    /// Calculated property for total chats per day
    /// </summary>
    public List<int> TotalDaily => Success.Zip(Failed, (s, f) => s + f).ToList();
}

public class RatingsDaily
{
    public List<string> Labels { get; set; } = new();
    public List<double> Data { get; set; } = new();

    /// <summary>
    /// Validates that Labels and Data arrays have the same length and ratings are valid
    /// </summary>
    public bool IsValid => Labels.Count == Data.Count && 
                          Labels.Count > 0 && 
                          Data.All(rating => rating >= 0 && rating <= 5);

    /// <summary>
    /// Calculated property for average rating across all days
    /// </summary>
    public double AverageRating => Data.Count > 0 ? Math.Round(Data.Average(), 2) : 0;
}

/// <summary>
/// Generic chart data point for flexible chart implementations
/// </summary>
public class ChartDataPoint
{
    public string Label { get; set; } = string.Empty;
    public double Value { get; set; }
    public DateTime? Date { get; set; }
    public string Category { get; set; } = string.Empty;
}