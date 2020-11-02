using static GooManGame.Debug;

namespace GooManGame {
    class Program {
        static void Main(string[] args) {
            Game.Preload();
            Raise("Starting game.");
            Game.Instance.Start();
        }
    }
}
