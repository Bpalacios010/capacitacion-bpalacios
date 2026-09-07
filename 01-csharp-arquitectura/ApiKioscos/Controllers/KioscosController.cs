using ApiKioscos.Dtos;
using DominioKioscos;
using Microsoft.AspNetCore.Mvc;

namespace ApiKioscos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KioscosController : ControllerBase
{
    private readonly IRepositorioKioscos _repositorio;
    private readonly MonitorDeKioscos _monitor;

    public KioscosController(IRepositorioKioscos repositorio, MonitorDeKioscos monitor)
    {
        _repositorio = repositorio;
        _monitor = monitor;
    }

    // GET /api/kioscos
    [HttpGet]
    public ActionResult<IEnumerable<KioscoDto>> ListarTodos()
        => Ok(_repositorio.ListarTodos().Select(ADto).ToList());

    // GET /api/kioscos/en-silencio
    [HttpGet("en-silencio")]
    public ActionResult<IEnumerable<KioscoDto>> EnSilencio([FromQuery] int minutos = 5)
        => Ok(_monitor.EnSilencio(DateTime.Now, TimeSpan.FromMinutes(minutos)).Select(ADto).ToList());

    // GET /api/kioscos/KSK-001
    [HttpGet("{codigo}")]
    public ActionResult<KioscoDto> BuscarPorCodigo(string codigo)
    {
        var kiosco = _repositorio.BuscarPorCodigo(codigo);
        if (kiosco is null)
            return NotFound(new { mensaje = $"No existe el kiosco {codigo}." });

        return Ok(ADto(kiosco));
    }

    // POST /api/kioscos
    [HttpPost]
    public ActionResult<KioscoDto> Crear(CrearKioscoDto datos)
    {
        if (string.IsNullOrWhiteSpace(datos.Codigo))
            return BadRequest(new { mensaje = "El codigo es obligatorio." });

        if (_repositorio.BuscarPorCodigo(datos.Codigo) is not null)
            return Conflict(new { mensaje = $"Ya existe un kiosco con codigo {datos.Codigo}." });

        var kiosco = new Kiosco(datos.Codigo);
        _repositorio.Guardar(kiosco);

        return CreatedAtAction(nameof(BuscarPorCodigo), new { codigo = kiosco.Codigo }, ADto(kiosco));
    }

    // POST /api/kioscos/KSK-001/reportes
    [HttpPost("{codigo}/reportes")]
    public ActionResult<KioscoDto> RegistrarReporte(string codigo)
    {
        var kiosco = _repositorio.BuscarPorCodigo(codigo);
        if (kiosco is null)
            return NotFound(new { mensaje = $"No existe el kiosco {codigo}." });

        try
        {
            kiosco.RegistrarReporte(DateTime.Now);
            _repositorio.Guardar(kiosco);
            return Ok(ADto(kiosco));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    // POST /api/kioscos/KSK-001/baja
    [HttpPost("{codigo}/baja")]
    public ActionResult<KioscoDto> DarDeBaja(string codigo, BajaKioscoDto datos)
    {
        var kiosco = _repositorio.BuscarPorCodigo(codigo);
        if (kiosco is null)
            return NotFound(new { mensaje = $"No existe el kiosco {codigo}." });

        try
        {
            kiosco.MarcarFueraDeServicio(datos.Motivo);
            _repositorio.Guardar(kiosco);
            return Ok(ADto(kiosco));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    private static KioscoDto ADto(Kiosco k) => new(
        k.Id, k.Codigo, k.Estado.ToString(), k.UltimoReporte, k.MotivoFueraDeServicio);
}
