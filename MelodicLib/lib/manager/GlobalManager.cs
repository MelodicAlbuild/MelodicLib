using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace MelodicLib.lib.manager
{
    public class GlobalManager
    {
        private readonly string path;
        public Rootobject manager;
        private readonly IMelodicLib view;

        public GlobalManager(IMelodicLib view)
        {
            this.view = view;
            path = view.PersistentDataPath + "/data/manager.json";
        }

        public void Load()
        {
            view.melodicLog.LogToMod("[Global Manager]: Import Started");
            view.melodicLog.LogToManager("[Global Manager]: Import Started", true);
            Import();
            view.melodicLog.LogToMod("[Global Manager]: Import Complete");
            view.melodicLog.LogToManager("[Global Manager]: Import Complete");
        }

        private void Import()
        {
            manager = JsonConvert.DeserializeObject<Rootobject>(File.ReadAllText(path));
        }
    }


    public class Rootobject
    {
        public Manager[] manager { get; set; }

        public override string ToString()
        {
            return "Manager: " + manager;
        }
    }

    public class Manager
    {
        public int id { get; set; }
        public string identifier { get; set; }
        public Setting[] settings { get; set; }
        public bool enabled { get; set; }

        public override string ToString()
        {
            return "Identifier: " + identifier  + " | Settings: " + settings[0].limit + " | " + settings[0].variation + " | Enabled: " + enabled;
        }
    }

    public class Setting
    {
        public int limit { get; set; }
        public bool variation { get; set; }

        public override string ToString()
        {
            return "Limit: " + limit + " | Variation: " + variation;
        }
    }

}