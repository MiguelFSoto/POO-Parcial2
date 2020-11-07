using System;
using System.Collections.Generic;

namespace linq.Observer
{
    class Gestor
    {
        #region Properties
        private List<IObserver> suscribers;
        public List<IObserver> Suscribers
        {
            get { return suscribers; }
            set { suscribers = value; }
        }
        #endregion Properties

        #region Initialize
        public Gestor()
        {
            Suscribers = new List<IObserver>();
        }
        #endregion Initialize

        #region Methods
        public void Suscribe(IObserver suscriber)
        {
            Suscribers.Add(suscriber);
        }

        public void Unsuscribe(IObserver suscriber)
        {
            Suscribers.Remove(suscriber);
        }

        public void Notify(IObserver sub, int p, int g, int a)
        {
            sub.update(p, g, a);
        }
        #endregion Methods
    }
}