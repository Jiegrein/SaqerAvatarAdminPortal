using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaqerAvatarAdminPortal.Models.Dashboard;
using SaqerAvatarAdminPortal.Services.Interfaces;
using SaqerAvatarAdminPortal.Exceptions;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace SaqerAvatarAdminPortal.Pages;

public class IndexModel : PageModel
{
    private readonly IDashboardService _dashboardService;
    private readonly ILogger<IndexModel> _logger;
    private readonly DashboardSettings _settings;

    public IndexModel(IDashboardService dashboardService, ILogger<IndexModel> logger, IOptions<DashboardSettings> settings)
    {
        _dashboardService = dashboardService;
        _logger = logger;
        _settings = settings.Value;
    }

    public DashboardViewModel ViewModel { get; set; } = new();

    [BindProperty]
    public DateFilterViewModel Filter { get; set; } = new()
    {
        DateFrom = DateTime.Today.AddDays(-7),
        DateTo = DateTime.Today
    };

    // Keep these for backward compatibility with the view
    public DateTime DateFrom => Filter.DateFrom ?? DateTime.Today.AddDays(-7);
    public DateTime DateTo => Filter.DateTo ?? DateTime.Today;
    public DashboardData DashboardData => ViewModel.Data;

    public async Task OnGetAsync()
    {
        await LoadDashboardDataAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // Validate the filter
        var validationError = Filter.GetValidationError(_settings.MaxDateRangeDays);
        if (!string.IsNullOrEmpty(validationError))
        {
            ModelState.AddModelError(string.Empty, validationError);
        }

        if (!ModelState.IsValid)
        {
            await LoadDashboardDataAsync();
            return Page();
        }

        await LoadDashboardDataAsync();
        return Page();
    }

    private async Task LoadDashboardDataAsync()
    {
        try
        {
            ViewModel.IsLoading = true;
            ViewModel.Filter = Filter;
            
            _logger.LogInformation("Loading dashboard with date range: {DateFrom} to {DateTo}", 
                Filter.DateFrom, Filter.DateTo);
                
            ViewModel.Data = await _dashboardService.GetDashboardDataAsync(Filter.DateFrom, Filter.DateTo);
            ViewModel.HasErrors = false;
            ViewModel.ErrorMessages.Clear();
        }
        catch (InvalidDateRangeException ex)
        {
            _logger.LogWarning(ex, "Invalid date range provided");
            ViewModel.HasErrors = true;
            ViewModel.ErrorMessages.Add(ex.Message);
            ModelState.AddModelError(string.Empty, ex.Message);
            ViewModel.Data = new DashboardData(); // Provide empty data
        }
        catch (DataServiceException ex)
        {
            _logger.LogError(ex, "Data service error loading dashboard");
            ViewModel.HasErrors = true;
            ViewModel.ErrorMessages.Add("Unable to load dashboard data. Please try again later.");
            ModelState.AddModelError(string.Empty, "Unable to load dashboard data. Please try again later.");
            ViewModel.Data = new DashboardData(); // Provide empty data
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error loading dashboard data");
            ViewModel.HasErrors = true;
            ViewModel.ErrorMessages.Add("An unexpected error occurred. Please try again later.");
            ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
            ViewModel.Data = new DashboardData(); // Provide empty data
        }
        finally
        {
            ViewModel.IsLoading = false;
        }
    }
}


