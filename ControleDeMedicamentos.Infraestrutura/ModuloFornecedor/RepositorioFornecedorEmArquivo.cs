using ControleDeMedicamentos.Dominio.ModuloFornecedor;
using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFornecedor;

public class RepositorioFornecedorEmArquivo : RepositorioBaseEmArquivo<Fornecedor>
{
    public RepositorioFornecedorEmArquivo(ContextoDados contextoDados) : base(contextoDados)
    {
    }

    protected override List<Fornecedor> ObterRegistros()
    {
        return contextoDados.Fornecedores;
    }
}