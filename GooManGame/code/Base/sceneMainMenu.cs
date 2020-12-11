using PixelEngine;
using static GooManGame.Debug;

namespace GooManGame.code.Base {
    public class sceneMainMenu : Scene {
        public sceneMainMenu() {
            Raise("Constructing Main Menu.");
            AddUpdateable(new Title(new Vector2<float>(0, 0)));
        }

        public class Title : IUpdateable {
            public Vector2<float> position;
            Sprite sprite;

            float ox, oy;
            float oelapsed;
            Vector2<int> debugTextLocation = new Vector2<int>(10, 10);

            public void Update(float elapsed) {
                Game.Instance.Clear(Pixel.Empty);

                Game.Instance.DrawSprite(position, sprite);

                float tx = Game.Instance.Lerp(ox, Game.Instance.MouseX, 500f * elapsed);
                float ty = Game.Instance.Lerp(oy, Game.Instance.MouseY, 500f * elapsed);

                ox = tx;
                oy = ty;

                Game.Instance.Draw((int)tx, (int)ty, Pixel.Presets.White);
                Game.Instance.Draw(Game.Instance.MouseX, Game.Instance.MouseY, Pixel.Presets.White);

                float telapsed = Game.Instance.Lerp(oelapsed, elapsed, 500f * elapsed);
                oelapsed = telapsed;

                Game.Instance.DrawText(debugTextLocation, $"{telapsed}ms/f", Pixel.Presets.Green);
            }

            public Title(Vector2<float> topLeft) {
                position = topLeft;
                sprite = AssetManager.GetSprite("mainmenu_title");
            }
        }
    }
}
