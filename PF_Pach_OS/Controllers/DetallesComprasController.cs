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
    public class DetallesComprasController : Controller
    {
        private readonly Pach_OSContext _context;

        public DetallesComprasController(Pach_OSContext context)
        {
            _context = context;
        }

        // GET: DetallesCompras
        public async Task<IActionResult> Index()
        {
            var pach_OSContext = _context.DetallesCompras.Include(d => d.IdCompraNavigation).Include(d => d.IdInsumoNavigation);
            return View(await pach_OSContext.ToListAsync());
        }

        [HttpPost]
        public IActionResult GuardarCompra(List<DetallesCompra> compras)
        {
            foreach (var compra in compras)
            {
                DetallesCompra detalle = new();

                int? nombreInsumo = compra.IdInsumo;
                int? cantidad = compra.Cantidad;

                TempData["Mensaje"] = $"Productos agregados exitosamente {nombreInsumo} {cantidad}";
            }

            // Puedes retornar un JSON como respuesta si es necesario
            return NotFound();
        }

        // GET: DetallesCompras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DetallesCompras == null)
            {
                return NotFound();
            }

            var detallesCompra = await _context.DetallesCompras
                .Include(d => d.IdCompraNavigation)
                .Include(d => d.IdInsumoNavigation)
                .FirstOrDefaultAsync(m => m.IdDetallesCompra == id);
            if (detallesCompra == null)
            {
                return NotFound();
            }

            return View(detallesCompra);
        }

        // GET: DetallesCompras/Create
        public IActionResult Create()
        {
            ViewData["IdCompra"] = new SelectList(_context.Compras, "IdCompra", "IdCompra");
            ViewData["IdInsumo"] = new SelectList(_context.Insumos, "IdInsumo", "IdInsumo");
            return View();
        }

        // POST: DetallesCompras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDetallesCompra,PrecioInsumo,Cantidad,IdCompra,IdInsumo")] DetallesCompra detallesCompra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detallesCompra);
                await _context.SaveChangesAsync();
                return RedirectToAction("Crear","Compras", new {detallesCompra.IdCompra});
            }
            ViewData["IdCompra"] = new SelectList(_context.Compras, "IdCompra", "IdCompra", detallesCompra.IdCompra);
            ViewData["IdInsumo"] = new SelectList(_context.Insumos, "IdInsumo", "IdInsumo", detallesCompra.IdInsumo);
            return View(detallesCompra);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var detallesCompra = await _context.DetallesCompras.FindAsync(id);
            _context.DetallesCompras.Remove(detallesCompra);
            await _context.SaveChangesAsync();
            return RedirectToAction("Crear", "Compras", new { detallesCompra.IdCompra });
        }


        private bool DetallesCompraExists(int id)
        {
          return (_context.DetallesCompras?.Any(e => e.IdDetallesCompra == id)).GetValueOrDefault();
        }
    }
}
