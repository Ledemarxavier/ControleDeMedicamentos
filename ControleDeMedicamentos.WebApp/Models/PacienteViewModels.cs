using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using ControleDeMedicamentos.Dominio.ModuloPaciente;
using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentos.WebApp.Models;

public class CadastrarPacienteViewModel
{
    [Required(ErrorMessage = "O campo 'Nome' é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo 'Nome' deve conter entre 3 e 100 caracteres.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo 'Telefone' é obrigatório.")]
    [RegularExpression(
        @"^\(?\d{2}\)?\s?(9\d{4}|\d{4})-?\d{4}$",
        ErrorMessage = "O campo 'Telefone' deve seguir o padrão (DDD) 0000-0000 ou (DDD) 00000-0000."
    )]
    public string Telefone { get; set; }

    [Required(ErrorMessage = "O campo 'Cartão SUS' é obrigatório.")]
    [RegularExpression(
        @"^\d{15}$",
        ErrorMessage = "O campo 'Cartão SUS' deve conter exatamente 15 dígitos numéricos."
    )]
    public string CartaoSUS { get; set; }

    [Required(ErrorMessage = "O campo 'CPF' é obrigatório.")]
    [RegularExpression(
        @"^\d{3}\.\d{3}\.\d{3}-\d{2}$",
        ErrorMessage = "O campo 'CPF' deve seguir o formato 000.000.000-00."
    )]
    public string Cpf { get; set; }

    public CadastrarPacienteViewModel()
    { }

    public CadastrarPacienteViewModel(string nome, string telefone, string cartaosus, string cpf) : this()
    {
        Nome = nome;
        Telefone = telefone;
        CartaoSUS = cartaosus;
        Cpf = cpf;
    }
}

public class EditarPacienteViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo 'Nome' é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo 'Nome' deve conter entre 3 e 100 caracteres.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo 'Telefone' é obrigatório.")]
    [RegularExpression(
        @"^\(?\d{2}\)?\s?(9\d{4}|\d{4})-?\d{4}$",
        ErrorMessage = "O campo 'Telefone' deve seguir o padrão (DDD) 0000-0000 ou (DDD) 00000-0000."
    )]
    public string Telefone { get; set; }

    [Required(ErrorMessage = "O campo 'Cartão SUS' é obrigatório.")]
    [RegularExpression(
        @"^\d{15}$",
        ErrorMessage = "O campo 'Cartão SUS' deve conter exatamente 15 dígitos numéricos."
    )]
    public string CartaoSUS { get; set; }

    [Required(ErrorMessage = "O campo 'CPF' é obrigatório.")]
    [RegularExpression(
        @"^\d{3}\.\d{3}\.\d{3}-\d{2}$",
        ErrorMessage = "O campo 'CPF' deve seguir o formato 000.000.000-00."
    )]
    public string Cpf { get; set; }

    public EditarPacienteViewModel()
    { }

    public EditarPacienteViewModel(Guid id, string nome, string telefone, string cartaosus, string cpf) : this()
    {
        Id = id;
        Nome = nome;
        Telefone = telefone;
        CartaoSUS = cartaosus;
        Cpf = cpf;
    }
}

public class ExcluirPacienteViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }

    public ExcluirPacienteViewModel()
    { }

    public ExcluirPacienteViewModel(Guid id, string nome) : this()
    {
        Id = id;
        Nome = nome;
    }
}

public class VisualizarPacientesViewModel
{
    public List<DetalhesPacienteViewModel> Registros { get; }

    public VisualizarPacientesViewModel(List<Paciente> pacientes)
    {
        Registros = [];

        foreach (var p in pacientes)
        {
            var detalhesVM = new DetalhesPacienteViewModel(
                p.Id,
                p.Nome,
                p.Telefone,
                p.CartaoSUS,
                p.Cpf
            );

            Registros.Add(detalhesVM);
        }
    }
}

public class DetalhesPacienteViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Telefone { get; set; }

    public string CartaoSUS { get; set; }
    public string Cpf { get; set; }

    public DetalhesPacienteViewModel(Guid id, string nome, string telefone, string cartaosus, string cpf)
    {
        Id = id;
        Nome = nome;
        Telefone = telefone;
        CartaoSUS = cartaosus;
        Cpf = cpf;
    }
}