using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;
public class PortfolioRepository(ApplicationDbContext db) : IPortfolioRepository
{
    public async Task<List<Stock>> GetUserPortfolioAsync(AppUser user)
    {
        return await db.Portfolios.Where(u => u.AppUserId == user.Id)
            .Select(p => new Stock
            {
                Id = p.Stock.Id,
                Symbol = p.Stock.Symbol,
                CopanyName = p.Stock.CopanyName,
                Purchase = p.Stock.Purchase,
                Industry = p.Stock.Industry,
                LastDiv = p.Stock.LastDiv,
                MarketCap = p.Stock.MarketCap,
            }).ToListAsync();
    }

    public async Task<Portfolio?> CreateAsync(Portfolio portfolio)
    {
        var res = await db.Portfolios.AddAsync(portfolio);
        await db.SaveChangesAsync();

        return res.Entity;
    }

    public async Task<Portfolio?> DeletAsync(AppUser appUser, string symbol)
    {
        var portfolioModel = await db.Portfolios.FirstOrDefaultAsync(x=> x.AppUserId == appUser.Id && x.Stock.Symbol.ToLower() == symbol.ToLower());

        if(portfolioModel is null)
            return null;

        var result = db.Remove(portfolioModel);

        await db.SaveChangesAsync();

        return result.Entity;
    }
}
