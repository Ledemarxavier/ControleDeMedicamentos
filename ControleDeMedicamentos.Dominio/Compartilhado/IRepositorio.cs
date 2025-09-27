using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.Dominio.Compartilhado;

public interface IRepositorio<T> where T : EntidadeBase<T>
{
    public void CadastrarRegistro(T novoRegistro);

    public bool EditarRegistro(Guid idSelecionado, T registroAtualizado);

    public bool ExcluirRegistro(Guid idSelecionado);

    public List<T> SelecionarRegistros();

    public T? SelecionarRegistroPorId(Guid idSelecionado);
}