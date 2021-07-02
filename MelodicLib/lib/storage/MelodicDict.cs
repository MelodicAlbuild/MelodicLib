using System.Collections.Generic;

namespace MelodicLib.lib.storage {
    public class MelodicDict {
        public readonly  Dictionary<string, GUID> melodicRegistry = new Dictionary<string, GUID>();
        private readonly IMelodicLib              view;

        public MelodicDict(IMelodicLib view) {
            this.view = view;
        }

        public void InitData() {
            view.melodicLog.LogToMod("[Dictionary Manager]: Dictionary Verified and Running, Continuing Init");
        }

        public void ReturnAllData() {
            foreach (var dict in melodicRegistry) {
                view.melodicLog.LogToMod("[Dictionary Manager]: Name: " + dict.Key + " | GUID: " + dict.Value + " has been Registered");
            }
        }
    }
}