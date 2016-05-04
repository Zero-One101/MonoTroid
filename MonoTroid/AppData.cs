using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTroid
{
    public static class AppData
    {
        // Graphics
        private const string samusDir = @"Samus\";

        public const string SamusRunL = samusDir + "RunL";
        public const string SamusRunR = samusDir + "RunR";
        public const string SamusStandL = samusDir + "StandL";
        public const string SamusStandR = samusDir + "StandR";
        public const string Spawn = samusDir + "Spawn";

        // Music
        public const string SpawnMusic = "Spawning";
    }
}
