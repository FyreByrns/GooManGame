using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooManGame.States {
    public interface IOwnedByAState {
        GameState Owner { get; set; }
    }

    public static class OwnedByAStateExtensions {
        public static void SetOwner(this IOwnedByAState owned, GameState newOwner)
            => owned.Owner = newOwner;

        public static void MatchOwner(this IOwnedByAState owned, IOwnedByAState other)
            => owned.SetOwner(other.Owner);
    }
}
