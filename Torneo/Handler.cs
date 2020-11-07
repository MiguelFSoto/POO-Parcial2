using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace linq.Torneo
{
    class Handler
    {
        public static List<Seleccion> Selecciones;
        public static Observer.Gestor gestor;

        public Handler()
        {
            RepositorioDatos Datos = new RepositorioDatos();
            gestor = new Observer.Gestor();
            Selecciones = Datos.Selecciones;
            Selecciones.ForEach(newSel => gestor.Suscribe(newSel));
        }

        public void AñadirSeleccion()
        {
            Seleccion newSel = new Seleccion();
            Console.Write("Nombre: ");
            newSel.Nombre = Console.ReadLine();
            newSel.PuntosTotales = 0;
            newSel.GolesTotales = 0;
            newSel.AsistenciasTotales = 0;
            newSel.Jugadores = new List<Jugador>();

            Console.WriteLine("Jugadores:");
            for (int i = 0; i < 11; i++)
            {
                try
                {
                    Console.Write("Nombre: ");
                    string n = Console.ReadLine();
                    Console.Write("Edad: ");
                    int e = Int32.Parse(Console.ReadLine());
                    Console.Write("Posicion: ");
                    int p = Int32.Parse(Console.ReadLine());
                    Console.Write("Ataque: ");
                    double a = Double.Parse(Console.ReadLine());
                    Console.Write("Defensa: ");
                    double d = Double.Parse(Console.ReadLine());
                    Console.Write("Goles: ");
                    int g = Int32.Parse(Console.ReadLine());
                    Console.Write("Asistencias: ");
                    int s = Int32.Parse(Console.ReadLine());

                    newSel.Jugadores.Add(new Jugador(n, e, p, a, d, g, s));
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Datos no validos");
                    i--;
                }
                Console.WriteLine("===========");
            }
            Selecciones.Add(newSel);
        }

        public void JugarPartido()
        {
            Console.WriteLine("Selecciones: ");
            Selecciones.ForEach(newSel =>
            {
                Console.WriteLine(newSel.Nombre);
            });
            Console.WriteLine();

            Seleccion selLocal, selVisitante;
            while (true)
            {
                Console.WriteLine("Equipo Local: ");
                string nombreLocal = Console.ReadLine();
                selLocal = Selecciones.FirstOrDefault(sel => sel.Nombre == nombreLocal);

                if (selLocal == null)
                {
                    Console.WriteLine("Equipo no encontrado");
                }
                else
                {
                    break;
                }
            }

            while (true)
            {
                Console.WriteLine("Equipo Visitante: ");
                string nombreVisitante = Console.ReadLine();
                selVisitante = Selecciones.FirstOrDefault(sel => sel.Nombre == nombreVisitante);

                if (selVisitante == null)
                {
                    Console.WriteLine("Equipo no encontrado");
                }
                else
                {
                    break;
                }
            }

            Partido newPart = new Partido(selLocal, selVisitante);
            Console.WriteLine(newPart.Resultado());
            int puntosLocal = 0;
            int puntosVisitante = 0;
            if (newPart.EquipoLocal.Goles > newPart.EquipoVisitante.Goles)
            {
                puntosLocal = 3;
            }
            else if (newPart.EquipoLocal.Goles < newPart.EquipoVisitante.Goles)
            {
                puntosVisitante = 3;
            }
            else
            {
                puntosLocal = 1;
                puntosVisitante = 1;
            }

            gestor.Notify(selLocal, puntosLocal, newPart.EquipoLocal.Goles, newPart.EquipoLocal.Asistencias);
            gestor.Notify(selVisitante, puntosVisitante, newPart.EquipoVisitante.Goles, newPart.EquipoVisitante.Asistencias);
            //Console.WriteLine(newPart.Resultado());

        }

        public void MostrarSelecciones()
        {
            Selecciones.ForEach(newSel =>
            {
                Console.WriteLine("================");
                Console.WriteLine("Nombre: " + newSel.Nombre);
                Console.WriteLine("Puntos: " + newSel.PuntosTotales);
                Console.WriteLine("Goles: " + newSel.GolesTotales);
                Console.WriteLine("Asistencias: " + newSel.AsistenciasTotales);
            });
        }

        public void Menu()
        {
            string option = "";
            while (option != "0")
            {
                Console.WriteLine("=================");
                Console.WriteLine("1) Añadir Seleccion");
                Console.WriteLine("2) Simular Partido");
                Console.WriteLine("3) Ver Selecciones");
                Console.WriteLine("0) Salir");
                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        AñadirSeleccion();
                        break;

                    case "2":
                        JugarPartido();
                        break;

                    case "3":
                        MostrarSelecciones();
                        break;

                    case "0":
                        File.WriteAllText("./sel.json", JsonConvert.SerializeObject(Selecciones));
                        return;

                    default:
                        Console.WriteLine("Opcion Invalida");
                        break;
                }
            }
        }
    }
}