using MelodicLib.lib.manager.scripts;
using MelodicLib.lib.scripts;
using System.Collections.Generic;

namespace MelodicLib.lib.manager
{
    public class GlobalStorage
    {
        public  readonly Dictionary<Manager, string> globalStorage = new Dictionary<Manager, string>();
        public readonly        ManagerController[]         managerData   = new ManagerController[11];
        private readonly       View                        view;
        public GlobalStorage(View view) {
            this.view = view;
        }
        public void Load()
        {
            Populate();
            MelodicLog.Log("[Global Storage]: Population Complete");
            ManagerLog.Log("[Global Storage]: Population Complete");
            LinkManagers();
            MelodicLog.Log("[Global Storage]: Managers Linked");
            ManagerLog.Log("[Global Storage]: Managers Linked");
            //OutputDictData();
            //ManagerLog.Log("");
            //OutputArrayData();

        }

        private void OutputDictData()
        {
            foreach (KeyValuePair<Manager, string> obj in globalStorage)
            {
                ManagerLog.Log("[Global Manager]: " + obj.Key);
            }
        }

        private void OutputArrayData()
        {
            foreach (ManagerController controller in managerData)
            {
                ManagerLog.Log("[Global Manager]: " + controller);
            }
        }

        private void Populate()
        {
            foreach (Manager manager in view.globalManager.manager)
            {
                globalStorage[manager] = manager.identifier;
            }
        }

        private void LinkManagers()
        {
            foreach (KeyValuePair<Manager, string> obj in globalStorage)
            {
                managerData[obj.Key.id] = new ManagerController() { id = obj.Key.id, identifier = obj.Value, enabled = obj.Key.enabled };
            }
        }
        // Totally done by Greg, Idfk how interfaces work.
        public interface View {
            GlobalManager globalManager { get; }
        }
    }

    public class ManagerController
    {
        public string identifier { get; set; }
        public int id { get; set; }
        public bool enabled { get; set; }

        public override string ToString()
        {
            return "ID: " + id + " | Identifier: " + identifier + " | Enabled: " + enabled;
        }
    }
}
