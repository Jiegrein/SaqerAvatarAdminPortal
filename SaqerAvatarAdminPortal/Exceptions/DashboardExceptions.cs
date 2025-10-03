namespace SaqerAvatarAdminPortal.Exceptions;

/// <summary>
/// Custom exception for dashboard-related errors
/// </summary>
public class DashboardException : Exception
{
    public string ErrorCode { get; }
    
    public DashboardException(string message, string errorCode = "DASHBOARD_ERROR") 
        : base(message)
    {
        ErrorCode = errorCode;
    }
    
    public DashboardException(string message, Exception innerException, string errorCode = "DASHBOARD_ERROR") 
        : base(message, innerException)
    {
        ErrorCode = errorCode;
    }
}

/// <summary>
/// Exception for invalid date range errors
/// </summary>
public class InvalidDateRangeException : DashboardException
{
    public DateTime? FromDate { get; }
    public DateTime? ToDate { get; }
    
    public InvalidDateRangeException(DateTime? fromDate, DateTime? toDate, string message) 
        : base(message, "INVALID_DATE_RANGE")
    {
        FromDate = fromDate;
        ToDate = toDate;
    }
}

/// <summary>
/// Exception for data service errors
/// </summary>
public class DataServiceException : DashboardException
{
    public string ServiceName { get; }
    
    public DataServiceException(string serviceName, string message, Exception innerException) 
        : base($"Error in {serviceName}: {message}", innerException, "DATA_SERVICE_ERROR")
    {
        ServiceName = serviceName;
    }
}