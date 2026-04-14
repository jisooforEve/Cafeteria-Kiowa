using System.Diagnostics;
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using ProyectoDSI_Avance.Data;
using ProyectoDSI_Avance.Models;

namespace ProyectoDSI_Avance.Controllers
{
    public class HomeController : Controller
    {
        private readonly KCafeteriaContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(KCafeteriaContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ComidaSeccion()
        {
            return View();
        }

      

        public IActionResult ComplementosEnchiladas(int idProducto, int cantidad)
        {
            ViewBag.IdProducto = idProducto;
            ViewBag.Cantidad = cantidad;

            return View();
        }

        public IActionResult ComplementosPalomitasPollo(int idProducto, int cantidad)
        {
            ViewBag.IdProducto = idProducto;
            ViewBag.Cantidad = cantidad;

            return View();
        }

        public IActionResult ComplementosHotDog(int idProducto, int cantidad)
        {
            ViewBag.IdProducto = idProducto;
            ViewBag.Cantidad = cantidad;

            return View();
        }

        public IActionResult ComplementosTengers(int idProducto, int cantidad)
        {
            ViewBag.IdProducto = idProducto;
            ViewBag.Cantidad = cantidad;

            return View();
        }

        public IActionResult ComplementoPirata(int idProducto, int cantidad)
        {
            ViewBag.IdProducto = idProducto;
            ViewBag.Cantidad = cantidad;

            return View();
        }

        public IActionResult ComplementoGringa(int idProducto, int cantidad)
        {
            ViewBag.IdProducto = idProducto;
            ViewBag.Cantidad = cantidad;

            return View();
        }

        public IActionResult PostresSeccion()
        {
            return View();
        }

        public IActionResult BebidasSeccion()
        {
            return View();
        }

        public IActionResult ComplementosLicuado(int idProducto, int cantidad)
        {
            ViewBag.IdProducto = idProducto;
            ViewBag.Cantidad = cantidad;

            return View();
        }

        public IActionResult CuentaPago()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] //para q no guarde cache
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Carrito()
        {
            var carrito = HttpContext.Session.Get<List<ItemCarrito>>("carrito") ?? new List<ItemCarrito>();
            return View(carrito);
        }
        public IActionResult Agregar(int idProducto, int cantidad, string personalizacion = "")
        {
            var carrito = HttpContext.Session.Get<List<ItemCarrito>>("carrito") ?? new List<ItemCarrito>();

            var item = carrito.FirstOrDefault(x => x.IdProducto == idProducto && x.Personalizacion == personalizacion);

            if (item != null)
            {
                item.Cantidad += cantidad;
            }
            else
            {var producto = _context.Productos.Find(idProducto);

                carrito.Add(new ItemCarrito
                {
                    IdProducto = producto.id_producto,
                    Nombre = producto.nombre,
                    Precio = producto.precio,
                    Cantidad = cantidad,
                    Personalizacion = personalizacion
                });
            }

            HttpContext.Session.Set("carrito", carrito);

            return Ok();
        }
        public IActionResult Eliminar(int idProducto)
        {
            var carrito = HttpContext.Session.Get<List<ItemCarrito>>("carrito");

            carrito.RemoveAll(x => x.IdProducto == idProducto);

            HttpContext.Session.Set("carrito", carrito);

            return RedirectToAction("Carrito");
        }

        [HttpPost]
        public IActionResult Pagar(string numero, string nip)
        {
            var tarjeta = _context.Tarjetas
                .FirstOrDefault(t => t.numero == numero && t.nip == nip && t.activa);

            if (tarjeta == null)
            {
                ViewBag.Error = "Tarjeta inválida";
                
                return View("CuentaPago");
            }

            var carrito = HttpContext.Session.Get<List<ItemCarrito>>("carrito");

            if (carrito == null || carrito.Count == 0)
            {
                ViewBag.Error = "Carrito vacío";
                return View("CuentaPago");
            }

            decimal total = carrito.Sum(x => x.Cantidad * x.Precio);

            if (tarjeta.saldo < total)
            {
                ViewBag.Error = "Saldo insuficiente";
                HttpContext.Session.Remove("carrito");
                return View("CuentaPago");
            }

            // quitar dinero
            tarjeta.saldo -= total;

            // pedido
            var pedido = new Pedido
            {
                folio = new Random().Next(10000, 99999).ToString(),
                total = total,
                fecha_hora = DateTime.Now
            };

            _context.Pedidos.Add(pedido);
            _context.SaveChanges();

            //detalle
            foreach (var item in carrito)
            {
                _context.DetallePedido.Add(new DetallePedido
                {
                    id_pedido = pedido.id_pedido,
                    id_producto = item.IdProducto,
                    cantidad = item.Cantidad,
                    precio_unitario = item.Precio,
                    personalizacion = item.Personalizacion
                });
            }

            _context.SaveChanges();

            //  limpiar carrito
            HttpContext.Session.Remove("carrito");

            //ticket
            return RedirectToAction("Ticket", new { id = pedido.id_pedido });
        }
        public IActionResult Ticket(int id)
        {
            var pedido = _context.Pedidos
                .Where(p => p.id_pedido == id)
                .Select(p => new
                {
                    p.folio,
                    p.fecha_hora,
                    p.total,
                    detalles = _context.DetallePedido
                        .Where(d => d.id_pedido == p.id_pedido)
                        .Select(d => new
                        {
                            producto = _context.Productos
                                .Where(pr => pr.id_producto == d.id_producto)
                                .Select(pr => pr.nombre)
                                .FirstOrDefault(),
                            d.cantidad,
                            d.precio_unitario
                        }).ToList()
                })
                .FirstOrDefault();

            return View(pedido);
        }

        [HttpPost]
        public IActionResult AgregarEspecial(int idProducto, int cantidad = 1)
        {
            var form = Request.Form;

            string personalizacion = "";

            // licuado
            if (form.ContainsKey("fruta") && form.ContainsKey("leche"))
            {
                string fruta = form["fruta"];
                string leche = form["leche"];

                if (string.IsNullOrEmpty(fruta) || string.IsNullOrEmpty(leche))
                {
                    return Content("Selecciona fruta y leche");
                }

                personalizacion = $"Fruta: {fruta}, Leche: {leche}";
            }
            else
            {
                // todos los item chb
                var ingredientes = form.Keys
                    .Where(k => form[k] == "on")
                    .ToList();

                personalizacion = string.Join(", ", ingredientes);
            }

            var carrito = HttpContext.Session.Get<List<ItemCarrito>>("carrito") ?? new List<ItemCarrito>();

            var item = carrito.FirstOrDefault(x =>
                x.IdProducto == idProducto &&
                x.Personalizacion == personalizacion);

            if (item != null)
            {
                item.Cantidad += cantidad;
            }
            else
            {
                var producto = _context.Productos.Find(idProducto);

                if (producto == null)
                    return Content("Producto no encontrado");

                carrito.Add(new ItemCarrito
                {
                    IdProducto = producto.id_producto,
                    Nombre = producto.nombre,
                    Precio = producto.precio,
                    Cantidad = cantidad,
                    Personalizacion = personalizacion
                });
            }

            HttpContext.Session.Set("carrito", carrito);

            return RedirectToAction("Carrito");
        }
        public IActionResult ComplementosChilaquiles(int idProducto, int cantidad)
        {
            ViewBag.IdProducto = idProducto;
            ViewBag.Cantidad = cantidad;

            return View();
        }

    }
}
