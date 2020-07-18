using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GooManGame.States;
using PixelEngine;

namespace GooManGame {
    /// <summary>
    /// Holds UI elements and lets them react to mouse and keyboard input
    /// </summary>
    public class UI : IOwnedByAState {
        public List<UIElement> elements = new List<UIElement>();
        public UIElement currentlySelected;
        public bool switchedThisFrame;

        public GameState Owner { get; set; }

        public void AddElement(UIElement element) {
            element.owner = this;
            elements.Add(element);
        }

        public void AddAbove(UIElement newElement, int distance, UIElement nextTo) {
            newElement.below = nextTo;
            nextTo.above = newElement;

            newElement.x = nextTo.x;
            newElement.y = nextTo.y - newElement.height - distance;

            AddElement(newElement);
        }
        public void AddBelow(UIElement newElement, int distance, UIElement nextTo) {
            newElement.above = nextTo;
            nextTo.below = newElement;

            newElement.x = nextTo.x;
            newElement.y = nextTo.y + nextTo.height + distance;

            AddElement(newElement);
        }
        public void AddRight(UIElement newElement, int distance, UIElement nextTo) {
            newElement.left = nextTo;
            nextTo.right = newElement;

            newElement.x = nextTo.x + nextTo.width + distance;
            newElement.y = nextTo.y;

            AddElement(newElement);
        }
        public void AddLeft(UIElement newElement, int distance, UIElement nextTo) {
            newElement.right = nextTo;
            nextTo.left = newElement;

            newElement.x = nextTo.x - newElement.width - distance;
            newElement.y = nextTo.y;

            AddElement(newElement);
        }

        public void Update() {
            switchedThisFrame = false;
            foreach (UIElement element in elements) {
                element.Update();

                if ((element.MouseHovering || element == currentlySelected) &&
                    (Game.Instance.GetMouse(Mouse.Left).Pressed || IO.InputInState("accept", InputState.JustPressed))) {
                    currentlySelected = element;
                    element.active = true;
                    element.OnActivated();
                }
                else element.active = false;
            }

            if (currentlySelected == null && elements.Count > 0) currentlySelected = elements[0];
        }

        public void Draw() {
            foreach (UIElement element in elements)
                element.Draw();
        }
    }
}
