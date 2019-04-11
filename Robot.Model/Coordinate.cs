using System;

namespace Robot.Model
{
    public class Coordinate : Entity
    {
        public int X { get; set; }

        public int Y { get; set; }

        public EDirection F { get; set; }
    }
}
