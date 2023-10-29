using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEAIC6_HFT_2023241.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected RoverDbContext ctx;

        public Repository(RoverDbContext ctx)
        {
            this.ctx = ctx;
        }
        public void Create(T element)
        {
            ctx.Set<T>().Add(element);
            ctx.SaveChanges();
        }

        public void Delete(int id)
        {
            ctx.Set<T>().Remove(Read(id));
            ctx.SaveChanges();
        }

        public IQueryable<T> ReadAll()
        {
            return ctx.Set<T>();
        }

        public abstract T Read(int id);
        public abstract void Update(T element);
    }
}
