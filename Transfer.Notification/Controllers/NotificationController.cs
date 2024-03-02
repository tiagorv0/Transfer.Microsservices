using Microsoft.AspNetCore.Mvc;
using Transfer.Notification.Service;

namespace Transfer.Notification.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetNotifications(CancellationToken ct = default)
    {
        var notifications = await _notificationService.GetAllAsync(ct);
        return Ok(notifications);
    }

    [HttpPut("view-notification")]
    public async Task<IActionResult> ViewNotification(Guid id, CancellationToken ct = default)
    {
        await _notificationService.ViewNotification(id, ct);
        return Ok();
    }
}
