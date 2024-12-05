using Microsoft.AspNetCore.Mvc;
using PhamDuyLoi_222601120.Models;

namespace PhamDuyLoi_222601120.ViewComponents
{
    public class NavViewComponent:ViewComponent
    {
        private readonly QlhangHoaContext _context;

        public NavViewComponent(QlhangHoaContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var listloai = _context.LoaiHangs.ToList();
            return View("RenderNav", listloai);
        }
    }
}
