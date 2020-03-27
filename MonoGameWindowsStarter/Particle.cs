using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter
{
    public struct Particle
    {
        public Vector2 Position,
                       Velocity,
                       Acceleration;

        public float Scale,
                     Life;

        public Color Color;
    }
}
