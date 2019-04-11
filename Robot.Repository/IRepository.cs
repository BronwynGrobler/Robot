using Robot.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Robot.Repository
{
    public interface IRepository<T>
        where T : Entity
    {
        void Add(T entity);

        T Last();
    }
}
