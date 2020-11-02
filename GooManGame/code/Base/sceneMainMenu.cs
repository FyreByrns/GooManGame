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

			public void Update(float elapsed) {
				Game.Instance.DrawSprite(position, sprite);
			}

			public Title(Vector2<float> topLeft) {
				position = topLeft;
				sprite = AssetManager.GetSprite("mainmenu_title");
			}
		}
	}
}
