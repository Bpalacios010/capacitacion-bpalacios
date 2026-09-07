namespace ApiKioscos.Dtos;

// Lo que la API DEVUELVE al cliente
public record KioscoDto(
    Guid Id,
    string Codigo,
    string Estado,
    DateTime? UltimoReporte,
    string? MotivoFueraDeServicio);

// Lo que la API RECIBE para crear
public record CrearKioscoDto(string Codigo);

// Lo que la API RECIBE para dar de baja
public record BajaKioscoDto(string Motivo);
