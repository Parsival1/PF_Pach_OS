using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PF_Pach_OS.Models;


namespace PF_Pach_OS.Controllers
{
    public class ComprasController : Controller
    {
        private Pach_OSContext context = new Pach_OSContext();
        public ComprasController(Pach_OSContext context)
        {
            this.context = context;
        }


        public IActionResult Index()
        {
            var compras = context.Compras
            .Join(context.Proveedores,
                c => c.IdProveedor,
                p => p.IdProveedor,
                (c, p) => new { Compra = c, Proveedor = p })
            .Join(context.DetallesCompras,
                cp => cp.Compra.IdCompra,
                oc => oc.IdCompra,
                (cp, oc) => new { CompraProveedor = cp, DetalleCompra = oc })
            .Join(context.Empleados,
                cpd => cpd.CompraProveedor.Compra.IdEmpleado,
                e => e.IdEmpleado,
                (cpd, e) => new { CompraProveedorDetalle = cpd, Empleado = e })
            .GroupBy(result => new
            {
                result.CompraProveedorDetalle.CompraProveedor.Compra.FechaCompra,
                result.CompraProveedorDetalle.CompraProveedor.Compra.Total,
                result.CompraProveedorDetalle.CompraProveedor.Proveedor.NomLocal,
                result.Empleado.Nombre,
                result.CompraProveedorDetalle.CompraProveedor.Compra.IdCompra
            })
            .Select(result => new
            {
                result.Key.FechaCompra,
                result.Key.Total,
                result.Key.NomLocal,
                result.Key.Nombre,
                result.Key.IdCompra,
                CantidadDetalles = result.Count()
            })
            .ToList();

            Console.WriteLine(compras.Count);



            ViewBag.Compras = compras;
            return View();
        }


        public async Task<IActionResult> Create(Compra compra)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("Aqui esta el error");
                var proveedor = await context.Proveedores.ToListAsync();
                var empleado = await context.Empleados.ToListAsync();
                if (proveedor.Count() > 0)
                {
                    Console.WriteLine("Aqui esta el error 2");
                    compra.IdProveedor = proveedor[0].IdProveedor;
                    compra.FechaCompra = DateTime.Parse(DateTime.Today.ToString("D"));
                    compra.IdEmpleado = empleado[0].IdEmpleado;
                    compra.Total = 0;
                    context.Add(compra);
                    context.SaveChanges();
                    return Redirect($"/DetalleCompra/Create/{compra.IdCompra}");
                }
                else
                {
                    Console.WriteLine("Aqui esta el error 3");
                    return RedirectToAction("Index");
                }

            }
            else
            {
                Console.WriteLine("Aqui esta el error 4");
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            var compradetalle = await context.Compras
                .Include(v => v.DetallesCompras)
                .FirstOrDefaultAsync(v => v.IdCompra == long.Parse(id));
            if (compradetalle == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var ordenes = compradetalle.DetallesCompras.ToList();
                if (compradetalle.DetallesCompras.Count() > 0)
                {
                    foreach (var orden in ordenes)
                    {
                        context.DetallesCompras.Remove(orden);
                        context.SaveChanges();
                    }
                    context.Compras.Remove(compradetalle);
                    context.SaveChanges();
                }
                else
                {
                    context.Compras.Remove(compradetalle);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }
    }
}
