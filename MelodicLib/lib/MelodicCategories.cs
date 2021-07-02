using UnityEngine;

namespace MelodicLib.lib {
    public class MelodicCategories : IHasData {
        private readonly IMelodicLib view;

        public MelodicCategories(IMelodicLib view) {
            this.view = view;
        }

        public void Init() {
            foreach (var dict in view.exportHandler.melodicCategories) {
                if (dict.Key.category_type == "recipe") {
                    CreateRecipeCategory(dict.Key.name, dict.Key.guid);
                } else if (dict.Key.category_type == "factory") {
                    CreateFactoryCategory(dict.Key.name, dict.Key.guid);
                } else if (dict.Key.category_type == "module") {
                    CreateModuleCategory(dict.Key.name, dict.Key.guid);
                } else if (dict.Key.category_type == "item") {
                    CreateItemCategory(dict.Key.name, dict.Key.guid);
                } else {
                    return;
                }
            }

            view.melodicLog.LogToMod($"[{view.modName} | Categories]: Categories Loaded...");
        }

        public void CreateFactoryCategory(string name, string categoryId) {
            var ok = ScriptableObject.CreateInstance<FactoryType>();
            ok.name = name;
            var guid   = GUID.Parse(categoryId);
            var assets = new AssetReference[] {new AssetReference() {Object = ok, Guid = guid, Labels = new string[0]}};
            RuntimeAssetStorage.Add(assets);

            view.melodicDict.melodicRegistry[name] = guid;
            view.melodicLog.LogToMod($"[{view.modName} | Categories]: Factory Category with name " + name + " has been loaded");
        }

        public void CreateModuleCategory(string name, string categoryId) {
            var ok = ScriptableObject.CreateInstance<ModuleCategory>();
            ok.name = name;
            var guid   = GUID.Parse(categoryId);
            var assets = new AssetReference[] {new AssetReference() {Object = ok, Guid = guid, Labels = new string[4]}};
            RuntimeAssetStorage.Add(assets);

            view.melodicDict.melodicRegistry[name] = guid;
            view.melodicLog.LogToMod($"[{view.modName} | Categories]: Module Category with name " + name + " has been loaded");
        }

        public void CreateRecipeCategory(string name, string categoryId) {
            var Forge = ScriptableObject.CreateInstance<RecipeCategory>();
            Forge.name = name;
            var guid   = GUID.Parse(categoryId);
            var assets = new AssetReference[] {new AssetReference() {Object = Forge, Guid = guid, Labels = new string[0]}};
            RuntimeAssetStorage.Add(assets);

            view.melodicDict.melodicRegistry[name] = guid;
            view.melodicLog.LogToMod($"[{view.modName} | Categories]: Recipe Category with name " + name + " has been loaded");
        }

        public void CreateItemCategory(string name, string categoryId) {
            var Forge = ScriptableObject.CreateInstance<ItemCategory>();
            Forge.name = name;
            var guid   = GUID.Parse(categoryId);
            var assets = new AssetReference[] {new AssetReference() {Object = Forge, Guid = guid, Labels = new string[0]}};
            RuntimeAssetStorage.Add(assets);

            view.melodicDict.melodicRegistry[name] = guid;
            view.melodicLog.LogToMod($"[{view.modName} | Categories]: Recipe Category with name " + name + " has been loaded");
        }
    }
}