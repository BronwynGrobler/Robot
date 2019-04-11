using Robot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robot.Repository
{
    public class PositionSettingRepository : IPositionSettingRepository
    {
        private readonly RobotDbContext context;

        public PositionSettingRepository(RobotDbContext context)
        {
            this.context = context;
        }

        public PositionSetting Setting(EDirection facing)
        {
            return this.context.Directions.Where(d => d.Facing == facing).FirstOrDefault();
        }
    }
}
