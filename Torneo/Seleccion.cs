using System.Collections.Generic;
using Newtonsoft.Json;
using linq.Observer;

namespace linq.Torneo
{
    public class Seleccion : IObserver
    {
        #region Properties  
        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("puntos")]
        public int PuntosTotales { get; set; }

        [JsonProperty("goles")]
        public int GolesTotales { get; set; }

        [JsonProperty("asistencias")]
        public int AsistenciasTotales { get; set; }

        [JsonProperty("jugadores")]
        public List<Jugador> Jugadores { get; set; }

        #endregion Properties

        public void update(int p, int g, int a)
        {
            PuntosTotales += p;
            GolesTotales += g;
            AsistenciasTotales += a;
        }
    }
}