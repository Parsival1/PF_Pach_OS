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
    public class ComprasController : Controller
    {
        private readonly Pach_OSContext _context;

        public ComprasController(Pach_OSContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var compras = await _context.Compras.Include(c => c.DetallesCompras).ToListAsync();
            return View(compras);
        }


        public IActionResult Crear(int IdCompra)
        {
            var DetallesCompras = _context.DetallesCompras
                .Where(d => d.IdCompra == IdCompra)
                .ToList();

            ViewData["DetallesCompras"] = DetallesCompras;


            ViewBag.IdCompra = IdCompra;
            ViewData["IdInsumo"] = new SelectList(_context.Insumos, "IdInsumo", "NomInsumo");
            var proveedores = _context.Proveedores.ToList();
            ViewBag.Proveedores = new SelectList(proveedores, "IdProveedor", "NomLocal");
            return View();
        }

        public async Task<IActionResult> Create([Bind("IdCompra")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compra);
                await _context.SaveChangesAsync();



                return RedirectToAction("Crear", "Compras", new { compra.IdCompra });
            }
            return NotFound();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarCompra([Bind("IdCompra,FechaCompra,Total,IdProveedor")] Compra compra)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompraExists(compra.IdCompra))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Compras", new {compra.IdCompra});
            }
            return View(compra);
        }

        private bool CompraExists(int id)
        {
            return (_context.Compras?.Any(e => e.IdCompra == id)).GetValueOrDefault();
        }

    }
}
