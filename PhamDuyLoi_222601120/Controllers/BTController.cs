using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhamDuyLoi_222601120.Models;

namespace PhamDuyLoi_222601120.Controllers
{
    public class BTController : Controller
    {
        private readonly QlhangHoaContext _context;

        public BTController(QlhangHoaContext context)
        {
            _context = context;
        }
        public IActionResult Loc(int mid)
        {
            var listhang = _context.HangHoas.Where(s => s.MaLoai == mid).Include(s => s.MaLoaiNavigation).ToList();
            return PartialView("~/Views/BT/ProductTable.cshtml", listhang);

        }
        // GET: BT
        public async Task<IActionResult> Index()
        {
            var listhang = _context.HangHoas.Where(s => s.Gia >= 100).ToList();
            return View(listhang);
        }

        // GET: BT/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangHoa = await _context.HangHoas
                .Include(h => h.MaLoaiNavigation)
                .FirstOrDefaultAsync(m => m.MaHang == id);
            if (hangHoa == null)
            {
                return NotFound();
            }

            return View(hangHoa);
        }

        // GET: BT/Create
        public IActionResult Create()
        {
            ViewData["MaLoai"] = new SelectList(_context.LoaiHangs, "MaLoai", "TenLoai");
            return View();
        }

        // POST: BT/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHang,MaLoai,TenHang,Gia,Anh")] HangHoa hangHoa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hangHoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLoai"] = new SelectList(_context.LoaiHangs, "MaLoai", "TenLoai", hangHoa.MaLoai);
            return View(hangHoa);
        }

        // GET: BT/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangHoa = await _context.HangHoas.FindAsync(id);
            if (hangHoa == null)
            {
                return NotFound();
            }
            ViewData["MaLoai"] = new SelectList(_context.LoaiHangs, "MaLoai", "MaLoai", hangHoa.MaLoai);
            return View(hangHoa);
        }

        // POST: BT/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaHang,MaLoai,TenHang,Gia,Anh")] HangHoa hangHoa)
        {
            if (id != hangHoa.MaHang)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hangHoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HangHoaExists(hangHoa.MaHang))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLoai"] = new SelectList(_context.LoaiHangs, "MaLoai", "MaLoai", hangHoa.MaLoai);
            return View(hangHoa);
        }

        // GET: BT/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangHoa = await _context.HangHoas
                .Include(h => h.MaLoaiNavigation)
                .FirstOrDefaultAsync(m => m.MaHang == id);
            if (hangHoa == null)
            {
                return NotFound();
            }

            return View(hangHoa);
        }

        // POST: BT/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hangHoa = await _context.HangHoas.FindAsync(id);
            if (hangHoa != null)
            {
                _context.HangHoas.Remove(hangHoa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HangHoaExists(int id)
        {
            return _context.HangHoas.Any(e => e.MaHang == id);
        }
    }
}
