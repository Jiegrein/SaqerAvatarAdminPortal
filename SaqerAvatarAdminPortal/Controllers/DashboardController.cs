using Microsoft.AspNetCore.Mvc;
using SaqerAvatarAdminPortal.Models.Dashboard;
using SaqerAvatarAdminPortal.Services.Interfaces;
using SaqerAvatarAdminPortal.Exceptions;

namespace SaqerAvatarAdminPortal.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;
    private readonly ILogger<DashboardController> _logger;

    public DashboardController(IDashboardService dashboardService, ILogger<DashboardController> logger)
    {
        _dashboardService = dashboardService;
        _logger = logger;
    }

    /// <summary>
    /// Gets a specific chat by ID for modal display
    /// </summary>
    /// <param name="id">Chat ID</param>
    /// <returns>Chat details with messages</returns>
    [HttpGet("chat/{id:int}")]
    public async Task<ActionResult<Chat>> GetChat(int id)
    {
        try
        {
            _logger.LogInformation("Fetching chat with ID: {ChatId}", id);
            
            var chat = await _dashboardService.GetChatByIdAsync(id);
            if (chat == null)
            {
                _logger.LogWarning("Chat not found with ID: {ChatId}", id);
                return NotFound(new { message = $"Chat with ID {id} not found" });
            }

            return Ok(chat);
        }
        catch (DataServiceException ex)
        {
            _logger.LogError(ex, "Data service error while fetching chat {ChatId}", id);
            return StatusCode(500, new { message = "Unable to retrieve chat data", errorCode = ex.ErrorCode });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while fetching chat {ChatId}", id);
            return StatusCode(500, new { message = "An unexpected error occurred" });
        }
    }
}