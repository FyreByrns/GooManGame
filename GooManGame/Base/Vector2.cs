using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static GooManGame.Utilities;

namespace GooManGame {
    public class Vector2 : Vector {
        public double X { get => GetValue(0); set => SetValue(0, value); }
        public double Y { get => GetValue(1); set => SetValue(1, value); }

        public double Heading => Math.Acos(X / pythagorean(X, Y));

        public Vector2() : base(2, 0, 0) { }
        public Vector2(double x, double y) : base(2, x, y) { }

        public static Vector2 operator +(Vector2 a, Vector2 b)
            => new Vector2(a.X + b.X, a.Y + b.Y);
        public static Vector2 operator -(Vector2 a, Vector2 b)
            => new Vector2(a.X - b.X, a.Y - b.Y);
    }
}
