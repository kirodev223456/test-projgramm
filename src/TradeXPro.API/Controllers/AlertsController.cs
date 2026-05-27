using Microsoft.AspNetCore.Mvc;
using TradeXPro.API.DTOs;
using TradeXPro.Data.Entities;
using TradeXPro.Data.Repositories.Interfaces;

namespace TradeXPro.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlertsController : ControllerBase
{
    private readonly IRepository<PriceAlert> _alertRepo;

    public AlertsController(IRepository<PriceAlert> alertRepo)
    {
        _alertRepo = alertRepo;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<AlertDto>>>> GetAll()
    {
        var alerts = await _alertRepo.GetAllAsync();
        var dtos = alerts.Select(a => new AlertDto
        {
            Id = a.Id,
            Symbol = a.Symbol,
            CompanyName = a.CompanyName,
            Condition = a.Condition,
            TargetPrice = a.TargetPrice,
            Status = a.Status,
            NotifyVia = a.NotifyVia,
            Notes = a.Notes,
            CreatedAt = a.CreatedAt,
            TriggeredAt = a.TriggeredAt
        });

        return Ok(ApiResponse<IEnumerable<AlertDto>>.Ok(dtos));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<AlertDto>>> Create([FromBody] CreateAlertRequest request)
    {
        var alert = new PriceAlert
        {
            UserId = 1, // TODO: Get from JWT
            Symbol = request.Symbol,
            CompanyName = request.CompanyName,
            Condition = request.Condition,
            TargetPrice = request.TargetPrice,
            NotifyVia = request.NotifyVia,
            Notes = request.Notes
        };

        if (request.Expiration == "1 Day") alert.ExpiresAt = DateTime.UtcNow.AddDays(1);
        else if (request.Expiration == "1 Week") alert.ExpiresAt = DateTime.UtcNow.AddDays(7);
        else if (request.Expiration == "1 Month") alert.ExpiresAt = DateTime.UtcNow.AddDays(30);

        await _alertRepo.AddAsync(alert);

        var dto = new AlertDto
        {
            Id = alert.Id,
            Symbol = alert.Symbol,
            CompanyName = alert.CompanyName,
            Condition = alert.Condition,
            TargetPrice = alert.TargetPrice,
            Status = alert.Status,
            NotifyVia = alert.NotifyVia,
            Notes = alert.Notes,
            CreatedAt = alert.CreatedAt
        };

        return Ok(ApiResponse<AlertDto>.Ok(dto, "Alert created successfully"));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse>> Delete(int id)
    {
        if (!await _alertRepo.ExistsAsync(id))
            return Ok(ApiResponse.Fail("Alert not found"));

        await _alertRepo.DeleteAsync(id);
        return Ok(ApiResponse.Ok("Alert deleted successfully"));
    }
}
