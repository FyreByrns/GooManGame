using System;
using PixelEngine;

namespace GooManGame.States {
    /// <summary>
    /// Main Menu GameState, first state to take input.
    /// </summary>
    public class MenuState : GameState {
        public override string Name => "Main Menu";

        public override string[] OwnedAssets =>
            new[] {
                "button_play",
                "button_leveleditor",
                "button_quit"
            };

        public MenuState() : base(true, false, false, false) {
            NextState = new PlayState();
            ui = new UI();
        }

        public override void OnLoad() {
            base.OnLoad();

            AssetManager.Load("button_play");
            AssetManager.Load("button_leveleditor");
            AssetManager.Load("button_quit");

            UIElement playButton = new UIElement(10, 10, 40, 10, "button_play");
            UIElement levelEditorButton = new UIElement(0, 0, 40, 10, "button_leveleditor");
            UIElement quitButton = new UIElement(0, 0, 40, 10, "button_quit");

            ui.AddElement(playButton);
            ui.AddBelow(levelEditorButton, 4, playButton);
            ui.AddBelow(quitButton, 4, levelEditorButton);

            playButton.Activated += () => Game.Instance.GoToNextState();
            quitButton.Activated += () => Game.Instance.Quit();
            levelEditorButton.Activated += () => {
                NextState = new LevelEditorState();
                Game.Instance.GoToNextState();
            };

            ui.currentlySelected = ui.elements[0];

            loaded = true;
        }

        public override void OnUnload() {
            base.OnUnload();

            AssetManager.Clear();
        }
    }
}
