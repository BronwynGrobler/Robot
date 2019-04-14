using Robot.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Repository
{
    public interface IPositionSettingRepository
    {
        Task<PositionSetting> Setting(EDirection facing);
    }
}
