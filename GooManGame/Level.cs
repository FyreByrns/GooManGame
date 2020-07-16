using GooManGame.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooManGame {
    public class Level : IOwnedByAState {
        public GameState Owner { get; set; }
    }
}
