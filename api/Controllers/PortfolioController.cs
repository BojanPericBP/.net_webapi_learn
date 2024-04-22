using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;
[Route("api/portfolios")]
[ApiController]
public class PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepo ) : ControllerBase
{
    [HttpGet]
    // [Authorize]
    public async Task<IActionResult> GetUserPortfolioAsync(){
        var username = User.GetUsername();
        var appUser = await userManager.FindByNameAsync(username);
        var userPortfolio = await portfolioRepo.GetUserPortfolioAsync(appUser);
        return Ok(userPortfolio);
    }

    [HttpPost]
    // [Authorize]
    public async Task<IActionResult> CreateUserPortfolioAsync(string symbol)
    {
        var username = User.GetUsername();
        var appUser = await userManager.FindByNameAsync(username);
        var stock = await stockRepository.GetBySymbolAsync(symbol);

        if(stock is null)
            return NotFound("Stock not found");

        var userPortfolio = await portfolioRepo.GetUserPortfolioAsync(appUser);

        if(userPortfolio.Where(x => x.Symbol.ToLower() == symbol.ToLower()).Any())
            return BadRequest("Cannot add same stock to portfolio");

        var portfolioModel = new Portfolio
        {
            StockId = stock.Id,
            AppUserId = appUser.Id,
        };

        var result = await portfolioRepo.CreateAsync(portfolioModel);

        if(result is null)
            return StatusCode(500, "Could not create");

        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUserPortfolio(string symbol){
        var username = User.GetUsername();
        var appUser = await userManager.FindByNameAsync(username);

        var userPortfolio = await portfolioRepo.GetUserPortfolioAsync(appUser);

        var filteredStock = userPortfolio.Where(x => x.Symbol.ToLower() == symbol.ToLower()).ToList();

        if(filteredStock is null)
            return BadRequest("Stock is not in portfolio");

        if(filteredStock.Count() == 1)
            await portfolioRepo.DeletAsync(appUser, symbol);

        return Ok();
        
    }
}
