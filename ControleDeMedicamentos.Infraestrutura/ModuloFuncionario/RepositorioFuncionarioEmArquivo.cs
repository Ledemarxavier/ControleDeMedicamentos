using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFuncionario;

public class RepositorioFuncionarioEmArquivo : RepositorioBaseEmArquivo<Funcionario>
{
	public RepositorioFuncionarioEmArquivo(ContextoDados contextoDados) : base(contextoDados)
	{
	}

	protected override List<Funcionario> ObterRegistros()
	{
		return contextoDados.Funcionarios;
	}
}