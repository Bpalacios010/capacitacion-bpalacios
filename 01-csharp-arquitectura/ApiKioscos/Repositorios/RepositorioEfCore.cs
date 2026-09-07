using Microsoft.EntityFrameworkCore;

namespace DominioKioscos;

public class RepositorioEfCore : IRepositorioKioscos
{
    private readonly KioscosDbContext _contexto;

    public RepositorioEfCore(KioscosDbContext contexto) => _contexto = contexto;

    public void Guardar(Kiosco kiosco)
    {
        if (_contexto.Entry(kiosco).State == EntityState.Detached)
            _contexto.Kioscos.Add(kiosco);

        _contexto.SaveChanges();
    }

    public Kiosco? BuscarPorCodigo(string codigo)
    {
        var normalizado = codigo.Trim().ToUpperInvariant();
        return _contexto.Kioscos.FirstOrDefault(k => k.Codigo == normalizado);
    }

    public IReadOnlyList<Kiosco> ListarTodos() => _contexto.Kioscos.ToList();
}
