using UnityEngine;
using MelodicLib.lib.scripts;
using MelodicLib.lib.storage;
using System.Collections.Generic;
using static MelodicLib.lib.data.ExportHandler;
using MelodicLib.lib.data;

namespace MelodicLib.lib
{
    class MelodicCategories
    {
        public void InitCategories()
        {
            foreach (KeyValuePair<Category, GUID> dict in melodicCategories)
            {
                if (dict.Key.category_type == "recipe")
                {
                    CreateRecipeCategory(dict.Key.name, dict.Key.guid);
                }
                else if (dict.Key.category_type == "factory")
                {
                    CreateFactoryCategory(dict.Key.name, dict.Key.guid);
                }
                else if (dict.Key.category_type == "module")
                {
                    CreateModuleCategory(dict.Key.name, dict.Key.guid);
                }
                else if (dict.Key.category_type == "item")
                {
                    CreateItemCategory(dict.Key.name, dict.Key.guid);
                }
                else
                {
                    return;
                }
            }

            MelodicLog.Log($"[{MelodicLib.modName} | Categories]: Categories Loaded...");
        }

        private void CreateFactoryCategory(string name, string categoryId)
        {
            var ok = ScriptableObject.CreateInstance<FactoryType>();
            ok.name = name;
            var guid = GUID.Parse(categoryId);
            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = ok, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets);

            MelodicDict.melodicRegistry[name] = guid;
            MelodicLog.Log($"[{MelodicLib.modName} | Categories]: Factory Category with name " + name + " has been loaded");
        }

        private void CreateModuleCategory(string name, string categoryId)
        {
            var ok = ScriptableObject.CreateInstance<ModuleCategory>();
            ok.name = name;
            var guid = GUID.Parse(categoryId);
            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = ok, Guid = guid, Labels = new string[4] } };
            RuntimeAssetStorage.Add(assets);

            MelodicDict.melodicRegistry[name] = guid;
            MelodicLog.Log($"[{MelodicLib.modName} | Categories]: Module Category with name " + name + " has been loaded");
        }

        private void CreateRecipeCategory(string name, string categoryId)
        {
            var Forge = ScriptableObject.CreateInstance<RecipeCategory>();
            Forge.name = name;
            var guid = GUID.Parse(categoryId);
            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = Forge, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets);

            MelodicDict.melodicRegistry[name] = guid;
            MelodicLog.Log($"[{MelodicLib.modName} | Categories]: Recipe Category with name " + name + " has been loaded");
        }

        private void CreateItemCategory(string name, string categoryId)
        {
            var Forge = ScriptableObject.CreateInstance<ItemCategory>();
            Forge.name = name;
            var guid = GUID.Parse(categoryId);
            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = Forge, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets);

            MelodicDict.melodicRegistry[name] = guid;
            MelodicLog.Log($"[{MelodicLib.modName} | Categories]: Recipe Category with name " + name + " has been loaded");
        }
    }
}
