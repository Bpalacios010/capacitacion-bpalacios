
using DominioKioscos;

IRepositorioKioscos repositorio = new RepositorioEnMemoria();

var a = new Kiosco("ksk-001");
var b = new Kiosco("ksk-002");

repositorio.Guardar(a);
repositorio.Guardar(b);

a.RegistrarReporte(DateTime.Now);

var monitor = new MonitorDeKioscos(repositorio);

Console.WriteLine("--- Todos ---");
foreach (var k in repositorio.ListarTodos()) Console.WriteLine(k);

Console.WriteLine("");
Console.WriteLine("--- En silencio ---");
foreach (var k in monitor.EnSilencio(DateTime.Now, TimeSpan.FromMinutes(5)))
    Console.WriteLine(k);