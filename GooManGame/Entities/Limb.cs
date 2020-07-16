using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static GooManGame.Utilities;

namespace GooManGame {
    public class Limb : Entity {
        public List<Segment> segments;

        public float EndX => segments.Last().endX;
        public float EndY => segments.Last().endY;

        public float MaxReach {
            get {
                float running = 0;
                foreach (Segment s in segments) running += s.length;
                return running;
            }
        }

        public float CurrentReach
            => dist(x, y, EndX, EndY);

        public Limb(float x, float y) {
            this.x = x; this.y = y;
            segments = new List<Segment>();
        }

        public Limb(float x, float y, float length, int num) : this(x, y) {
            for (int i = 0; i < num; i++)
                segments.Add(new Segment(0, 0, 0, length));
        }

        public void Follow(float targetX, float targetY) {
            Segment end = segments.Last();
            end.Follow(targetX, targetY);

            for (int i = segments.Count - 2; i >= 0; i--) {
                segments[i].Follow(segments[i + 1].baseX, segments[i + 1].baseY);
            }

            segments[0].baseX = x;
            segments[0].baseY = y;

            for (int i = 1; i < segments.Count; i++) {
                segments[i].baseX = segments[i - 1].endX;
                segments[i].baseY = segments[i - 1].endY;
            }
        }

        public override void Update() {
            base.Update();

            foreach (Segment s in segments) s.Update();
        }

        public override void Draw() {
            base.Draw();
            foreach (Segment s in segments) s.Draw();
        }
    }
}
