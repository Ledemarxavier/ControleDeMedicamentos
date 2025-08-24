using ControleDeMedicamentos.Dominio.ModuloMedicamento;
using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloMedicamento;

public class RepositorioMedicamentoEmArquivo : RepositorioBaseEmArquivo<Medicamento>
{
    public RepositorioMedicamentoEmArquivo(ContextoDados contextoDados) : base(contextoDados)
    {
    }

    protected override List<Medicamento> ObterRegistros()
    {
        return contextoDados.Medicamentos;
    }
}