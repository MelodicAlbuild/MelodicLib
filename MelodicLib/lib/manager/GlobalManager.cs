using Newtonsoft.Json;
using MelodicLib.lib.scripts;
using System;
using System.IO;
using MelodicLib.lib.manager.scripts;

namespace MelodicLib.lib.manager
{
    public class GlobalManager {
        private readonly View view;
        public GlobalManager(View view) {
            this.view = view;
        }

        public void Load()
        {
            MelodicLog.Log("[Global Manager]: Import Started");
            ManagerLog.Log("[Global Manager]: Import Started", true);
            Import();
            MelodicLog.Log("[Global Manager]: Import Complete");
            ManagerLog.Log("[Global Manager]: Import Complete");
        }
        public Rootobject manager;
        private readonly string path = view.PersistentDataPath + "/data/manager.json";
        private void Import()
        {
            var root = JsonConvert.DeserializeObject<Rootobject>(File.ReadAllText(path));
            manager = root;
        }
        // Totally done by Greg, Idfk how interfaces work.
        public interface View {
            string PersistentDataPath { get; }
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
        public string class_name { get; set; }
        public Setting[] settings { get; set; }
        public bool enabled { get; set; }
        public override string ToString()
        {
            return "ID: " + id + " | Identifier: " + identifier + " | Class Name: " + class_name + " | Settings: " + settings[0].limit + " | " + settings[0].variation + " | Enabled: " + enabled;
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
