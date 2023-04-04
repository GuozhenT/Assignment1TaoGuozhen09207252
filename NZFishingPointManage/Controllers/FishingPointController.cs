using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZFishingPointManage.Data;
using NZFishingPointManage.Models.Domain;
using NZFishingPointManage.Models;

namespace NZFishingPointManage.Controllers
{
    public class FishingPointController : Controller
    {
        private readonly ApplicationDbContext _appDbContext;

        public FishingPointController(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var fishingPoints = await _appDbContext.FishingPoints.ToListAsync();
            return View(fishingPoints);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddFishingPointViewModel addFishingPointRequest)
        {
            var fishingPoint = new FishingPoint()
            {
                Id = Guid.NewGuid(),
                Name = addFishingPointRequest.Name,
                Address = addFishingPointRequest.Address,
                PhoneNumber = addFishingPointRequest.PhoneNumber
            };
            await _appDbContext.FishingPoints.AddAsync(fishingPoint);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var fishingPoint = await _appDbContext.FishingPoints.FirstOrDefaultAsync(x => x.Id == id);

            if (fishingPoint != null)
            {
                var viewModel = new UpdateFishingPointViewModel()
                {
                    Id = fishingPoint.Id,
                    Name = fishingPoint.Name,
                    Address = fishingPoint.Address,
                    PhoneNumber = fishingPoint.PhoneNumber
                };

                return await Task.Run(()=> View("View", viewModel));
            }
            

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateFishingPointViewModel model)
        {
            var fishingPoint = await _appDbContext.FishingPoints.FindAsync(model.Id);
            if (fishingPoint != null)
            {
                fishingPoint.Name = model.Name;
                fishingPoint.Address = model.Address;
                fishingPoint.PhoneNumber = model.PhoneNumber;

                await _appDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateFishingPointViewModel model)
        {
            var fishingPoint = await _appDbContext.FishingPoints.FindAsync(model.Id);

            if (fishingPoint != null)
            {

                _appDbContext.FishingPoints.Remove(fishingPoint);
                await _appDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
     }
}
