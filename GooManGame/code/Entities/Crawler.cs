using System.Collections.Generic;

using static GooManGame.Utilities;

namespace GooManGame {
    public class Crawler : Entity {
        List<DoubleLimb> limbs = new List<DoubleLimb>();

        float motionX = 1;

        public Crawler() {
            DoubleLimb test = new DoubleLimb {
                limb1 = new Limb(0, 0, 2, 15),
                limb2 = new Limb(0, 0, 15, 2)
            };

            limbs.Add(test);
        }

        public override void Update() {
            base.Update();

            motionX = dist(x, 0, Game.Instance.MouseX, 0);
            x = (float)lerp(x, Game.Instance.MouseX, 0.05);

            foreach (DoubleLimb limb in limbs) {
                limb.limb1.x = x;
                limb.limb1.y = y;
                limb.elbowTargetX = x + motionX * (Game.Instance.MouseX > x ? -1 : 1) / 2;
                limb.elbowTargetY = Game.Instance.MouseY - 10;

                limb.handTargetX = Game.Instance.MouseX;
                limb.handTargetY = Game.Instance.MouseY;

                limb.Update();
            }
        }

        public override void Draw() {
            base.Draw();

            foreach (DoubleLimb limb in limbs)
                limb.Draw();
        }

        public class DoubleLimb {
            public float elbowTargetX, elbowTargetY;
            public float handTargetX, handTargetY;

            public Limb limb1, limb2;

            public void Update() {
                limb1.Follow(elbowTargetX, elbowTargetY);
                limb1.Update();

                limb2.x = limb1.EndX;
                limb2.y = limb1.EndY;
                limb2.Follow(handTargetX, handTargetY);
                limb2.Update();
            }
            public void Draw() {
                limb1.Draw();
                limb2.Draw();
            }
        }
    }
}
