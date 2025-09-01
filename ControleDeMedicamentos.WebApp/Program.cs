using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;

using ControleDeMedicamentos.WebApp.DependencyInjection;

namespace ControleDeMedicamentos.WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Injeção de dependências

        builder.Services.AddScoped((_) => new ContextoDados(true));

        builder.Services.AddCamadaInfraestrutura();

        builder.Services.AddSerilogConfig(builder.Logging, builder.Configuration);

        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");

            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}