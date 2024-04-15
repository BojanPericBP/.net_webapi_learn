using api.Data;
using api.Dtos.Stocks;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;
public class StockRepository(ApplicationDbContext context) : IStockRepository
{
    private readonly ApplicationDbContext _db = context;

    public async Task<List<Stock>> GetAllAsync()
    {
        return await _db.Stocks.Include(x=>x.Comments).ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await _db.Stocks.Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Stock> CreateAsync(Stock stockModel)
    {
        await _db.Stocks.AddAsync(stockModel);
        await _db.SaveChangesAsync();

        return stockModel;
    }

    public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockInput)
    {
        var stockModel = await _db.Stocks.FirstOrDefaultAsync(x => x.Id == id);

        if (stockModel is null)
            return null;
        
        stockModel.Symbol = stockInput.Symbol;
        stockModel.CopanyName = stockInput.CopanyName;
        stockModel.Purchase = stockInput.Purchase;
        stockModel.MarketCap = stockInput.MarketCap;
        stockModel.Industry = stockInput.Industry;
        stockModel.LastDiv = stockInput.LastDiv;
        
        await _db.SaveChangesAsync();

        return stockModel;
    }

    public async Task<Stock?> DeleteAsync(int id)
    {
        var stockModel = await _db.Stocks.FirstOrDefaultAsync(x => x.Id == id);

        if(stockModel is null)
            return null;
        
        _db.Stocks.Remove(stockModel);

        await _db.SaveChangesAsync();

        return stockModel;
    }

}
