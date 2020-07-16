using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooManGame {
    public class Matrix {
        public int Width { get; }
        public int Height { get; }

        double[,] values;

        public double this[int x, int y, bool zeroIndexed = true] {
            get => GetValue(x, y, zeroIndexed);
            set => SetValue(value, x, y, zeroIndexed);
        }
        public double GetValue(int x, int y, bool zeroIndexed = true) {
            if (!zeroIndexed) {
                x -= 1;
                y -= 1;
            }

            if (x < 0 || x > Width || y < 0 || y > Height)
                throw new ArgumentOutOfRangeException();

            return values[x, y];
        }
        public void SetValue(double value, int x, int y, bool zeroIndexed = true) {
            if (!zeroIndexed) {
                x -= 1;
                y -= 1;
            }

            if (x < 0 || x > Width || y < 0 || y > Height)
                throw new ArgumentOutOfRangeException();

            values[x, y] = value;
        }

        Matrix(double[,] values) {
            Width = values.GetLength(0);
            Height = values.GetLength(1);
            this.values = values;
        }
        public Matrix(int width, int height) {
            Width = width;
            Height = height;
            values = new double[width, height];
        }
        public Matrix(int width, int height, params double[] contents) : this(width, height) {
            if (contents.Length != width * height)
                throw new ArgumentException("Initialization values do not fit exactly in matrix.");

            for (int x = 0; x < Width; x++) {
                for (int y = 0; y < Height; y++) {
                    this[x, y] = contents[y * width + x];
                }
            }
        }

        public static bool DimensionsAreEqual(Matrix a, Matrix b) => a.Width == b.Width && a.Height == b.Height;

        public static Matrix Difference(Matrix a, Matrix b) {
            return a + Negate(b);
        }
        public static Matrix EntrywiseSum(Matrix a, Matrix b) {
            if (!DimensionsAreEqual(a, b)) throw new ArgumentException("Matrices must have equal sizes");

            double[,] result = new double[a.Width, a.Height];

            for (int x = 0; x < a.Width; x++) {
                for (int y = 0; y < a.Height; y++) {
                    result[x, y] = a[x, y] + b[x, y];
                }
            }

            return new Matrix(result);
        }
        public static Matrix DirectSum(Matrix a, Matrix b) {
            double[,] result = new double[a.Width + b.Width, a.Height + b.Height];

            for (int x = 0; x < a.Width; x++) {
                for (int y = 0; y < a.Height; y++) {
                    result[x, y] = a[x, y];
                }
            }

            for (int x = 0; x < b.Width; x++) {
                for (int y = 0; y < b.Height; y++) {
                    result[x + a.Width, y + a.Height] = b[x, y];
                }
            }

            return new Matrix(result);
        }
        public static Matrix DotProduct(Matrix a, Matrix b) {
            if (a.Width != b.Height)
                throw new ArgumentException("Width of matrix A must equal height of matrix B.");

            double[,] result = new double[b.Width, a.Height];

            for (int x = 0; x < b.Width; x++) {
                for (int y = 0; y < a.Height; y++) {
                    for (int i = 0; i < a.Width; i++) {
                        result[x, y] += a[i, y] * b[x, i];
                    }
                }
            }

            return new Matrix(result);
        }
        public static Matrix Multiplicar(double scalar, Matrix matrix) {
            double[,] result = new double[matrix.Width, matrix.Height];

            for (int x = 0; x < matrix.Width; x++) {
                for (int y = 0; y < matrix.Height; y++) {
                    result[x, y] = matrix[x, y] * scalar;
                }
            }

            return new Matrix(result);
        }
        public static Matrix Product(double scalar, Matrix matrix) {
            double[,] result = new double[matrix.Width, matrix.Height];

            for (int x = 0; x < matrix.Width; x++) {
                for (int y = 0; y < matrix.Height; y++) {
                    result[x, y] = matrix[x, y] / scalar;
                }
            }

            return new Matrix(result);
        }
        public static Matrix Negate(Matrix matrix) {
            double[,] result = new double[matrix.Width, matrix.Height];

            for (int x = 0; x < matrix.Width; x++) {
                for (int y = 0; y < matrix.Height; y++) {
                    result[x, y] = matrix[x, y] * -1;
                }
            }

            return new Matrix(result);
        }

        public static Matrix operator +(Matrix a, Matrix b) => EntrywiseSum(a, b);
        public static Matrix operator -(Matrix a, Matrix b) => Difference(a, b);
        public static Matrix operator *(Matrix a, Matrix b) => DotProduct(a, b);
        public static Matrix operator *(double scalar, Matrix matrix) => Multiplicar(scalar, matrix);
        public static Matrix operator /(Matrix matrix, double scalar) => Product(scalar, matrix);

        public override string ToString() {
            StringBuilder result = new StringBuilder();

            for (int y = 0; y < Height; y++) {
                result.Append(" [ ");
                for (int x = 0; x < Width; x++) {
                    result.Append($"{this[x, y]} ");
                }
                result.AppendLine("]");
            }

            return result.ToString();
        }
    }
}
