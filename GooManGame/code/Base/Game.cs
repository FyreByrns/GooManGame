namespace GooManGame {
	public class Game : PixelEngine.Game {
		public static Game Instance {
			get {
				if (_instance == null) _instance = new Game();
				return _instance;
			}
		}
		static Game _instance;

		public static void Preload() {
			IO.LoadConfig();
			AssetManager.Setup();
			Instance.Construct(IO.ScreenWidth, IO.ScreenHeight, IO.ScreenScale, IO.ScreenScale, IO.FPSLock);
		}

		public override void OnUpdate(float elapsed) {
			base.OnUpdate(elapsed);
		}

		Game() { }
	}
}
