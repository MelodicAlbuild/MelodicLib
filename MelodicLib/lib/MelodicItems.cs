using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MelodicLib.lib.scripts;
using UnityEngine;

namespace MelodicLib.lib {
    public class MelodicItems : IHasData {
        private readonly IMelodicLib view;

        public MelodicItems(IMelodicLib view) {
            this.view = view;
        }

        public void Init() {
            foreach (var dict in view.exportHandler.melodicItems) {
                view.melodicLog.LogToMod($"[{view.modName} | Items]: " + dict.Key);
                CreateItem(dict.Key.item_name, dict.Key.stack_size, dict.Key.name, dict.Key.description, dict.Key.guid, dict.Key.base_item, MelodicScripts.Sprite2(view, dict.Key.icon_path));
            }

            view.melodicLog.LogToMod($"[{view.modName} | Items]: Items Loaded...");
        }
        public ItemCategory FindICategories(string targetCategory)
        {
            return (from category in GameResources.Instance.ItemCategoryLookup.Keys
                    where targetCategory == category.name
                    select category).FirstOrDefault();
        }

        private void CreateItem(string codename, int maxstack, LocalizedString name, LocalizedString desc, string guidstring, string recipecategoryname, Sprite icon) {
            var itemPassthrough = GUID.Parse(recipecategoryname);

            var item = ScriptableObject.CreateInstance<ItemDefinition>();
            item.name     = codename;
            item.Category = FindICategories(recipecategoryname);
            item.MaxStack = maxstack;
            item.Icon     = icon;
            var nameStr = name;
            var descStr = desc;

            typeof(ItemDefinition).GetField("m_name", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, nameStr);
            typeof(ItemDefinition).GetField("m_description", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, descStr);

            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, guid);

            var assets = new AssetReference[] {new AssetReference() {Object = item, Guid = guid, Labels = new string[0]}};
            RuntimeAssetStorage.Add(assets);

            view.melodicDict.melodicRegistry[codename] = guid;
            view.melodicLog.LogToMod($"[{view.modName} | Items]: Item " + codename + " has been loaded");
        }
    }
}