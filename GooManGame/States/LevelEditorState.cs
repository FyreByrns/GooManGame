using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooManGame.States {
    class LevelEditorState : GameState {
        public override string Name => "Level Editor";

        public LevelEditorState() : base(true, true, true, true) { }
    }
}
