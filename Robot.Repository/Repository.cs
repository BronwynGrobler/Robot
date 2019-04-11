using Robot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robot.Repository
{
    public class Repository<T> : IRepository<T>
        where T : Entity
    {
        private readonly RobotDbContext context;

        public Repository(RobotDbContext context)
        {
            this.context = context;
        }

        // An upsert could have been used, but in case auditing is needed in the future
        public void Add(T entity)
        {
            this.context.Add<T>(entity);
            this.context.SaveChanges();
        }

        public T Last()
        {
            return this.context.Set<T>().OrderBy(i => i.Id).LastOrDefault();
        }
    }
}
