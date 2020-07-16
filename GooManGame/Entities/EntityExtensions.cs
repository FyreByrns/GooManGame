using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooManGame.Entities {
    public static class EntityExtensions {
        public static void ReachFor(this Entity me, float x, float y) {
            if (me is Limb limb)
                limb.ReachFor(x, y);
        }
    }
}
