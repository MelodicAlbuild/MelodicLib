using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using MelodicLib.lib.scripts;
using MelodicLib.lib.storage;
using static MelodicLib.lib.data.ExportHandler;
using MelodicLib.lib.data;
using System.Collections.Generic;

namespace MelodicLib.lib
{
    class MelodicItems
    {
        public void InitItems()
        {
            foreach (KeyValuePair<Item, GUID> dict in melodicItems)
            {
                MelodicLog.Log($"[{MelodicLib.modName} | Items]: " + dict.Key);
                CreateItem(dict.Key.item_name, dict.Key.stack_size, dict.Key.name, dict.Key.description, dict.Key.guid, dict.Key.base_item, MelodicScripts.Sprite2(dict.Key.icon_path));
            }

            MelodicLog.Log($"[{MelodicLib.modName} | Items]: Items Loaded...");
        }

        private void CreateItem(string codename, int maxstack, LocalizedString name, LocalizedString desc, string guidstring, string recipecategoryname, Sprite icon)
        {
            var itemPassthrough = GUID.Parse(recipecategoryname);
            var recipecategory = GameResources.Instance.Items.FirstOrDefault(s => s.AssetId == itemPassthrough);

            var item = ScriptableObject.CreateInstance<ItemDefinition>();
            item.name = codename;
            item.Category = recipecategory.Category;
            item.MaxStack = maxstack;
            item.Icon = icon;
            LocalizedString nameStr = name;
            LocalizedString descStr = desc;

            typeof(ItemDefinition).GetField("m_name", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, nameStr);
            typeof(ItemDefinition).GetField("m_description", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, descStr);

            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = item, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets);

            MelodicDict.melodicRegistry[codename] = guid;
            MelodicLog.Log($"[{MelodicLib.modName} | Items]: Item " + codename + " has been loaded");
        }
    }
}
