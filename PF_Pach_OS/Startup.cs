using Microsoft.AspNetCore.Builder;

namespace PF_Pach_OS
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "guardarCompra",
                    pattern: "DetallesCompras/GuardarCompra",
                    defaults: new { controller = "DetallesCompras", action = "GuardarCompra" });
            });
        }
    }
}
