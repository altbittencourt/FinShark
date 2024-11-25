using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace api.Controllers
{
	[Route("api/portfolio")]
	public class PortfolioController : ControllerBase
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IStockRepository _stockRepo;
		private readonly IPortfolioRepository _portfolioRepo;


		public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepo)
		{
			_userManager = userManager;
			_stockRepo = stockRepository;
			_portfolioRepo = portfolioRepo;
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> GetPortfolio()
		{
			var username = User.GetUsername();
			var appUser = await _userManager.FindByNameAsync(username);
			var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
			return Ok(userPortfolio);
		}

	}
}