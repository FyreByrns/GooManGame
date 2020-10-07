namespace GooManGame {
    public static class Utilities {
        /// <summary>
        /// Map a value in one range to the equivalent value in another. For example: 20 in range 1 to 100 would map to 1 in range 1 to 5.
        /// </summary>
        /// <param name="value">value to map</param>
        /// <param name="origFrom">start of original range</param>
        /// <param name="origTo">end of original range</param>
        /// <param name="targetFrom">start of new range</param>
        /// <param name="targetTo">end of new range</param>
        public static double map(double value, double origFrom, double origTo, double targetFrom, double targetTo)
            => (value - origFrom) / (origTo - origFrom) * (targetTo - targetFrom) + targetFrom;
        /// <inheritdoc cref="map(double, double, double, double, double)"/>
        public static float map(float value, float origFrom, float origTo, float targetFrom, float targetTo)
            => (float)map((double)value, origFrom, origTo, targetFrom, targetTo);
        /// <inheritdoc cref="map(double, double, double, double, double)"/>
        public static int map(int value, int origFrom, int origTo, int targetFrom, int targetTo)
            => (int)map((double)value, origFrom, origTo, targetFrom, targetTo);
        /// <inheritdoc cref="map(double, double, double, double, double)"/>
        public static byte map(byte value, byte origFrom, byte origTo, byte targetFrom, byte targetTo)
            => (byte)map((double)value, origFrom, origTo, targetFrom, targetTo);

        /// <summary>
        /// Constrain a value within a range. For example: 40 constrained between 3 and 10 would be 10.
        /// </summary>
        /// <param name="value">value to constrain</param>
        /// <param name="min">minimum value</param>
        /// <param name="max">maximum value</param>
        public static double constrain(double value, double min, double max)
            => value > max ? max : value < min ? min : value;
        /// <inheritdoc cref="constrain(double, double, double)"/>
        public static float constrain(float value, float min, float max)
            => (float)constrain((double)value, min, max);
        /// <inheritdoc cref="constrain(double, double, double)"/>
        public static int constrain(int value, int min, int max)
            => (int)constrain((double)value, min, max);

        /// <summary>
        /// Get a value between two numbers. For example: between 0 and 2 would return 1
        /// </summary>
        public static double between(double a, double b)
            => (a + b) / 2.0;
        /// <inheritdoc cref="between(double, double)"/>
        public static float between(float a, float b)
            => (a + b) / 2f;
        /// <inheritdoc cref="between(double, double)"/>
        public static int between(int a, int b)
            => (a + b) / 2;
        /// <inheritdoc cref="between(double, double)"/>
        public static byte between(byte a, byte b)
            => (byte)between((int)a, b);

        /// <summary>
        /// Whether a value is near another number.
        /// </summary>
        /// <param name="a">value to check</param>
        /// <param name="b">value to check against</param>
        /// <param name="threshold">how near a needs to be to b</param>
        /// <returns></returns>
        public static bool verycloseto(double a, double b, double threshold = 0.005)
            => abs(a - b) < threshold;

        /// <summary>
        /// Absolute value of a value.
        /// </summary>
        public static double abs(double n)
            => n < 0 ? n * -1 : n;

        /// <summary>
        /// Linearly interpolate between two values by an amount.
        /// </summary>
        /// <param name="from">starting value</param>
        /// <param name="to">ending value</param>
        /// <param name="amount">percentage of progress between the two</param>
        public static double lerp(double from, double to, double amount)
            => (from * (1.0 - amount)) + (to * amount);
        /// <inheritdoc cref="lerp(double, double, double)"/>
        public static float lerp(float from, float to, float amount)
            => (float)lerp((double)from, to, amount);

        /// <summary>
        /// find distance between two points
        /// </summary>
        public static float dist(float x1, float y1, float x2, float y2)
            => (float)System.Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));

        /// <summary>
        /// Convert radians to degrees.
        /// </summary>
        public static float rad2deg(float rad)
            => (float)(rad * 180.0 / System.Math.PI);
        /// <summary>
        /// Convert degrees to radians.
        /// </summary>
        public static float deg2rad(float deg)
            => (float)(deg * System.Math.PI / 180.0);

        /// <summary>
        /// Length of hypotenuse from given base and height.
        /// </summary>
        public static double pythagorean(double x, double y)
            => System.Math.Sqrt((x * x) + (y * y));

        /// <summary>
        /// Rotate a point around another point by an angle.
        /// </summary>
        /// <param name="aroundX">origin X</param>
        /// <param name="aroundY">origin Y</param>
        /// <param name="originalX">point X</param>
        /// <param name="original">point Y</param>
        /// <returns></returns>
        public static Vector2 rotate(float aroundX, float aroundY, float angle, float originalX, float original) {
            Vector2 result = (originalX, original);

            float s = sin(angle);
            float c = cos(angle);

            result.x -= aroundX;
            result.y -= aroundY;

            float nx = result.x * c - result.y * s;
            float ny = result.x * s + result.y * c;

            result.x = nx;
            result.y = ny;

            result.x += aroundX;
            result.y += aroundY;

            return result;
        }
        /// <inheritdoc cref="rotate(float, float, float, float, float)"/>
        public static Vector2 rotate(Vector2 origin, float angle, Vector2 point) =>
            rotate(origin.x, origin.y, angle, point.x, point.y);

        public static float sin(float n)
            => (float)System.Math.Sin(n);
        public static float cos(float n)
            => (float)System.Math.Cos(n);
        public static float atan2(float x, float y)
            => (float)System.Math.Atan2(x, y);

        public static PixelEngine.Point point(int x, int y)
            => new PixelEngine.Point(x, y);
        public static PixelEngine.Point point(float x, float y)
            => point((int)x, (int)y);

        #region colours
        public static PixelEngine.Pixel colour(byte r, byte g, byte b, byte a = 255)
            => new PixelEngine.Pixel(r, g, b, a);

        public static PixelEngine.Pixel mix(PixelEngine.Pixel a, PixelEngine.Pixel b, float amount = 0.5f)
            => colour(
                (byte)lerp(a.R, b.R, amount),
                (byte)lerp(a.G, b.G, amount),
                (byte)lerp(a.B, b.B, amount),
                (byte)lerp(a.A, b.A, amount)
                );

        public static PixelEngine.Pixel white = colour(255, 255, 255, 255);
        public static PixelEngine.Pixel black = colour(0, 0, 0, 255);
        public static PixelEngine.Pixel empty = colour(0, 0, 0, 0);
        public static PixelEngine.Pixel strawberry = colour(200, 100, 130);

        public static PixelEngine.Pixel halfwhite = mix(white, empty);
        public static PixelEngine.Pixel halfblack = mix(black, empty);
        #endregion colours;

        #region collision
        /// <summary>
        /// Detect collision between a point and a circle.
        /// </summary>
        public static bool c_point_circle(float px, float py, float cx, float cy, float cr)
            => dist(px, py, cx, cy) < cr;
        /// <inheritdoc cref="c_point_circle(float, float, float, float, float)"/>
        public static bool c_circle_point(float cx, float cy, float cr, float px, float py)
            => c_point_circle(px, py, cx, cy, cr);

        /// <summary>
        /// Detect collision between a point and a rectangle.
        /// </summary>
        public static bool c_point_rect(float px, float py, float rx, float ry, float rw, float rh)
            => px > rx && py > ry && px < rx + rw && py < ry + rh;
        /// <inheritdoc cref="c_point_rect(float, float, float, float, float, float)"/>
        public static bool c_rect_point(float rx, float ry, float rw, float rh, float px, float py)
            => c_point_rect(px, py, rx, ry, rw, rh);

        /// <summary>
        /// Detect collision between two rectangles.
        /// </summary>
        public static bool c_rect_rect(float r1x, float r1y, float r1w, float r1h, float r2x, float r2y, float r2w, float r2h)
            => r1x + r1w > r2x && r1y + r1h > r2y && r1x < r2x + r2w && r1y < r2y + r2h;
        #endregion collision
    }
}