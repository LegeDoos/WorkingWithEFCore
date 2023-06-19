﻿using Microsoft.AspNetCore.Mvc;
using Northwind.Models;
using Northwind.Shared;
using System.Diagnostics;

namespace Northwind.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NorthwindDataContext db;

        public HomeController(ILogger<HomeController> logger, NorthwindDataContext injectedContext)
        {
            _logger = logger;
            db = injectedContext;
        }

        public IActionResult Index()
        {
            HomeIndexViewModel model = new(Random.Shared.Next(1, 1001), db.Categories.ToList(), db.Products.ToList());
            return View(model);
        }

        public IActionResult ProductDetail(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("You have to pass a product id!");
            }

            Product? model = db.Products.SingleOrDefault(p => p.ProductId == id);

            if (model is null)
            {
                return NotFound($"Product {id} not found");
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            _logger.LogError("This is a serious error (not!)");
            _logger.LogWarning("Warning 1... 2...");
            _logger.LogCritical("Einde oefening!");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}