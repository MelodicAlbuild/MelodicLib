using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MelodicLib.lib.scripts;
using UnityEngine;

namespace MelodicLib.lib {
    public class MelodicStations : IHasData {
        private readonly IMelodicLib view;

        public MelodicStations(IMelodicLib view) {
            this.view = view;
        }

        private static readonly GUID productionStationGUID = GUID.Parse("7c32d187420152f4da3a79d465cbe87a");

        public void Init() {
            foreach (var dict in view.exportHandler.melodicStations) {
                var categories = new RecipeCategory[dict.Key.categories.Length];
                var i          = 0;
                foreach (var category in dict.Key.categories) {
                    categories[i] = FindRecipeCategories(category);
                    i++;
                    view.melodicLog.LogToMod($"[{view.modName} | Stations]: " + category + " has been added to station " + dict.Key.station_name);
                }
                CreateStation(FindFactoryCategories(dict.Key.factory_type), dict.Key.station_name, dict.Key.stack_size, dict.Key.name, dict.Key.description, dict.Key.guid, Sprite2(dict.Key.icon_path), dict.Key.variant, categories);
            }

            view.melodicLog.LogToMod($"[{view.modName} | Stations]: Stations Loaded...");
        }

        public FactoryType FindFactoryCategories(string categoryName) {
            return GameResources.Instance.FactoryTypes.FirstOrDefault(type => type?.name == categoryName);
        }

        private RecipeCategory tempcategory;

        public RecipeCategory FindRecipeCategories(string categoryname) {
            tempcategory = null;
            foreach (var recipe in GameResources.Instance.Recipes) {
                foreach (var category in recipe.Categories) {
                    if (category != null && categoryname != null) {
                        if (category.name == categoryname) {
                            tempcategory = category;
                        }
                    }
                }
            }
            return tempcategory;
        }

        private Sprite Sprite2(string iconpath) {
            var path = System.IO.Path.Combine(view.PersistentDataPath, iconpath);
            if (!File.Exists(path)) {
                view.melodicLog.LogToMod($"ERROR: [{view.modName} | Stations]: Specified Icon path not found: " + path);
                Debug.LogError($"[{view.modName} | Stations]: Specified Icon path not found: " + path);
                return null;
            }
            var bytes = File.ReadAllBytes(path);


            var texture = new Texture2D(512, 512, TextureFormat.ARGB32, true);
            texture.LoadImage(bytes);

            var sprite = Sprite.Create(texture, new Rect(Vector2.zero, Vector2.one * texture.width), new Vector2(0.5f, 0.5f), texture.width, 0, SpriteMeshType.FullRect, Vector4.zero, false);
            return sprite;
        }

        private void CreateStation(FactoryType factoryType, string codename, int maxStack, LocalizedString name, LocalizedString desc, string guidString, Sprite icon, string variantname, RecipeCategory[] categories) {
            var category = GameResources.Instance.Items.FirstOrDefault(s => s.AssetId == productionStationGUID)?.Category;
            var item     = ScriptableObject.CreateInstance<ItemDefinition>();
            if (item == null) {
                Debug.Log("Item is null");
                return;
            }
            if (category == null) {
                Debug.Log("Category is null");
                return;
            }
            item.name     = codename;
            item.Category = category;
            item.MaxStack = maxStack;
            item.Icon     = icon;

            var prefabParent = new GameObject();
            var olditem      = GameResources.Instance.Items.FirstOrDefault(s => s.AssetId == productionStationGUID);
            prefabParent.SetActive(false);
            var newmodule = Object.Instantiate(olditem.Prefabs[0], prefabParent.transform);
            var module    = newmodule.GetComponentInChildren<FactoryStation>();
            var producer  = newmodule.GetComponentInChildren<Producer>();
            newmodule.SetName("AlloyForgeStation");
            var gridmodule = newmodule.GetComponent<GridModule>();
            gridmodule.VariantName = variantname;
            gridmodule.Item        = item;

            var productionGroup = MelodicReferences.GetOrCreateTyping(factoryType);

            var nameStr = name;
            var descStr = desc;

            item.SetPrivateField("m_name", nameStr);
            item.SetPrivateField("m_description", descStr);
            typeof(FactoryStation).GetField("m_factoryType", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(module, factoryType);
            typeof(FactoryStation).GetField("m_productionGroup", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(module, productionGroup);
            typeof(Producer).GetField("m_categories", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(producer, categories);

            var guid = GUID.Parse(guidString);
            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, guid);

            item.Prefabs = new GameObject[] {newmodule};

            var assets = new AssetReference[] {new AssetReference() {Object = item, Guid = guid, Labels = new string[0]}};
            RuntimeAssetStorage.Add(assets);

            view.melodicDict.melodicRegistry[codename] = guid;
        }
    }
}