using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GooManGame.States;

namespace GooManGame {
    /// <summary>
    /// An object which belongs to a GameState and has a position etc
    /// </summary>
    public class Entity : IOwnedByAState {
        public GameState Owner { get; set; }
        public float x, y;

        public virtual void Update() { }
        public virtual void Draw() { }
    }
}
