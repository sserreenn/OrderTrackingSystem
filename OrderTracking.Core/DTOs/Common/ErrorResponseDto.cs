namespace OrderTracking.Core.DTOs.Common;
public class ErrorResponseDto
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = null!;
    public string? Details { get; set; }

    public override string ToString() => System.Text.Json.JsonSerializer.Serialize(this);
}