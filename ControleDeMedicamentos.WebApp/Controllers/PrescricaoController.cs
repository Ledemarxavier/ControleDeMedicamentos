using ControleDeMedicamentos.Dominio.ModuloPrescricao;
using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloMedicamento;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloPaciente;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloPrescricao;
using ControleDeMedicamentos.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleDeMedicamentos.WebApp.Controllers;

public class PrescricaoController : Controller
{
    private readonly ContextoDados contexto;
    private readonly RepositorioPrescricaoEmArquivo repositorioPrescricao;
    private readonly RepositorioMedicamentoEmArquivo repositorioMedicamento;
    private readonly RepositorioPacienteEmArquivo repositorioPaciente;

    public PrescricaoController(
        ContextoDados contexto,
        RepositorioPrescricaoEmArquivo repositorioPrescricao,
        RepositorioMedicamentoEmArquivo repositorioMedicamento,
        RepositorioPacienteEmArquivo repositorioPaciente
    )
    {
        this.contexto = contexto;
        this.repositorioPrescricao = repositorioPrescricao;
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioPaciente = repositorioPaciente;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var prescricoes = repositorioPrescricao.SelecionarRegistros();

        var visualizarVm = new VisualizarPrescricoesViewModel(prescricoes);

        return View(visualizarVm);
    }

    [HttpGet]
    public IActionResult Cadastrar()
    {
        var pacientesDisponiveis = repositorioPaciente.SelecionarRegistros();

        var cadastrarVm = new CadastrarPrescricaoViewModel(pacientesDisponiveis);

        return View(cadastrarVm);
    }

    [HttpPost]
    public IActionResult Cadastrar(CadastrarPrescricaoViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
        {
            var pacientesDisponiveis = repositorioPaciente.SelecionarRegistros();

            cadastrarVm.PacientesDisponiveis = pacientesDisponiveis
                .Select(p => new SelectListItem(p.Nome, p.Id.ToString()))
                .ToList();

            return View(cadastrarVm);
        }

        var pacienteSelecionado = repositorioPaciente.SelecionarRegistroPorId(cadastrarVm.PacienteId);

        var entidade = new Prescricao(
            cadastrarVm.Descricao,
            cadastrarVm.DataValidade,
            cadastrarVm.CrmMedico,
            pacienteSelecionado
        );

        repositorioPrescricao.CadastrarRegistro(entidade);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Editar(Guid id)
    {
        var registro = repositorioPrescricao.SelecionarRegistroPorId(id);

        var pacientesDisponiveis = repositorioPaciente.SelecionarRegistros();

        var editarVm = new EditarPrescricaoViewModel(
            registro.Id,
            registro.Descricao,
            registro.DataValidade,
            registro.CrmMedico,
            registro.Paciente.Id,
            pacientesDisponiveis
        );

        return View(editarVm);
    }

    [HttpPost]
    public IActionResult Editar(EditarPrescricaoViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        var pacienteSelecionado = repositorioPaciente.SelecionarRegistroPorId(editarVm.PacienteId);

        var PrescricaoEditada = new Prescricao(
            editarVm.Descricao,
            editarVm.DataValidade,
            editarVm.CrmMedico,
            pacienteSelecionado
        );

        repositorioPrescricao.EditarRegistro(editarVm.Id, PrescricaoEditada);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Excluir(Guid id)
    {
        var registro = repositorioPrescricao.SelecionarRegistroPorId(id);

        var excluirVm = new ExcluirPrescricaoViewModel(
            registro.Id,
            registro.Descricao
        );

        return View(excluirVm);
    }

    [HttpPost]
    public IActionResult Excluir(ExcluirPrescricaoViewModel excluirVm)
    {
        repositorioPrescricao.ExcluirRegistro(excluirVm.Id);

        return RedirectToAction(nameof(Index));
    }
}