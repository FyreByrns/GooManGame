namespace GooManGame {
	/// <summary>
	/// Singleton, manages the main loop.
	/// </summary>
	public class Game : PixelEngine.Game {
		/// <summary>
		/// Instance of the game.
		/// </summary>
		public static Game Instance {
			get {
				if (_instance == null) _instance = new Game();
				return _instance;
			}
		}
		static Game _instance;

		/// <summary>
		/// Load configs and assets.
		/// </summary>
		public static void Preload() {
			IO.LoadConfig();
			AssetManager.Setup();
			Instance.Construct(IO.ScreenWidth, IO.ScreenHeight, IO.ScreenScale, IO.ScreenScale, IO.FPSLock);
		}

		/// <summary>
		/// Called every frame.
		/// </summary>
		public override void OnUpdate(float elapsed) {
			base.OnUpdate(elapsed);
		}

		Game() { }
	}
}
