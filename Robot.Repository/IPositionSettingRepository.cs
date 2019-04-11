using Robot.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Robot.Repository
{
    public interface IPositionSettingRepository
    {
        PositionSetting Setting(EDirection facing);
    }
}
