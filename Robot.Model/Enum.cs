using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Robot.Model
{
    public enum ECommand
    {
        [Description("PLACE")]
        PLACE,

        [Description("MOVE")]
        MOVE,

        [Description("LEFT")]
        LEFT,

        [Description("RIGHT")]
        RIGHT,

        [Description("REPORT")]
        REPORT,

        [Description("EXIT")]
        EXIT
    }

    public enum EDirection
    {
        [Description("NORTH")]
        NORTH,

        [Description("SOUTH")]
        SOUTH,

        [Description("WEST")]
        WEST,

        [Description("EAST")]
        EAST
    }

    public enum ECoordinate
    {
        [Description("X")]
        X,

        [Description("Y")]
        Y
    }
}
