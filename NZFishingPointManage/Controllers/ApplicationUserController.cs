using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZFishingPointManage.Data;

namespace NZFishingPointManage.Controllers
{
    public class ApplicationUserController : Controller
    {
        private readonly ApplicationDbContext _appDbContext;
        public ApplicationUserController(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var applicationUsers = await _appDbContext.ApplicationUsers.ToListAsync();
            return View(applicationUsers);
        }
    }
}
