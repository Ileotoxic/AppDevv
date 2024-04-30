using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppDev2.Data;
using AppDev2.Models;
using Microsoft.AspNetCore.Authorization;

namespace AppDev2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Customer")]
    public class JobListingModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobListingModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách việc làm
        public async Task<IActionResult> Index()
        {
            var jobListings = await _context.JobListings.ToListAsync();
            return View(jobListings);
        }

        // Hiển thị chi tiết việc làm
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobListing = await _context.JobListings
                .FirstOrDefaultAsync(m => m.JobListingId == id);
            if (jobListing == null)
            {
                return NotFound();
            }

            return View(jobListing);
        }

        // Hiển thị trang tạo việc làm mới
        public IActionResult Create()
        {
            return View();
        }

        // Tạo việc làm mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobListingId,Title,Description,ApplicationDeadline,Location,ApplicationId,Image")] JobListingModel jobListing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobListing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobListing);
        }

        // Hiển thị trang chỉnh sửa việc làm
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobListing = await _context.JobListings.FindAsync(id);
            if (jobListing == null)
            {
                return NotFound();
            }
            return View(jobListing);
        }

        // Cập nhật việc làm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobListingId,Title,Description,ApplicationDeadline,Location,ApplicationId,Image")] JobListingModel jobListing)
        {
            if (id != jobListing.JobListingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobListing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobListingModelExists(id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(jobListing);
        }

        // Xóa việc làm
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobListing = await _context.JobListings
                .FirstOrDefaultAsync(m => m.JobListingId == id);
            if (jobListing == null)
            {
                return NotFound();
            }

            return View(jobListing);
        }

        // Xác nhận xóa việc làm
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobListing = await _context.JobListings.FindAsync(id);
            if (jobListing != null)
            {
                _context.JobListings.Remove(jobListing);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool JobListingModelExists(int id)
        {
            return _context.JobListings.Any(e => e.JobListingId == id);
        }
    }
}
