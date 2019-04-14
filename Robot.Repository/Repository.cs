using Microsoft.EntityFrameworkCore;
using Robot.Model;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task Add(T entity)
        {
            await this.context.AddAsync<T>(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task<T> Last()
        {
            return await this.context.Set<T>().OrderBy(i => i.Id).LastOrDefaultAsync();
        }
    }
}
