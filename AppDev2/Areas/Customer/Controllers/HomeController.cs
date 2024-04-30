using AppDev2.Data;
using AppDev2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AppDev2.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            {
                // Lấy danh sách các JobListings từ cơ sở dữ liệu
                var jobListings = await _context.JobListings.ToListAsync();

                // Trả về danh sách các JobListings cho giao diện
                return View(jobListings);
            }
            
        } 


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicationId,JobListingId,Message,Description,DisplayOrder")] ApplicationModel applicationModel)
        {
            if (ModelState.IsValid)
            {
                _context.Applications.Add(applicationModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Trả về lại trang tạo với mô hình đã nhập nếu có lỗi
            return View(applicationModel);
        }
    }
}
