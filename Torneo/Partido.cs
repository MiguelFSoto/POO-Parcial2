using System;
using System.Collections.Generic;
using System.Linq;
using Excepciones.CustomExceptions;

namespace linq.Torneo
{
    public class Partido
    {
        #region Properties  
        public Equipo EquipoLocal { get; set; }
        public Equipo EquipoVisitante { get; set; }
        public List<String> tarjetasLocal = new List<String>();
        public List<String> tarjetasVisitante = new List<String>();

        #endregion Properties

        #region Initialize
        public Partido(Seleccion EquipoLocal, Seleccion EquipoVisitante) 
        {
            this.EquipoLocal = new Equipo(EquipoLocal);
            this.EquipoVisitante = new Equipo(EquipoVisitante);
        }
        #endregion Initialize
        #region Methods
        private void CalcularExpulsiones()
        {
            Random random = new Random();
            List<string> jugadoresVacios = Enumerable.Repeat(string.Empty, 11).ToList();
            List<String> JugadoresLocales = EquipoLocal.Seleccion.Jugadores.Select(j => j.Nombre).ToList().Concat(jugadoresVacios).ToList();
            List<String> JugadoresVisitantes = EquipoVisitante.Seleccion.Jugadores.Select(j => j.Nombre).ToList().Concat(jugadoresVacios).ToList();
            int position = random.Next(JugadoresLocales.Count);
            String expulsadoLocal = JugadoresLocales[position];
            position = random.Next(JugadoresVisitantes.Count);
            String expulsadoVisitante = JugadoresVisitantes[position];
            EquipoLocal.ExpulsarJugador(expulsadoLocal);
            EquipoVisitante.ExpulsarJugador(expulsadoVisitante);
        }

        private void CalcularTarjetas()
        {
            Random random = new Random();
            List<string> jugadoresVacios = Enumerable.Repeat(string.Empty, 11).ToList();
            List<String> JugadoresLocales = EquipoLocal.Seleccion.Jugadores.Select(j => j.Nombre).ToList().Concat(jugadoresVacios).ToList();
            List<String> JugadoresVisitantes = EquipoVisitante.Seleccion.Jugadores.Select(j => j.Nombre).ToList().Concat(jugadoresVacios).ToList();
            int position = random.Next(JugadoresLocales.Count);
            String tarjetaLocal = JugadoresLocales[position];
            position = random.Next(JugadoresVisitantes.Count);
            String tarjetaVisitante = JugadoresVisitantes[position];

            if (tarjetasLocal.Any(j => j == tarjetaLocal))
            {
                EquipoLocal.ExpulsarJugador(tarjetaLocal);
            }
            else
            {
                tarjetasLocal.Add(tarjetaLocal);
            }

            if (tarjetasVisitante.Any(j => j == tarjetaVisitante))
            {
                EquipoVisitante.ExpulsarJugador(tarjetaVisitante);
            }
            else
            {
                tarjetasVisitante.Add(tarjetaVisitante);
            }
        }

        private void CalcularResultado()
        {
            Random random = new Random();
            EquipoLocal.Goles = random.Next(0,6);
            EquipoVisitante.Goles = random.Next(0,6);
            EquipoLocal.Asistencias = random.Next(0, EquipoLocal.Goles);
            EquipoVisitante.Asistencias = random.Next(0, EquipoVisitante.Goles);
        }

        public string Resultado()
        {
            string resultado = "0 - 0";
            try
            {
                for (int i = 0; i < 5; i++)
                {
                    CalcularExpulsiones();
                }
                for (int i = 0; i < 8; i++)
                {
                    CalcularTarjetas();
                }
                CalcularResultado();
                resultado = EquipoLocal.Goles.ToString() + " - " + EquipoVisitante.Goles.ToString();
            }
            catch(LoseForWException ex)
            {
                Console.WriteLine(ex.Message);
                EquipoLocal.Goles -= EquipoLocal.Goles;
                EquipoLocal.Goles -= EquipoLocal.Goles;
                if (ex.NombreEquipo == EquipoLocal.Seleccion.Nombre)
                {
                    EquipoVisitante.Goles += 3;
                    resultado = "0 - 3";
                }
                else
                {
                    EquipoLocal.Goles += 3;
                    resultado = "3 - 0";
                }
            }

            return resultado;
        }
        #endregion Methods

    }
}