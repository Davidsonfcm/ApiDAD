using DataAccess;
using System;

namespace Services
{
    public abstract class Services : IDisposable
    {
        protected ConnectorContext context;

        public Services() { }

        public Services(ConnectorContext context)
        {
            this.context = context;
        }

        public void Save()
        {
            this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
