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
    public class RecetasController : Controller
    {
        private readonly Pach_OSContext _context;

        public RecetasController(Pach_OSContext context)
        {
            _context = context;
        }

        // GET: Recetas
        public async Task<IActionResult> Index()
        {
            var pach_OSContext = _context.Recetas.Include(r => r.IdInsumoNavigation).Include(r => r.IdProductoNavigation);
            return View(await pach_OSContext.ToListAsync());
        }

        // GET: Recetas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Recetas == null)
            {
                return NotFound();
            }

            var receta = await _context.Recetas
                .Include(r => r.IdInsumoNavigation)
                .Include(r => r.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdReceta == id);
            if (receta == null)
            {
                return NotFound();
            }

            return View(receta);
        }

        // GET: Recetas/Create
        public IActionResult Create()
        {
            ViewData["IdInsumo"] = new SelectList(_context.Insumos, "IdInsumo", "IdInsumo");
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto");
            return View();
        }

        // POST: Recetas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CantInsumo,IdProducto,IdInsumo")] Receta receta, [Bind("IdProducto,NomProducto,PrecioVenta,Estado,IdTamano,IdCategoria")] Producto producto)
        {
            

            
            if (ModelState.IsValid)

            {
                try
                {

                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.IdProducto))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                _context.Add(receta);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Productos", new { receta.IdProducto });
            }
            ViewData["IdInsumo"] = new SelectList(_context.Insumos, "IdInsumo", "IdInsumo", receta.IdInsumo);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", receta.IdProducto);
            return RedirectToAction("Details", "Productos", new { receta.IdProducto });
        }

        // GET: Recetas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Recetas == null)
            {
                return NotFound();
            }

            var receta = await _context.Recetas.FindAsync(id);
            if (receta == null)
            {
                return NotFound();
            }
            ViewData["IdInsumo"] = new SelectList(_context.Insumos, "IdInsumo", "IdInsumo", receta.IdInsumo);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", receta.IdProducto);
            return View(receta);
        }

        // POST: Recetas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdReceta,CantInsumo,IdProducto,IdInsumo")] Receta receta)
        {
            if (id != receta.IdReceta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecetaExists(receta.IdReceta))
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
            ViewData["IdInsumo"] = new SelectList(_context.Insumos, "IdInsumo", "IdInsumo", receta.IdInsumo);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", receta.IdProducto);
            return View(receta);
        }

        // GET: Recetas/Delete/5
        public async Task<IActionResult> Delete(int? id, int IdProducto)
        {
            if (id == null || _context.Recetas == null)
            {
                return NotFound();
            }

            var receta = await _context.Recetas
                .Include(r => r.IdInsumoNavigation)
                .Include(r => r.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdReceta == id);
            if (receta == null)
            {
                return NotFound();
            }

            return View(receta);
        }

        // POST: Recetas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int IdProducto)
        {
            if (_context.Recetas == null)
            {
                return Problem("Entity set 'Pach_OSContext.Recetas'  is null.");
            }
            var receta = await _context.Recetas.FindAsync(id);
            if (receta != null)
            {
                _context.Recetas.Remove(receta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Productos", new { IdProducto });
        }

        private bool RecetaExists(int id)
        {
          return (_context.Recetas?.Any(e => e.IdReceta == id)).GetValueOrDefault();
        }
        private bool ProductoExists(int id)
        {
            return (_context.Productos?.Any(e => e.IdProducto == id)).GetValueOrDefault();
        }
    }
}
