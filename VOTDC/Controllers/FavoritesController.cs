using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VOTDC.Data;
using VOTDC.Models;
using VOTDC.ViewModels;

namespace VOTDC.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly DataContext dataContext;
        public FavoritesController(DataContext _dataContext, ILogger<HomeController> _logger)
        {
            dataContext = _dataContext;
            logger = _logger;
        }
        public IActionResult Index()
        {
            var user = GetUser();
            if (user == null)
            {
                return Redirect("/Home/Login");
            }

            var verses = dataContext.Favorites.Where(f => f.UserId == user.Id)
                .Select(f => f.Verse)
                .Select(v => new VerseViewModel
                {
                    Id = v.Id,
                    IsFavorite = true,
                    ImageLink = v.ImageLink,
                    ReferenceText = v.ReferenceText,
                    VerseText = v.VerseText,
                    VerseDate = v.VerseDate
                })
                .OrderByDescending(v => v.VerseDate)
                .ToList();

            return View(verses);
        }

        private User GetUser()
        {
            var userIdString = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userIdString))
            {
                return null;
            }

            Guid userId;
            if (!Guid.TryParse(userIdString, out userId))
            {
                return null;
            }

            return dataContext.Users.Where(u => u.Id == userId).FirstOrDefault();
        }
    }
}
