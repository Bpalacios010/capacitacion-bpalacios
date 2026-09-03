namespace DominioKioscos;
public class RepositorioEnMemoria : IRepositorioKioscos
{
    private readonly Dictionary <string, Kiosco>_Kioscos = new ();

   public void Guardar(Kiosco kiosco) => _Kioscos[kiosco.Codigo] = kiosco;
    public Kiosco? BuscarPorCodigo(string codigo)
    => _Kioscos.TryGetValue(codigo.Trim().ToUpperInvariant(), out var k) ? k : null;
     public IReadOnlyList<Kiosco> ListarTodos() => _Kioscos.Values.ToList();
}