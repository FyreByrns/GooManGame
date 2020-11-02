using static GooManGame.Debug;

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
				if (_instance == null) {
					Raise("No game instance found, creating a new one.");
					_instance = new Game();
				}
				return _instance;
			}
		}
		static Game _instance;

		/// <summary>
		/// Load configs and assets.
		/// </summary>
		public static void Preload() {
			Raise("Beginning preload.");
			IO.LoadConfig();
			Raise("Loaded config.");
			AssetManager.Setup();
			Raise("Loaded assets.");
			Instance.Construct(IO.ScreenWidth, IO.ScreenHeight, IO.ScreenScale, IO.ScreenScale, IO.FPSLock);
			Raise("Constructed game instance.");
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
