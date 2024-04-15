using api.Dtos.Comments;
using api.Models;

namespace api.Dtos.Stocks;

public class StockDto
{
    public int Id { get; set; }

    public string Symbol { get; set; }  = string.Empty;

    public string CopanyName { get; set; } = string.Empty;

    public decimal Purchase { get; set; }

    public decimal LastDiv { get; set; }

    public string Industry { get; set; } = string.Empty;

    public long MarketCap { get; set; }

    public List<CommentDto> Comments { get; set; } = default!;
}