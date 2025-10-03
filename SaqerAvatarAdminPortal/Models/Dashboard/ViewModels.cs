using System.ComponentModel.DataAnnotations;

namespace SaqerAvatarAdminPortal.Models.Dashboard;

/// <summary>
/// View model for date filtering input
/// </summary>
public class DateFilterViewModel
{
    [DataType(DataType.Date)]
    [Display(Name = "From Date")]
    public DateTime? DateFrom { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "To Date")]
    public DateTime? DateTo { get; set; }

    /// <summary>
    /// Validates that the date range is reasonable
    /// </summary>
    public bool IsValid(int maxDays = 365)
    {
        if (!DateFrom.HasValue || !DateTo.HasValue)
            return true; // Allow null values for default range

        if (DateFrom > DateTo)
            return false;

        return (DateTo.Value - DateFrom.Value).TotalDays <= maxDays;
    }

    /// <summary>
    /// Gets validation error message if invalid
    /// </summary>
    public string? GetValidationError(int maxDays = 365)
    {
        if (!DateFrom.HasValue || !DateTo.HasValue)
            return null;

        if (DateFrom > DateTo)
            return "From date cannot be after To date.";

        if ((DateTo.Value - DateFrom.Value).TotalDays > maxDays)
            return $"Date range cannot exceed {maxDays} days.";

        return null;
    }
}

/// <summary>
/// View model for dashboard display with additional UI properties
/// </summary>
public class DashboardViewModel
{
    public DashboardData Data { get; set; } = new();
    public DateFilterViewModel Filter { get; set; } = new();
    public bool HasErrors { get; set; }
    public List<string> ErrorMessages { get; set; } = new();
    public bool IsLoading { get; set; }
    
    /// <summary>
    /// Formatted date range for display
    /// </summary>
    public string DateRangeDisplay 
    { 
        get 
        {
            if (Filter.DateFrom.HasValue && Filter.DateTo.HasValue)
                return $"{Filter.DateFrom:yyyy-MM-dd} to {Filter.DateTo:yyyy-MM-dd}";
            return "Last 7 days";
        } 
    }

    /// <summary>
    /// Indicates if the dashboard has any data to display
    /// </summary>
    public bool HasData => !IsLoading && !HasErrors && 
                          (Data.Stats.TotalChats > 0 || Data.RecentChats.Any());

    /// <summary>
    /// Gets a summary message for the current state
    /// </summary>
    public string StatusMessage 
    { 
        get 
        {
            if (IsLoading) return "Loading dashboard data...";
            if (HasErrors) return "Error loading data";
            if (!HasData) return "No data available";
            return $"Showing data for {DateRangeDisplay}";
        } 
    }
}