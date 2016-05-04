using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTroid.States.Player
{
    class Landing : OnGround
    {
        public override void Begin(Samus context)
        {
            // TODO: Don't immediately redirect to Standing
            context.State = new Standing();
            context.State.Begin(context);
        }
    }
}
