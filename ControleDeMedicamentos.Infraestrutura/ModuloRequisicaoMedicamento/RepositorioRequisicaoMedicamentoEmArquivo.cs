using ControleDeMedicamentos.Dominio.ModuloRequisicaoMedicamento;
using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloRequisicaoMedicamento;

public class RepositorioRequisicaoMedicamentoEmArquivo
{
    private readonly ContextoDados contexto;
    private readonly List<RequisicaoEntrada> requisicoesEntrada;

    public RepositorioRequisicaoMedicamentoEmArquivo(ContextoDados contexto)
    {
        this.contexto = contexto;

        requisicoesEntrada = contexto.RequisicoesEntrada;
    }

    public void CadastrarRequisicaoEntrada(RequisicaoEntrada requisicao)
    {
        requisicoesEntrada.Add(requisicao);

        contexto.Salvar();
    }

    public List<RequisicaoEntrada> SelecionarRequisicoesEntrada()
    {
        return requisicoesEntrada;
    }
}