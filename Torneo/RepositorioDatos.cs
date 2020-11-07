using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace linq.Torneo
{
    public class RepositorioDatos
    {
        #region Properties  
        public List<Seleccion> Selecciones { get; set; }
        
        #endregion Properties

        #region Initialize
        public RepositorioDatos()
        {
            Selecciones = CrearSelecciones();
        }
        #endregion Initialize

        #region Methods
        private List<Seleccion> CrearSelecciones()
        {
            List<Seleccion> selecciones = new List<Seleccion>();
            try
            {
                selecciones = JsonConvert.DeserializeObject<List<Seleccion>>(File.ReadAllText("./sel.json"));
            }
            catch (System.IO.FileNotFoundException)
            {
                File.Create("./sel.json").Close();
                File.WriteAllText("./sel.json", "[]");
            }
            return selecciones;
        }

        #endregion Methods



    }
}