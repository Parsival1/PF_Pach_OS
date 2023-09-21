using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PF_Pach_OS.Models;

namespace PF_Pach_OS.Controllers
{
    public class InsumosController : Controller
    {
        private readonly Pach_OSContext _context;

        public InsumosController(Pach_OSContext context)
        {
            _context = context;
        }

        // GET: Insumos
        public async Task<IActionResult> Index()
        {
              return _context.Insumos != null ? 
                          View(await _context.Insumos.ToListAsync()) :
                          Problem("Entity set 'Pach_OSContext.Insumos'  is null.");
        }


        // GET: Insumos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Insumos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInsumo,NomInsumo,CantInsumo,Medida")] Insumo insumo)
        {
            if (ModelState.IsValid)
            {

                if (insumo.Medida == "Gramo")
                {
                    insumo.CantInsumo = insumo.CantInsumo;
                    insumo.Medida = "Gramo";
                }
                else if (insumo.Medida == "Mililitro")
                {
                    insumo.CantInsumo += insumo.CantInsumo;
                    insumo.Medida = "Mililitro";
                }
                else if (insumo.Medida == "Onza")
                {
                    var convercion = insumo.CantInsumo * 30;
                    insumo.CantInsumo = convercion;
                    insumo.Medida = "Mililitro";
                }
                else if (insumo.Medida == "Unidad")
                {
                    insumo.CantInsumo = insumo.CantInsumo;
                    insumo.Medida = "Unidad";
                }

                _context.Add(insumo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult NombreDuplicado(string Nombre)
        {
            var isDuplicate = _context.Insumos.Any(x => x.NomInsumo == Nombre);
            return Json(isDuplicate);
        }


        // GET: Insumos/Edit/5
        public async Task<IActionResult> Editar(int? id)
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

        // POST: Insumos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("IdInsumo,NomInsumo,CantInsumo,Medida")] Insumo insumo)
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

        private bool InsumoExists(int id)
        {
          return (_context.Insumos?.Any(e => e.IdInsumo == id)).GetValueOrDefault();
        }
    }
}
