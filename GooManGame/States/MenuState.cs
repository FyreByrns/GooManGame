using System;
using PixelEngine;

namespace GooManGame.States {
    /// <summary>
    /// Main Menu GameState, first state to take input.
    /// </summary>
    public class MenuState : GameState {
        public override string Name => "Main Menu";

        public MenuState() : base(true, false, false, false) {
            NextState = new PlayState();
            ui = new UI();
        }

        public override void OnLoad() {
            base.OnLoad();

            UIElement playButton = new UIElement(10, 10, 40, 10, "button_play");
            UIElement levelEditorButton = new UIElement(0, 0, 40, 10, "button_leveleditor");

            ui.AddElement(playButton);

            UIElement quitButton = new UIElement(40, 10) { text = "Quit" };
            ui.AddBelow(quitButton, 4, playButton);

            UIElement otherButton = new UIElement(40, 10) { text = "Yes" };
            ui.AddBelow(otherButton, 4, quitButton);

            playButton.Activated += () => Game.Instance.GoToNextState();
            quitButton.Activated += () => Game.Instance.Quit();
            otherButton.Activated += () => {
                NextState = new LevelEditorState();
                Game.Instance.GoToNextState();
            };

            for (int i = 0; i < 10; i++) {
                UIElement e = new UIElement(10, 10) { text = i.ToString() };
                e.Activated += () => Debug.Raise(e.text);
                ui.AddRight(e, 2, ui.elements[ui.elements.Count - 1]);
            }

            ui.currentlySelected = ui.elements[0];

            loaded = true;
        }
    }
}
