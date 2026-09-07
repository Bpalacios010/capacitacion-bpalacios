namespace DominioKioscos;
public class MonitorDeKioscos
{
    private readonly IRepositorioKioscos _repositorio;
    public MonitorDeKioscos(IRepositorioKioscos repositorio) => _repositorio = repositorio;
    public IReadOnlyList<Kiosco> EnSilencio(DateTime ahora, TimeSpan tolerancia)
      => _repositorio.ListarTodos()
              .Where(k => k.EstaEnSilencio(ahora, tolerancia))
              .ToList();
}