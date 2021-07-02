using System;
using System.Collections.Generic;

namespace MelodicLib.lib.manager {
    public class GlobalStorage {
        public readonly  Dictionary<Manager, string>           globalStorage = new Dictionary<Manager, string>();
        public readonly  Dictionary<string, ManagerController> managerData   = new Dictionary<string, ManagerController>();
        private readonly IMelodicLib                           view;

        public GlobalStorage(IMelodicLib view) {
            this.view = view;
        }

        public void Load() {
            Populate();
            view.melodicLog.LogToMod("[Global Storage]: Population Complete");
            view.melodicLog.LogToManager("[Global Storage]: Population Complete");
            LinkManagers();
            view.melodicLog.LogToMod("[Global Storage]: Managers Linked");
            view.melodicLog.LogToManager("[Global Storage]: Managers Linked");
            //OutputDictData();
            //ManagerLog.Log("");
            //OutputArrayData();
        }

        private void OutputDictData() {
            foreach (var obj in globalStorage) {
                view.melodicLog.LogToManager("[Global Manager]: " + obj.Key);
            }
        }

        private void OutputArrayData() {
            foreach (var controller in managerData) {
                view.melodicLog.LogToManager("[Global Manager]: " + controller);
            }
        }

        private void Populate() {
            foreach (var manager in view.globalManager.manager) {
                globalStorage[manager] = manager.identifier;
            }
        }

        private void LinkManagers() {
            foreach (var obj in globalStorage) {
                if (obj.Key.identifier != obj.Value) throw new InvalidOperationException("The key doesn't match the identifier.");
                managerData[obj.Key.identifier] = new ManagerController() {identifier = obj.Key.identifier, settings = obj.Key.settings, enabled = obj.Key.enabled};
            }
        }
    }

    public class ManagerController {
        public string identifier { get; set; }
        public Setting[] settings { get; set; }
        public bool   enabled    { get; set; }

        public override string ToString() {
            return "Identifier: " + identifier + " | Enabled: " + enabled;
        }
    }
}