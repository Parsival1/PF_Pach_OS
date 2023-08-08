using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PF_Pach_OS.Models;

namespace PF_Pach_OS.Controllers
{
    public class InsumoesController : Controller
    {
        private readonly Pach_OSContext _context;

        public InsumoesController(Pach_OSContext context)
        {
            _context = context;
        }

        // GET: Insumoes
        public async Task<IActionResult> Index()
        {
              return _context.Insumos != null ? 
                          View(await _context.Insumos.ToListAsync()) :
                          Problem("Entity set 'Pach_OSContext.Insumos'  is null.");
        }

        // GET: Insumoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Insumos == null)
            {
                return NotFound();
            }

            var insumo = await _context.Insumos
                .FirstOrDefaultAsync(m => m.IdInsumo == id);
            if (insumo == null)
            {
                return NotFound();
            }

            return View(insumo);
        }

        // GET: Insumoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Insumoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInsumo,NomInsumo,CantInsumo,Medida")] Insumo insumo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insumo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insumo);
        }

        // GET: Insumoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Insumos == null)
            {
                return NotFound();
            }

            var insumo = await _context.Insumos.FindAsync(id);
            if (insumo == null)
            {
                return NotFound();
            }
            return View(insumo);
        }

        // POST: Insumoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdInsumo,NomInsumo,CantInsumo,Medida")] Insumo insumo)
        {
            if (id != insumo.IdInsumo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insumo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsumoExists(insumo.IdInsumo))
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
            return View(insumo);
        }

        // GET: Insumoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Insumos == null)
            {
                return NotFound();
            }

            var insumo = await _context.Insumos
                .FirstOrDefaultAsync(m => m.IdInsumo == id);
            if (insumo == null)
            {
                return NotFound();
            }

            return View(insumo);
        }

        // POST: Insumoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Insumos == null)
            {
                return Problem("Entity set 'Pach_OSContext.Insumos'  is null.");
            }
            var insumo = await _context.Insumos.FindAsync(id);
            if (insumo != null)
            {
                _context.Insumos.Remove(insumo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsumoExists(int id)
        {
          return (_context.Insumos?.Any(e => e.IdInsumo == id)).GetValueOrDefault();
        }
    }
}
