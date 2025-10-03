using Microsoft.AspNetCore.Mvc;
using SaqerAvatarAdminPortal.Services.Interfaces;

namespace SaqerAvatarAdminPortal.Controllers.Api;

[ApiController]
[Route("api/chat")]
public class ChatApiController : Controller  // Changed to Controller instead of ControllerBase
{
    private readonly IChatService _chatService;
    private readonly ILogger<ChatApiController> _logger;

    public ChatApiController(IChatService chatService, ILogger<ChatApiController> logger)
    {
        _chatService = chatService;
        _logger = logger;
    }

    [HttpGet("{chatId}")]
    public async Task<IActionResult> GetChatDetails(int chatId)
    {
        try
        {
            _logger.LogInformation("Getting chat details for ID: {ChatId}", chatId);

            var chatDetails = await _chatService.GetChatDetailsAsync(chatId);
            if (chatDetails == null)
            {
                _logger.LogWarning("Chat not found for ID: {ChatId}", chatId);
                return NotFound($"Chat with ID {chatId} not found");
            }

            // Return partial view for AJAX requests
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_ChatDetailsPartial", chatDetails);
            }

            // Return JSON for API requests
            return Ok(chatDetails);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting chat details for ID: {ChatId}", chatId);
            return StatusCode(500, "An error occurred while retrieving chat details");
        }
    }
}