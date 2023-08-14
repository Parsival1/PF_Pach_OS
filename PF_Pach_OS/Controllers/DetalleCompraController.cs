﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PF_Pach_OS.Models;

namespace PF_Pach_OS.Controllers
{
    public class DetalleCompraController : Controller
    {
        private Pach_OSContext context = new Pach_OSContext();
        public DetalleCompraController(Pach_OSContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create(int id)
        {
            
           
            string? _urlData = HttpContext.Request.Path.Value;
            string[] splitData = _urlData.Split('/');
            var IdCompra = int.Parse(splitData[splitData.Length - 1]);
            ViewBag.IdCompra = IdCompra;

            var detallescompras = context.DetallesCompras
                .Where(o => o.IdCompra == IdCompra)
                .Join(
                    context.Insumos,
                    Detalle => Detalle.IdInsumo,
                    insumo => insumo.IdInsumo,
                    (Detalle, insumo) => new
                    {
                        insumo.NomInsumo,
                        Detalle.Cantidad,
                        Detalle.PrecioInsumo,
                        Detalle.Medida,
                        Detalle.IdDetallesCompra,
                        Detalle.IdCompra,
                        Total = Detalle.Cantidad * Detalle.PrecioInsumo
                    })
                .ToList();

            
            ViewBag.Detalles = detallescompras;
            ViewBag.Insumos = await context.Insumos.Select(x => new { x.IdInsumo, x.NomInsumo }).ToListAsync();
            ViewBag.Proveedores = await context.Proveedores.Select(x => new { x.IdProveedor, x.NomLocal }).ToListAsync();
            ViewBag.Empleados = await context.Empleados.Select(x => new { x.IdEmpleado, x.Nombre }).ToListAsync();
            Tuple<DetallesCompra, Compra> models = new Tuple<DetallesCompra, Compra>(new DetallesCompra(), new Compra());
            return View(models);
        }

        [HttpPost]
        public IActionResult Create([Bind(Prefix = "Item1")] DetallesCompra detallecompra, [Bind(Prefix = "Item2")] Compra compra, Insumo insumo)
        {
            if (detallecompra.IdInsumo != null && detallecompra.Cantidad != null)
            {
                var insummo = context.Insumos.Where(x => x.IdInsumo == detallecompra.IdInsumo).FirstOrDefault()!;
                compra.Total = detallecompra.PrecioInsumo * detallecompra.Cantidad;
                context.Add(detallecompra);
                context.SaveChanges();

                var insumoo = context.Insumos.FirstOrDefault(i => i.IdInsumo == detallecompra.IdInsumo);
                if (insumoo != null)
                {
                    insumoo.CantInsumo += detallecompra.Cantidad;
                    context.Update(insumoo);
                    context.SaveChanges();
                }

                return Redirect($"/DetalleCompra/Create/{detallecompra.IdCompra}");
            }
            else
            {
                ViewData["MessageInsumo"] = "Debe ingresar un insumo";
                ViewData["MessageCantidad"] = "Debe ingrsar la cantidad";
                return Redirect($"/DetalleCompra/Create/{detallecompra.IdCompra}");
            }
        }

        public IActionResult ConfirmSale([Bind(Prefix = "Item2")] Compra compra)
        {
            var Compra = context.Compras.Where(x => x.IdCompra == compra.IdCompra).FirstOrDefault()!;
            if (ModelState.IsValid)
            {
                if (compra.Total == 0)
                {
                    return Redirect($"/DetalleCompra/Create/{compra.IdCompra}");
                }
                Compra.IdProveedor = compra.IdProveedor;
                Compra.IdEmpleado = compra.IdEmpleado;
                Compra.Total = compra.Total;
                context.Update(Compra);
                context.SaveChanges();
                return Redirect("/Compras");
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> Details()
        {
            
            
            string? _urlData = HttpContext.Request.Path.Value;
            string[] splitData = _urlData.Split('/');
            var IdCompra = int.Parse(splitData[splitData.Length - 1]);

            var Ordenes = context.DetallesCompras
                .Where(o => o.IdCompra == IdCompra)
                .Join(
                    context.Insumos,
                    orden => orden.IdInsumo,
                    insumo => insumo.IdInsumo,
                    (orden, insumo) => new
                    {
                        insumo.NomInsumo,
                        orden.Cantidad,
                        orden.PrecioInsumo,
                        orden.Medida,
                        Total = orden.Cantidad*orden.PrecioInsumo,
                        orden.IdDetallesCompra,
                        orden.IdCompra
                    }).ToList();
            var compra = context.Compras.Where(o => o.IdCompra == IdCompra).Select(x => new { x.IdCompra, x.IdProveedor,x.IdEmpleado, x.FechaCompra, x.Total }).ToList();
            ViewBag.Detalles = Ordenes;
            ViewBag.IdCompra = IdCompra;
            foreach (var item in compra)
            {
                ViewBag.TotalCompra = item.Total;
            }

            return View();
            
        }

        public async Task<IActionResult> Delete(String id, int otroId)
        {
            var orden = await context.DetallesCompras.FindAsync(int.Parse(id));
            if (orden == null)
            {
                return Redirect($"/DetalleCompra/Create/{otroId}");
            }
            else
            {
                context.DetallesCompras.Remove(orden);
                context.SaveChanges();
                return Redirect($"/DetalleCompra/Create/{otroId}");
            }
        }
    }
}