using PixelEngine;

using static GooManGame.Utilities;

namespace GooManGame {
    public class UIElement {
        public UI owner;
        public UIElement above, below, left, right;

        public delegate void UIElementActivatedEvent();
        public event UIElementActivatedEvent Activated;

        public int x, y, width, height;
        public string assetName;
        public string text;

        public bool active;

        public Pixel outlineColour;
        public Pixel inactiveBackground, selectedBackground, activeBackground;
        public Pixel inactiveText, selectedText, activeText;

        public UIElement() {
            outlineColour = Pixel.Presets.White;

            inactiveBackground = Pixel.Presets.DarkGrey;
            selectedBackground = Pixel.Presets.Grey;
            activeBackground = Pixel.Presets.White;

            inactiveText = Pixel.Presets.Snow;
            selectedText = Pixel.Presets.White;
            activeText = Pixel.Presets.Black;
        }

        public UIElement(int width, int height) : this() {
            this.width = width;
            this.height = height;
        }

        public UIElement(int x, int y, int width, int height, string assetName) : this(width, height) {
            this.x = x;
            this.y = y;
            this.assetName = assetName;
        }

        public bool MouseHovering => Game.Instance.MouseX > x && Game.Instance.MouseY > y &&
                                     Game.Instance.MouseX < x + width && Game.Instance.MouseY < y + height;

        public virtual void OnActivated() {
            Activated?.Invoke();
        }

        void SwitchTo(UIElement element) {
            owner.currentlySelected = element;
            owner.switchedThisFrame = true;
        }

        public virtual void Update() {
            if (MouseHovering) SwitchTo(this);

            if (!owner.switchedThisFrame && owner.currentlySelected == this)
                if (IO.InputInState("up", InputState.JustPressed)) SwitchTo(above ?? this);
                else if (IO.InputInState("down", InputState.JustPressed)) SwitchTo(below ?? this);
                else if (IO.InputInState("left", InputState.JustPressed)) SwitchTo(left ?? this);
                else if (IO.InputInState("right", InputState.JustPressed)) SwitchTo(right ?? this);
        }

        public virtual void Draw() {
            if (active) {
                Game.Instance.FillRect(new Point(x, y), width, height, activeBackground);
                Game.Instance.DrawText(new Point(x, y), text, activeText);
            }
            else if (owner.currentlySelected == this || MouseHovering) {
                Game.Instance.DrawRect(new Point(x - 1, y - 1), width + 1, height + 1, outlineColour);
                Game.Instance.FillRect(new Point(x, y), width, height, selectedBackground);
                Game.Instance.DrawText(new Point(x, y), text, selectedText);
            }
            else {
                Game.Instance.FillRect(new Point(x, y), width, height, inactiveBackground);
                Game.Instance.DrawText(new Point(x, y), text, inactiveText);
            }
            Game.Instance.DrawSprite(point(x, y), AssetManager.GetSprite(assetName));
        }
    }
}
