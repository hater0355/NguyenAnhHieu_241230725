using Microsoft.AspNetCore.Mvc;
using NguyenAnhHieu_241230725.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace NguyenAnhHieu_241230725.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
        public async Task<IActionResult> Index(int? maTl)
        {
            if(maTl.HasValue)
            {
                var loai = await _db.Set<TheLoai>().FirstOrDefaultAsync(x => x.MaTl == maTl.Value);
                if(loai == null)
                {
                    return NotFound();
                }
                ViewData["Title"] = loai.TenTl;
                var sachTheoLoai = await _db.Set<Sach>().Where(x => x.MaTl == maTl.Value && x.Gia.HasValue).ToListAsync();
                return View(sachTheoLoai);
            }
        }
        ViewData["Title"] = "Nguyễn Anh Hiếu";
        var ds = await _db.Set<Sach>().Where(x => x.Gia.HasValue && x.Gia.Value >=100).ToListAsync();
        return View(ds);
    }
}
