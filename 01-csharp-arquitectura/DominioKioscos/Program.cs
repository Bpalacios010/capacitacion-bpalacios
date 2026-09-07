using DominioKioscos;
using Microsoft.EntityFrameworkCore;

using var db = new KioscosDbContext();

// GUARDAR - solo si no existe, porque el indice unico no deja duplicados
if (!db.Kioscos.Any(k => k.Codigo == "KSK-003"))
{
    var nuevo = new Kiosco("ksk-003");
    db.Kioscos.Add(nuevo);
    db.SaveChanges();
    Console.WriteLine("\n>>> Kiosco guardado en la base");
}

// LEER
var todos = db.Kioscos.ToList();

Console.WriteLine($"\n>>> Kioscos en la base: {todos.Count}");
foreach (var k in todos) Console.WriteLine(k);
