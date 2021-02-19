using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VOTDC.Data;
using VOTDC.Models;

namespace VOTDC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly DataContext dataContext;

        public HomeController(DataContext _dataContext, ILogger<HomeController> _logger)
        {
            dataContext = _dataContext;
            logger = _logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string Search(ViewModels.Search search)
        {
            if (ModelState.IsValid)
            {
                Response.ContentType = "application/json";
                var client = new ApiClient();
                return client.GetResponse(search);
            }
            throw new Exception("Not valid");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
