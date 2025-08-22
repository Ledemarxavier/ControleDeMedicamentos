using ControleDeMedicamentos.Dominio.Compartilhado;
using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.Dominio.ModuloPaciente;

public class Paciente : EntidadeBase<Paciente>
{
    public string Nome { get; set; }
    public string Telefone { get; set; }

    public string CartaoSUS { get; set; }
    public string Cpf { get; set; }

    protected Paciente()
    { }

    public Paciente(string nome, string telefone, string cartaosus, string cpf) : this()
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Telefone = telefone;
        CartaoSUS = cartaosus;
        Cpf = cpf;
    }

    public override void AtualizarRegistro(Paciente registroEditado)
    {
        Nome = registroEditado.Nome;
        Telefone = registroEditado.Telefone;
        CartaoSUS = registroEditado.CartaoSUS;
        Cpf = registroEditado.Cpf;
    }

    public override string Validar()
    {
        string erros = "";

        if (string.IsNullOrWhiteSpace(Nome))
            erros += "O campo 'Nome' é obrigatório.\n";
        else if (Nome.Length < 3 || Nome.Length > 100)
            erros += "O campo 'Nome' deve conter entre 3 e 100 caracteres.\n";

        if (string.IsNullOrWhiteSpace(Telefone))
            erros += "O campo 'Telefone' é obrigatório.\n";
        else if (!Regex.IsMatch(Telefone, @"^\(?\d{2}\)?\s?(9\d{4}|\d{4})-?\d{4}$"))
            erros += "O campo 'Telefone' deve seguir o padrão (DDD) 0000-0000 ou (DDD) 00000-0000.\n";

        if (string.IsNullOrWhiteSpace(CartaoSUS))
            erros += "O campo 'Cartão SUS' é obrigatório.\n";
        else if (!Regex.IsMatch(CartaoSUS, @"^\d{15}$"))
            erros += "O campo 'Cartão SUS' deve conter exatamente 15 dígitos numéricos.\n";

        if (string.IsNullOrWhiteSpace(Cpf))
            erros += "O campo 'CPF' é obrigatório.\n";
        else if (!Regex.IsMatch(Cpf, @"^\d{3}\.\d{3}\.\d{3}-\d{2}$"))
            erros += "O campo 'CPF' deve seguir o formato 000.000.000-00.\n";

        return erros;
    }
}