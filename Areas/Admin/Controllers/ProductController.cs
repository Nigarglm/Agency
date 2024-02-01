using Agency.Areas.ViewModels;
using Agency.Areas.ViewModels.Product;
using Agency.DAL;
using Agency.Models;
using Agency.Utilities.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agency.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context= context;
            _env = env;
        }
        [Authorize("Admin, Moderator")]
        public async Task<IActionResult> Index(int page)
        {
            double count = await _context.Products.CountAsync();
            List<Product> products = await _context.Products.Skip(page*2).Take(2).ToListAsync();

            PaginateVM<Product> paginateVM = new PaginateVM<Product>
            {
                CurrentPage = page + 1,
                TotalPage = Math.Ceiling(count / 2),
                Items = products
            };

            return View(paginateVM);
        }

        [Authorize("Admin, Moderator")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories=await _context.Categories.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM productVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.ToListAsync();
                return View();
            }

            bool result=await _context.Products.AnyAsync(x=>x.Title.ToLower()==productVM.Title.ToLower());
            if(result)
            {
                ViewBag.Categories = await _context.Products.ToListAsync();
                ModelState.AddModelError("Title", "Bu adli product artiq movcuddur");
                return View();
            }
            if (!productVM.Photo.ValidateType("image/"))
            {
                ViewBag.Categories = await _context.Products.ToListAsync();
                ModelState.AddModelError("Photo", "File tipi uygun deyil");
                return View();
            }
            if (!productVM.Photo.ValidateSize(2 * 1024))
            {
                ViewBag.Categories = await _context.Products.ToListAsync();
                ModelState.AddModelError("Photo", "File olcusu uygun deyil");
                return View();
            }

            string fileName = await productVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "img", "portfolio");

            
            Product product = new Product
            {
                Image = fileName,
                Title = productVM.Title,
                CategoryId=productVM.CategoryId,
                Client=productVM.Client,
                Description= productVM.Description
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            Product existed = await _context.Products.FirstOrDefaultAsync(p=>p.Id== id);

            if (existed == null)
            {
                return NotFound();
            }
            UpdateProductVM productVM = new UpdateProductVM
            {
                Photo = existed.Photo,
                Title = existed.Title,
                Category = existed.Category
            };

            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateProductVM productVM)
        {
            if (!ModelState.IsValid) return View(productVM);
            Product existed = await _context.Products.FirstOrDefaultAsync(p=>p.Id== id);

            if(existed == null) return NotFound();
            
            bool result = _context.Products.Any(p=> p.Title == productVM.Title);
            if (result)
            {
                ModelState.AddModelError("Title", "Bu adli product artiq movcuddur");
                return View();
            }
            if (productVM.Photo != null)
            {
                if (!productVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError("Photo", "File tipi uygun deyil");
                    return View(existed);
                }
                if (!productVM.Photo.ValidateSize(2 * 1024))
                {
                    ModelState.AddModelError("Photo", "File olcusu uygun deyil");
                    return View(existed);
                }

                string newImage = await productVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "img", "portfolio");
                existed.Image.DeleteFile(_env.WebRootPath,"assets","img","portfolio");
                existed.Image = newImage;
            }

            existed.Title = productVM.Title;
            existed.Photo = productVM.Photo;
            existed.Category = productVM.Category;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if(id<=0) return BadRequest();

            Product product = await _context.Products.FirstOrDefaultAsync(p=>p.Id == id);
            if (product == null) return NotFound();

            product.Image.DeleteFile(_env.WebRootPath, "assets", "img", "portfolio");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return View(products);
        }
    }
}
