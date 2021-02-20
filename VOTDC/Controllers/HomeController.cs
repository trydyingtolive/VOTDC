using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VOTDC.Data;
using VOTDC.Models;
using VOTDC.ViewModels;

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

        public IActionResult Search(ViewModels.SearchViewModel search)
        {
            if (ModelState.IsValid)
            {
                var user = GetUser();
                var client = new ApiClient();
                return Json(client.GetResponse(search, user));
            }
            throw new Exception("Not valid");
        }

        [HttpPost]
        public IActionResult Favorite(Guid verseId)
        {
            // Get the claims values
            User user = GetUser();

            if (user == null)
            {
                return Json(new FavoriteViewModel { IsLoggedIn = false });
            }

            var favoriteViewModel = new FavoriteViewModel()
            {
                IsLoggedIn = true,
                VerseId = verseId,
                IsFavorited = false
            };

            if (!dataContext.Verses.Any(v => v.Id == verseId))
            {
                return Json(favoriteViewModel);
            }

            var favorite = dataContext.Favorites.Where(f => f.UserId == user.Id && f.VerseId == verseId).FirstOrDefault();

            //toggle off
            if (favorite != null)
            {
                dataContext.Favorites.Remove(favorite);
                dataContext.SaveChanges();
                return Json(favoriteViewModel);
            }

            //toggle on
            favorite = new Favorite
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                VerseId = verseId,
                CreatedDateTime = DateTime.Now
            };
            dataContext.Favorites.Add(favorite);
            dataContext.SaveChanges();

            favoriteViewModel.IsFavorited = true;
            return Json(favoriteViewModel);
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

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Failed Login");
            }

            login.Username = login.Username.ToLower();

            //Get or create the user from the db
            var user = dataContext.Users.Where(u => u.Username == login.Username).FirstOrDefault();
            if (user == null)
            {
                user = new User
                {
                    Id = Guid.NewGuid(),
                    Username = login.Username
                };

                dataContext.Users.Add(user);
                dataContext.SaveChanges();
            }

            //save the claim in the cookie
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };
            await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", "user", "role")));


            //add the favorite
            var favorite = dataContext.Favorites
                .Where(f => f.VerseId == login.VerseId && f.UserId == user.Id)
                .FirstOrDefault();

            if (favorite == null && login.VerseId != Guid.Empty)
            {
                dataContext.Favorites.Add(new Favorite
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    VerseId = login.VerseId,
                    CreatedDateTime = DateTime.Now
                });
                dataContext.SaveChanges();
            }

            //There may be some other favorites so we'll send back a fresh view
            var apiClient = new ApiClient();
            return Json(apiClient.GetResponse(new SearchViewModel(login.StartDate, login.PageSize), user));
        }

        public IActionResult Login()
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
