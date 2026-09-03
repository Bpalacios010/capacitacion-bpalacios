namespace DominioKioscos;

public class Kiosco
{
    public Guid Id { get; }
    public string Codigo { get; }
    public EstadoKiosco Estado { get; private set; }
    public DateTime? UltimoReporte { get; private set; }
    public string? MotivoFueraDeServicio { get; private set; }

    public Kiosco(string codigo)
    {
        if (string.IsNullOrWhiteSpace(codigo))
            throw new ArgumentException("El código es obligatorio.", nameof(codigo));

        Id = Guid.NewGuid();
        Codigo = codigo.Trim().ToUpperInvariant();
        Estado = EstadoKiosco.Desconectado;
    }

    public void RegistrarReporte(DateTime momento)
    {
        if (Estado == EstadoKiosco.FueraDeServicio)
            throw new InvalidOperationException(
                $"El kiosco {Codigo} está fuera de servicio y no debería reportar.");

        UltimoReporte = momento;
        Estado = EstadoKiosco.Activo;
    }

    public void MarcarFueraDeServicio(string motivo)
    {
        if (string.IsNullOrWhiteSpace(motivo))
            throw new ArgumentException("Debe indicar el motivo.", nameof(motivo));

        Estado = EstadoKiosco.FueraDeServicio;
        MotivoFueraDeServicio = motivo;
    }

    public bool EstaEnSilencio(DateTime ahora, TimeSpan tolerancia)
        => UltimoReporte is null || (ahora - UltimoReporte.Value) > tolerancia;

    public override string ToString()
        => $"{Codigo} · {Estado} · último reporte: {UltimoReporte?.ToString("g") ?? "nunca"}";
}
