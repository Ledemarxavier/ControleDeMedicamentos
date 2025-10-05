using ControleDeMedicamentos.Dominio.ModuloRequisicaoMedicamento;
using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFuncionario;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloMedicamento;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloPaciente;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloPrescricao;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloRequisicaoMedicamento;
using ControleDeMedicamentos.Infraestrutura.BancoDeDados.ModuloFuncionario;
using ControleDeMedicamentos.Infraestrutura.BancoDeDados.ModuloMedicamento;
using ControleDeMedicamentos.Infraestrutura.BancoDeDados.ModuloPrescricao;
using ControleDeMedicamentos.Infraestrutura.BancoDeDados.ModuloRequisicaoMedicamento;
using ControleDeMedicamentos.Infraestrutura.SqlServe.ModuloPaciente;
using ControleDeMedicamentos.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleDeMedicamentos.WebApp.Controllers;

public class RequisicaoMedicamentoController : Controller
{
	private readonly RepositorioRequisicaoMedicamentoEmBancoDeDados repositorioRequisicaoMedicamento;
	private readonly RepositorioMedicamentoEmBancoDeDados repositorioMedicamento;
	private readonly RepositorioFuncionarioEmBancoDeDados repositorioFuncionario;
	private readonly RepositorioPacienteEmBancoDeDados repositorioPaciente;
	private readonly RepositorioPrescricaoEmBancoDeDados repositorioPrescricao;

	public RequisicaoMedicamentoController(
		ContextoDados contexto,
		RepositorioRequisicaoMedicamentoEmBancoDeDados repositorioRequisicaoMedicamento,
		RepositorioMedicamentoEmBancoDeDados repositorioMedicamento,
		RepositorioFuncionarioEmBancoDeDados repositorioFuncionario,
		 RepositorioPacienteEmBancoDeDados repositorioPaciente,
		RepositorioPrescricaoEmBancoDeDados repositorioPrescricao
	)
	{
		this.repositorioRequisicaoMedicamento = repositorioRequisicaoMedicamento;
		this.repositorioMedicamento = repositorioMedicamento;
		this.repositorioFuncionario = repositorioFuncionario;
		this.repositorioPaciente = repositorioPaciente;
		this.repositorioPrescricao = repositorioPrescricao;
	}

	[HttpGet]
	public IActionResult Index()
	{
		var requisicoesEntrada = repositorioRequisicaoMedicamento.SelecionarRequisicoesEntrada();
		var requisicoesSaida = repositorioRequisicaoMedicamento.SelecionarRequisicoesSaida();

		var visualizarVm = new VisualizarRequisicoesMedicamentoViewModel(requisicoesEntrada, requisicoesSaida);

		return View(visualizarVm);
	}

	[HttpGet]
	public IActionResult CadastrarRequisicaoEntrada()
	{
		var medicamentosDisponiveis = repositorioMedicamento.SelecionarRegistros();
		var funcionariosDisponiveis = repositorioFuncionario.SelecionarRegistros();

		var cadastrarVm = new CadastrarRequisicaoEntradaViewModel(medicamentosDisponiveis, funcionariosDisponiveis);

		return View(cadastrarVm);
	}

	[HttpPost]
	public IActionResult CadastrarRequisicaoEntrada(CadastrarRequisicaoEntradaViewModel cadastrarVm)
	{
		if (!ModelState.IsValid)
		{
			var medicamentosDisponiveis = repositorioMedicamento.SelecionarRegistros();

			cadastrarVm.MedicamentosDisponiveis = medicamentosDisponiveis
				.Select(m => new SelectListItem(m.Nome, m.Id.ToString()))
				.ToList();

			var funcionariosDisponiveis = repositorioFuncionario.SelecionarRegistros();

			cadastrarVm.FuncionariosDisponiveis = funcionariosDisponiveis
				.Select(f => new SelectListItem(f.Nome, f.Id.ToString()))
				.ToList();

			return View(cadastrarVm);
		}

		var funcionarioSelecionado = repositorioFuncionario.SelecionarRegistroPorId(cadastrarVm.FuncionarioId);

		var medicamentoSelecionado = repositorioMedicamento.SelecionarRegistroPorId(cadastrarVm.MedicamentoId);

		var requisicaoEntrada = new RequisicaoEntrada(
			funcionarioSelecionado,
			medicamentoSelecionado,
			cadastrarVm.QuantidadeRequisitada
		);

		medicamentoSelecionado.AdicionarAoEstoque(requisicaoEntrada);

		repositorioRequisicaoMedicamento.CadastrarRequisicaoEntrada(requisicaoEntrada);

		return RedirectToAction(nameof(Index));
	}

