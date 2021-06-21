using MelodicLib.lib.scripts;
using System.Collections.Generic;
using MelodicLib.lib.storage.scripts;

namespace MelodicLib.lib.storage
{
    class MelodicDict
    {
        public static readonly Dictionary<string, GUID> melodicRegistry = new Dictionary<string, GUID>();
        public MelodicDict()
        {
            MelodicLog.Log("[Dictionary Manager]: Dictionary Verified and Running, Continuing Init");
        }

        public static void ReturnAllData()
        {
            foreach (KeyValuePair<string, GUID> dict in melodicRegistry)
            {
                DictLog.Log("[Dictionary Manager]: Name: " + dict.Key + " | GUID: " + dict.Value + " has been Registered");
            }
        }
    }
}
