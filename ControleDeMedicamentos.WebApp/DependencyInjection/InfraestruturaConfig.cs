using ControleDeMedicamentos.Dominio.ModuloFornecedor;
using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using ControleDeMedicamentos.Dominio.ModuloMedicamento;
using ControleDeMedicamentos.Dominio.ModuloPaciente;
using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFornecedor;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFuncionario;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloMedicamento;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloPaciente;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloPrescricao;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloRequisicaoMedicamento;
using ControleDeMedicamentos.Infraestrutura.BancoDeDados.ModuloFornecedor;
using ControleDeMedicamentos.Infraestrutura.BancoDeDados.ModuloFuncionario;
using ControleDeMedicamentos.Infraestrutura.BancoDeDados.ModuloMedicamento;
using ControleDeMedicamentos.Infraestrutura.BancoDeDados.ModuloPrescricao;
using ControleDeMedicamentos.Infraestrutura.SqlServe.ModuloPaciente;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ControleDeMedicamentos.WebApp.DependencyInjection;

public static class InfraestruturaConfig
{
    public static void AddCamadaInfraestrutura(this IServiceCollection services, IConfiguration configuracao)
    {
        services.AddScoped<IDbConnection>(opt =>
        {
            var connectionString = configuracao["SQL_CONNECTION_STRING"];

            return new SqlConnection(connectionString);
        });

        services.AddScoped<RepositorioFornecedorEmBancoDeDados>();
        services.AddScoped<RepositorioPacienteEmBancoDeDados>();
        services.AddScoped<RepositorioFuncionarioEmBancoDeDados>();
        services.AddScoped<RepositorioMedicamentoEmBancoDeDados>();
        services.AddScoped<RepositorioPrescricaoEmBancoDeDados>();

        services.AddScoped((_) => new ContextoDados(true));

        services.AddScoped<RepositorioRequisicaoMedicamentoEmArquivo>();
    }
}