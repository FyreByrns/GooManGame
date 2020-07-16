using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static GooManGame.Utilities;

namespace GooManGame {
    public class Segment {
        public float baseX, baseY;
        public float endX, endY;
        public float length, angle;

        public Segment(float x, float y, float angle, float length) {
            baseX = x;
            baseY = y;
            this.angle = angle;
            this.length = length;
        }

        public void LookAt(float targetX, float targetY) {
            float diffX = targetX - baseX;
            float diffY = -(targetY - baseY);
            angle = atan2(diffX, diffY);

            CalculateEnd();
        }

        public void Follow(float targetX, float targetY) {
            LookAt(targetX, targetY);

            baseX = targetX - length * sin(angle);
            baseY = targetY + length * cos(angle);
        }

        public void Forward(float distance) {
            baseX += distance * sin(angle);
            baseY -= distance * cos(angle);
        }

        public void CalculateEnd() {
            float dx = length * cos(angle + deg2rad(270));
            float dy = length * sin(angle + deg2rad(270));
            endX = baseX + dx;
            endY = baseY + dy;
        }

        public void Update() {
            CalculateEnd();
        }

        public void Draw() {
            Game.Instance.DrawLine(new PixelEngine.Point((int)baseX, (int)baseY), new PixelEngine.Point((int)endX, (int)endY), PixelEngine.Pixel.Presets.White);
        }
    }
}
