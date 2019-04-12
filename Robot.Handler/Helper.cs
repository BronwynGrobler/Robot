using System;
using System.Collections.Generic;
using System.Text;

namespace Robot.Handler
{
    public static class Helper
    {
        public static bool Verify(int X, int Y)
        {
            return (X > 5 || X < 0 || Y > 5 || Y < 0);
        }
    }
}
