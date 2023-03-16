using BingoWebApp.Models;
using BingoWebApp.Reglas;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.Intrinsics.X86;

namespace BingoWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static readonly ReglasBingo Reglas = new ReglasBingo();
        private static readonly Bolillero Bolillero = new Bolillero();
        private static List<cartonBingo> Cartones = new();


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Bolillero.Sorteos = 0;
            Bolillero.Bolillas = Reglas.LlenarBolillero(); // Creo una lista con 90 bolillas desordenadas
            for (int i = 0; i < 4; i++)
            {
                var carton = new cartonBingo();
                carton.BolillasAModelo = new List<int>();
                carton.CartonNumeros = Reglas.GenerarCarton(); // Creo 4 cartones y los lleno según requerimientos del juego
                Cartones.Add(carton);
                for (int j =  0; j < Cartones.Count; j++)
                {
                    carton.NumeroDeCarton = i + 1; // Asigno numero a los cartones a partir del 1
                }
            }
            return View(Cartones);
        }


        public IActionResult Sortear()
        {
            Bolillero.Bolilla = Reglas.SortearBolilla(Bolillero.Bolillas); // A la bolilla de la clase Bolillero la uso para mostrar por ViewData
            foreach (var carton in Cartones)
            {               
                carton.BolillasAModelo.Add(Bolillero.Bolilla); // Decidi llevarme las mismas bolillas en otra lista, pero aqui propiedad del modelo

                for (int f = 0; f < 3; f++) // Recorro filas de cada carton
                {
                    for (int c = 0; c < 9; c++) // Recorro columnas de cada carton
                    {
                        if (carton.NumeroDeCarton == 1) // Voy preguntando por nro de carton
                        {
                            if (Bolillero.Bolilla == carton.CartonNumeros[f, c] && carton.CartonNumeros[f, c] != 0) // Si la bolilla sacada coincide con el número en la posición actual
                            {
                                carton.Coincide1++; // Si coincide sumo un acierto al carton
                                if (carton.Coincide1 == 15) // Pregunto en cada carton, si llego a 15 aciertos, es el ganador
                                {
                                    ViewBag.CartonGanador = carton.NumeroDeCarton;
                                }
                            }
                        }
                        else if (carton.NumeroDeCarton == 2)
                        {
                            if (Bolillero.Bolilla == carton.CartonNumeros[f, c] && carton.CartonNumeros[f, c] != 0)
                            {
                                carton.Coincide2++;
                                if (carton.Coincide2 == 15)
                                {
                                    ViewBag.CartonGanador = carton.NumeroDeCarton;
                                }
                            }
                        }
                        else if (carton.NumeroDeCarton == 3)
                        {
                            if (Bolillero.Bolilla == carton.CartonNumeros[f, c] && carton.CartonNumeros[f, c] != 0)
                            {
                                carton.Coincide3++;
                                if (carton.Coincide3 == 15)
                                {
                                    ViewBag.CartonGanador = carton.NumeroDeCarton;
                                }
                            }
                        }
                        else if (carton.NumeroDeCarton == 4)
                        {
                            if (Bolillero.Bolilla == carton.CartonNumeros[f, c] && carton.CartonNumeros[f, c] != 0)
                            {
                                carton.Coincide4++;
                                if (carton.Coincide4 == 15)
                                {
                                    ViewBag.CartonGanador = carton.NumeroDeCarton;
                                }
                            }
                        }                      
                    }
                }
                
            }

            ViewData["Bolilla"] = Bolillero.Bolilla; // Lo llevo en el ViewData para mostrar en la vista

            Bolillero.Sorteos++;
            ViewBag.Sorteos = Bolillero.Sorteos; // Esta variable la use para llevar la cantidad de sorteos y mostrar un mensaje al pasar los 90 nros del bolillero

            ViewData["Mensaje"] = "JUEGO TERMINADO";
            
            return View("Index", Cartones);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}