namespace GooManGame {
    public static class Utilities {
        public static double map(double value, double origFrom, double origTo, double targetFrom, double targetTo)
            => (value - origFrom) / (origTo - origFrom) * (targetTo - targetFrom) + targetFrom;
        public static float map(float value, float origFrom, float origTo, float targetFrom, float targetTo)
            => (float)map((double)value, origFrom, origTo, targetFrom, targetTo);
        public static int map(int value, int origFrom, int origTo, int targetFrom, int targetTo)
            => (int)map((double)value, origFrom, origTo, targetFrom, targetTo);
        public static byte map(byte value, byte origFrom, byte origTo, byte targetFrom, byte targetTo)
            => (byte)map((double)value, origFrom, origTo, targetFrom, targetTo);

        public static double constrain(double value, double min, double max)
            => value > max ? max : value < min ? min : value;
        public static float constrain(float value, float min, float max)
            => (float)constrain((double)value, min, max);
        public static int constrain(int value, int min, int max)
            => (int)constrain((double)value, min, max);

        public static double between(double a, double b)
            => (a + b) / 2.0;
        public static float between(float a, float b)
            => (a + b) / 2f;
        public static int between(int a, int b)
            => (a + b) / 2;
        public static byte between(byte a, byte b)
            => (byte)between((int)a, b);

        public static bool verycloseto(double a, double b, double threshold = 0.005)
            => abs(a - b) < threshold;

        public static double abs(double n)
            => n < 0 ? n * -1 : n;

        public static double lerp(double from, double to, double amount)
            => (from * (1.0 - amount)) + (to * amount);
        public static float lerp(float from, float to, float amount)
            => (float)lerp((double)from, to, amount);

        /// <summary>
        /// find distance between two points
        /// </summary>
        public static float dist(float x1, float y1, float x2, float y2)
            => (float)System.Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));

        public static float rad2deg(float rad)
            => (float)(rad * 180.0 / System.Math.PI);
        public static float deg2rad(float deg)
            => (float)(deg * System.Math.PI / 180.0);

        public static double pythagorean(double x, double y)
            => System.Math.Sqrt((x * x) + (y * y));

        public static (float x, float y) rotate(float aroundX, float aroundY, float angle, float originalX, float original) {
            (float x, float y) result = (originalX, original);

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
        public static bool c_point_circle(float px, float py, float cx, float cy, float cr)
            => dist(px, py, cx, cy) < cr;
        public static bool c_circle_point(float cx, float cy, float cr, float px, float py)
            => c_point_circle(px, py, cx, cy, cr);

        public static bool c_point_rect(float px, float py, float rx, float ry, float rw, float rh)
            => px > rx && py > ry && px < rx + rw && py < ry + rh;
        public static bool c_rect_point(float rx, float ry, float rw, float rh, float px, float py)
            => c_point_rect(px, py, rx, ry, rw, rh);

        public static bool c_rect_rect(float r1x, float r1y, float r1w, float r1h, float r2x, float r2y, float r2w, float r2h)
            => r1x + r1w > r2x && r1y + r1h > r2y && r1x < r2x + r2w && r1y < r2y + r2h;
        #endregion collision
    }
}