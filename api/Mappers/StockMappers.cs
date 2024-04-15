using api.Dtos;
using api.Dtos.Stocks;
using api.Models;

namespace api.Mappers;
public static class StickMappers
{
    public static StockDto ToStockDto(this Stock stockModel)
    {
        return new StockDto
        {
            Id = stockModel.Id,
            Symbol = stockModel.Symbol,
            CopanyName = stockModel.CopanyName,
            Industry = stockModel.Industry,
            LastDiv = stockModel.LastDiv,
            MarketCap =stockModel.MarketCap,
            Purchase = stockModel.Purchase,
            Comments = stockModel.Comments.Select(x=>x.ToCommentDto()).ToList()
        };
    }

    public static Stock ToStockFromCreateDTO(this CreateStockRequestDto stockDto){
        return new Stock{
            Symbol = stockDto.Symbol,
            CopanyName = stockDto.CopanyName,
            Industry = stockDto.Industry,
            Purchase = stockDto.Purchase,
            LastDiv = stockDto.LastDiv,
            MarketCap = stockDto.MarketCap,
        };
    }
}