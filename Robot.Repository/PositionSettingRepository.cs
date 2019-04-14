using Microsoft.EntityFrameworkCore;
using Robot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Repository
{
    public class PositionSettingRepository : IPositionSettingRepository
    {
        private readonly RobotDbContext context;

        public PositionSettingRepository(RobotDbContext context)
        {
            this.context = context;
        }

        public async Task<PositionSetting> Setting(EDirection facing)
        {
            return await this.context.Directions.Where(d => d.Facing == facing).FirstOrDefaultAsync();
        }
    }
}
