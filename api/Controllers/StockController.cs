using api.Data;
using api.Dtos;
using api.Dtos.Stocks;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/stocks")]
[ApiController]
public class StockController(IStockRepository stockRepository) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var stocks = await stockRepository.GetAllAsync();

        var stocksDto = stocks.Select(x => x.ToStockDto());

        return Ok(stocksDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var stock = await stockRepository.GetByIdAsync(id);
        
        if(stock == null)
            return NotFound();

        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateStockRequestDto stock)
    {
        var stockModel = stock.ToStockFromCreateDTO();
        await stockRepository.CreateAsync(stockModel);
        return CreatedAtAction("GetById", new { id = stockModel.Id}, stockModel.ToStockDto());
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id,  [FromBody] UpdateStockRequestDto stockInput)
    {
        var stockModel = await stockRepository.UpdateAsync(id, stockInput);

        if(stockModel is null)
            return NotFound();

        return Ok(stockModel);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        var stock = await stockRepository.DeleteAsync(id);

        if(stock is null)
            return NotFound();

        return NoContent();
    }
}