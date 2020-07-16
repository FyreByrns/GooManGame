using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PixelEngine;

using GooManGame.States;
using static GooManGame.Utilities;

namespace GooManGame {
    public class Camera : Entity {
        public Pixel backgroundColour = colour(0, 0, 0);

        public override void Draw() {
            Game.Instance.Clear(backgroundColour);

            DrawBackground();
            DrawMidground();
            DrawForeground();
        }

        void DrawBackground() { }

        void DrawMidground() {
            Owner.entities.Draw();
        }

        void DrawForeground() { }
    }
}
