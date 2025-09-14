using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using ControleDeMedicamentos.Dominio.ModuloPaciente;
using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloPaciente;

public class RepositorioPacienteEmArquivo : RepositorioBaseEmArquivo<Paciente>
{
    public RepositorioPacienteEmArquivo(ContextoDados contextoDados) : base(contextoDados)
    {
    }

    public Paciente? SelecionarPacientePorCpf(string cpf)
    {
        return registros.Find(p => p.Cpf == cpf);
    }

    protected override List<Paciente> ObterRegistros()
    {
        return contextoDados.Pacientes;
    }
}