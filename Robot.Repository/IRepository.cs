using Robot.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Repository
{
    public interface IRepository<T>
        where T : Entity
    {
        Task Add(T entity);

        Task<T> Last();
    }
}
