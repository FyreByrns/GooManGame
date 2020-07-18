using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooManGame {
    public class Vector : Matrix {
        public double GetValue(int index) => this[0, index];
        public void SetValue(int index, double value) => this[0, index] = value;

        public double Magnitude() {
            double runningTotal = 0;
            for (int i = 0; i < Height; i++) {
                runningTotal += GetValue(0, i) * GetValue(0, i);
            }
            return Math.Sqrt(runningTotal);
        }
        public Vector Normalized => (Vector)(this / Magnitude());

        public Vector(int dimensions, params double[] values) : base(1, dimensions, values) { }
    }
}
