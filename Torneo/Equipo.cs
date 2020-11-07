using System;
using System.Collections.Generic;
using System.Linq;
using Excepciones.CustomExceptions;

namespace linq.Torneo
{
    public class Equipo
    {
        #region Properties  
        public int Goles { get; set; }
        public int Asistencias { get; set; }
        public int Faltas { get; set; }
        public int TarjetasAmarillas { get; set; }
        public int TarjetasRojas { get; set; }
        public Seleccion Seleccion { get; set; }
        public bool EsLocal { get; set; }

        #endregion Properties

        #region Initialize
        public Equipo(Seleccion seleccion)
        {
            this.Seleccion = new Seleccion();
            this.Seleccion.Nombre = seleccion.Nombre;
            this.Seleccion.PuntosTotales = seleccion.PuntosTotales;
            this.Seleccion.GolesTotales = seleccion.GolesTotales;
            this.Seleccion.AsistenciasTotales = seleccion.AsistenciasTotales;
            this.Seleccion.Jugadores = seleccion.Jugadores.ToList();
        }
        #endregion Initialize

        #region Methods
        public void ExpulsarJugador(string name)
        {
            try
            {
                Jugador jugadorExpulsado = Seleccion.Jugadores.First(j => j.Nombre == name);
                TarjetasRojas++;
                if (Seleccion.Jugadores.Count < 8)
                {
                    LoseForWException ex = new LoseForWException(Seleccion.Nombre);
                    ex.NombreEquipo = Seleccion.Nombre;
                    throw ex;
                }
                Seleccion.Jugadores.Remove(jugadorExpulsado);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("No existe ese jugador para expulsarlo del equipo " + Seleccion.Nombre);
            }
            
        }
        #endregion Methods
    }
}