	[HttpGet]
	public IActionResult PrimeiraEtapaCadastrarRequisicaoSaida()
	{
		var funcionariosDisponiveis = repositorioFuncionario.SelecionarRegistros();

		var cadastrarVm = new PrimeiraEtapaCadastrarRequisicaoSaidaViewModel(funcionariosDisponiveis);

		return View(cadastrarVm);
	}

	[HttpPost]
	public IActionResult PrimeiraEtapaCadastrarRequisicaoSaida(PrimeiraEtapaCadastrarRequisicaoSaidaViewModel cadastrarVm)
	{
		var funcionarioSelecionado = repositorioFuncionario.SelecionarRegistroPorId(cadastrarVm.FuncionarioId);
		var pacienteSelecionado = repositorioPaciente.SelecionarPacientePorCpf(cadastrarVm.CpfPaciente);

		var prescricoesDoPaciente = repositorioPrescricao.SelecionarPrescricoesDoPaciente(pacienteSelecionado!.Id);

		var segundaEtapaVm = new SegundaEtapaCadastrarRequisicaoSaidaViewModel(
			cadastrarVm.FuncionarioId,
			funcionarioSelecionado.Nome,
			pacienteSelecionado!.Nome,
			prescricoesDoPaciente
		);

		return View(nameof(SegundaEtapaCadastrarRequisicaoSaida), segundaEtapaVm);
	}

	[HttpPost]
	public IActionResult SegundaEtapaCadastrarRequisicaoSaida(Guid idFuncionario, Guid idPrescricao)
	{
		var funcionarioSelecionado = repositorioFuncionario.SelecionarRegistroPorId(idFuncionario);

		var prescricaoSelecionada = repositorioPrescricao.SelecionarRegistroPorId(idPrescricao);

		var ultimaEtapaVm = new UltimaEtapaCadastrarRequisicaoSaidaViewModel(
			idFuncionario,
			funcionarioSelecionado.Nome,
			idPrescricao,
			prescricaoSelecionada.Descricao,
			prescricaoSelecionada.Paciente.Nome,
			prescricaoSelecionada.MedicamentosPrescritos
		);

		return View(nameof(UltimaEtapaCadastrarRequisicaoSaida), ultimaEtapaVm);
	}

	[HttpPost]
	public IActionResult UltimaEtapaCadastrarRequisicaoSaida(UltimaEtapaCadastrarRequisicaoSaidaViewModel ultimaEtapaVm)
	{
		var funcionarioSelecionado = repositorioFuncionario.SelecionarRegistroPorId(ultimaEtapaVm.FuncionarioId);

		var prescricaoSelecionada = repositorioPrescricao.SelecionarRegistroPorId(ultimaEtapaVm.PrescricaoId);

		var requisicaoSaida = new RequisicaoSaida(funcionarioSelecionado, prescricaoSelecionada);

		foreach (var mp in prescricaoSelecionada.MedicamentosPrescritos)
		{
			var medicamento = mp.Medicamento;

			medicamento.RemoverDoEstoque(requisicaoSaida);
		}

		repositorioRequisicaoMedicamento.CadastrarRequisicaoSaida(requisicaoSaida);

		return RedirectToAction(nameof(Index));
	}

	[HttpGet]
	public IActionResult DetalhesRequisicaoEntrada(Guid id)
	{
		var requisicao = repositorioRequisicaoMedicamento.SelecionarRequisicaoEntradaPorId(id);

		if (requisicao == null)
			return NotFound();

		var detalhesVm = new DetalhesRequisicaoEntradaViewModel(
			requisicao.Id,
			requisicao.DataOcorrencia,
			requisicao.Funcionario.Nome,
			requisicao.Medicamento.Nome,
			requisicao.QuantidadeRequisitada
		);

		return View(detalhesVm);
	}

	[HttpGet]
	public IActionResult DetalhesRequisicaoSaida(Guid id)
	{
		var requisicao = repositorioRequisicaoMedicamento.SelecionarRequisicaoSaidaPorId(id);

		if (requisicao?.Prescricao == null || requisicao.Prescricao.Paciente == null)
			return BadRequest("Prescrição ou paciente não encontrados para esta requisição.");


        var detalhesVm = new DetalhesRequisicaoSaidaViewModel(
			requisicao.Id,
			requisicao.DataOcorrencia,
			requisicao.Funcionario.Nome,
			requisicao.Prescricao.Paciente.Nome,
			requisicao.Prescricao.Descricao,
			requisicao.Prescricao.MedicamentosPrescritos
		);

		return View(detalhesVm);
	}
}