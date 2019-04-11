using System;
using System.Collections.Generic;
using System.Text;

namespace Robot.Model
{
    public class PositionSetting : Entity
    {
        public EDirection Facing { get; set; }

        public EDirection Left { get; set; }

        public EDirection Right { get; set; }

        public ECoordinate Coordinate { get; set; }
    }
}
