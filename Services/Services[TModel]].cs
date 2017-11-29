using DataAccess;
using System.Collections.Generic;
using System.Data.Entity;

namespace Services
{
    public abstract class Services<T> : Services where T : class
    {
        public Services(ConnectorContext context)
            : base(context)
        {
        }

        public Services(bool proxyCreationEnabled = true)
        {
            this.context = new ConnectorContext(proxyCreationEnabled);
        }


        /// <summary>
        /// Retorna todos os regristros
        /// </summary>
        /// <returns>Um item da entidade</returns>
        public IEnumerable<T> All()
        {
            return this.context.Set<T>();
        }

        /// <summary>
        /// Salva um item
        /// </summary>
        /// <param name="item">Item da entidade</param>
        protected void Save(T item)
        {
            this.context.Set<T>().Add(item);
            base.Save();
        }

        /// <summary>
        /// Salva vários items
        /// </summary>
        /// <param name="items">Coleção de itens da entidade</param>
        protected void Save(IEnumerable<T> items)
        {
            this.context.Set<T>().AddRange(items);
            base.Save();
        }

        protected void Update(T item)
        {
            this.context.Set<T>().Attach(item);
            this.context.Entry(item).State = EntityState.Modified;
            base.Save();
        }

        /// <summary>
        /// Deleta um item da entidade
        /// </summary>
        /// <param name="item">Item da entidade</param>
        public void Delete(object id)
        {
            var item = this.Get(id);
            this.context.Set<T>().Remove(item);
            base.Save();
        }

        /// <summary>
        /// Retorna um item pela chave
        /// </summary>
        /// <param name="id">Chave</param>
        /// <returns>Item da entidae</returns>
        public T Get(object id)
        {
            return base.context.Set<T>().Find(id);
        }
    }
}
