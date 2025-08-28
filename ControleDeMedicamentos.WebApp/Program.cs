using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFornecedor;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFuncionario;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloPaciente;
using Serilog.Events;
using Serilog;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloMedicamento;
using ControleDeMedicamentos.WebApp.DependencyInjection;

namespace ControleDeMedicamentos.WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Inje��o de depend�ncias

        builder.Services.AddScoped((_) => new ContextoDados(true));

        builder.Services.AddScoped<RepositorioFornecedorEmArquivo>();
        builder.Services.AddScoped<RepositorioFuncionarioEmArquivo>();          // Injeta uma inst�ncia do servi�o por requisi��o (a��o) HTTP, essa inst�ncia acompanha a requisi��o do cliente
        builder.Services.AddScoped<RepositorioPacienteEmArquivo>();
        builder.Services.AddScoped<RepositorioMedicamentoEmArquivo>();

        SerilogConfig.AddSerilogConfig(builder.Services, builder.Logging, builder.Configuration);

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