namespace DominioKioscos;

public interface IRepositorioKioscos
{
    void Guardar(Kiosco  kiosco);
    Kiosco? BuscarPorCodigo(string codigo);
    IReadOnlyList<Kiosco> ListarTodos();
}