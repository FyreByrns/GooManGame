using System.Collections.Generic;

using PixelEngine;
using static GooManGame.Utilities;

namespace GooManGame.States {
    public class PlayState : GameState {
        public override string Name => "Play State";

        public PlayState() : base(true, true, true, true) { }

        public override void OnLoad() {
            base.OnLoad();

            loaded = true;
        }

        public override void Update(float elapsed) {
            base.Update(elapsed);

            ui.Update();

            foreach (Entity e in entities.FindEntities<Entity>()) {
                if (e is Limb limb)
                    limb.Follow(Game.Instance.MouseX, Game.Instance.MouseY);

                e.Update();
            }
        }
    }
}
