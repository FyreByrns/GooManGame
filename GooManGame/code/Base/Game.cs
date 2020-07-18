using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PixelEngine;

using GooManGame.States;

namespace GooManGame {
    public class Game : PixelEngine.Game{
        public static Game Instance {
            get {
                if (_instance == null) _instance = new Game();
                return _instance;
            }
        }
        static Game _instance;

        public GameState gameState;

        public static void Begin() {
            IO.LoadConfig();
            AssetManager.Setup();
            Instance.Construct(IO.ScreenWidth, IO.ScreenHeight, IO.ScreenScale, IO.ScreenScale, IO.FPSLock);
            Instance.gameState = new MenuState();
            Instance.gameState.OnLoad();
        }

        public override void OnUpdate(float elapsed) {
            base.OnUpdate(elapsed);

            if (IO.InputInState("cancel", InputState.JustPressed)) Quit();

            if (gameState == null) Finish();
            if (gameState.loaded) {
                gameState.Update(elapsed);
                gameState?.Draw();
            }
            if (gameState == null) Finish();

        }

        public void GoToNextState() {
            gameState.OnUnload();
            gameState = gameState.NextState;
            gameState?.OnLoad();
        }

        public void Quit() {
            gameState.OnUnload();
            gameState = null;
        }
    }
}
