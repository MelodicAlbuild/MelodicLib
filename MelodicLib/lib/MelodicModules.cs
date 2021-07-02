using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MelodicLib.lib.scripts;
using UnityEngine;

namespace MelodicLib.lib {
    public class MelodicModules : IHasData {
        private readonly IMelodicLib view;

        public MelodicModules(IMelodicLib view) {
            this.view = view;
        }

        public void Init() {
            foreach (var dict in view.exportHandler.melodicModules) {
                var finalInput = new RecipeCategory[dict.Key.categories.Length];
                var i          = 0;
                foreach (var category in dict.Key.categories) {
                    finalInput[i] = Findcategories(category);
                    i++;
                }
                CreateProductionModule(dict.Key.module_name, dict.Key.variant, dict.Key.stack_size, dict.Key.base_item, dict.Key.name, dict.Key.description, dict.Key.guid, dict.Key.category_name, dict.Key.factory_type, MelodicScripts.Sprite2(view, dict.Key.icon_path), finalInput, dict.Key.first);
            }
            view.melodicLog.LogToMod($"[{view.modName} | Modules]: Modules Loaded...");
        }

        private RecipeCategory tempcategory;

        public RecipeCategory Findcategories(string categoryname) {
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

        public void CreateProductionModule(string codename, string variantname, int maxstack, string baseitem, LocalizedString name, LocalizedString desc, string guidstring, string categoryname, string factorytypename, Sprite icon, RecipeCategory[] categories) {
            var category = GameResources.Instance.Items.FirstOrDefault(s => s.name == categoryname).Category;
            var item     = ScriptableObject.CreateInstance<ItemDefinition>();
            item.name     = codename;
            item.Category = category;
            item.MaxStack = maxstack;
            item.Icon     = icon;

            var prefabParent = new GameObject();
            var olditem      = GameResources.Instance.Items.FirstOrDefault(s => s.name == baseitem);
            var factorytype  = GameResources.Instance.FactoryTypes.FirstOrDefault(s => s.name == factorytypename);
            prefabParent.SetActive(false);
            var newmodule  = Object.Instantiate(olditem.Prefabs[0], prefabParent.transform);
            var module     = newmodule.GetComponentInChildren<ProductionModule>();
            var gridmodule = newmodule.GetComponent<GridModule>();
            gridmodule.VariantName = variantname;
            gridmodule.Item        = item;
            newmodule.name         = codename;
            item.Prefabs           = new GameObject[] {newmodule};
            var modulecategory = RuntimeAssetCacheLookup.Get<ModuleCategory>().First(s => s.name == factorytypename);
            modulecategory.Modules = modulecategory.Modules.Concat(new ItemDefinition[] {item}).ToArray();

            var productionGroup = MelodicReferences.GetOrCreateTyping(factorytype);

            var nameStr = name;
            var descStr = desc;

            typeof(ProductionModule).GetField("m_factoryType", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(module, factorytype);
            typeof(ProductionModule).GetField("m_module", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(module, gridmodule);
            typeof(ProductionModule).GetField("m_categories", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(module, categories);
            typeof(ProductionModule).GetField("m_productionGroup", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(module, productionGroup);
            typeof(ItemDefinition).GetField("m_name", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, nameStr);
            typeof(ItemDefinition).GetField("m_description", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, descStr);

            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, guid);

            var assets = new AssetReference[] {new AssetReference() {Object = item, Guid = guid, Labels = new string[0]}};
            RuntimeAssetStorage.Add(assets);

            view.melodicDict.melodicRegistry[codename] = guid;
            view.melodicLog.LogToMod($"[{view.modName} | Modules]: Module " + codename + " has been loaded");
        }

        public void CreateProductionModule(string codename, string variantname, int maxstack, string basename, LocalizedString name, LocalizedString desc, string guidstring, string categoryname, string factorytypename, Sprite icon, RecipeCategory[] categories, bool looping) {
            var category = GameResources.Instance.Items.FirstOrDefault(s => s.name == categoryname).Category;
            var item     = ScriptableObject.CreateInstance<ItemDefinition>();
            item.name     = codename;
            item.Category = category;
            item.MaxStack = maxstack;
            item.Icon     = icon;

            var prefabParent = new GameObject();
            var olditem      = GameResources.Instance.Items.FirstOrDefault(s => s.name == basename);
            var factorytype  = GameResources.Instance.FactoryTypes.FirstOrDefault(s => s.name == factorytypename);
            prefabParent.SetActive(false);
            var newmodule  = Object.Instantiate(olditem.Prefabs[0], prefabParent.transform);
            var module     = newmodule.GetComponentInChildren<ProductionModule>();
            var gridmodule = newmodule.GetComponent<GridModule>();
            gridmodule.VariantName = variantname;
            gridmodule.Item        = item;
            item.Prefabs           = new GameObject[] {newmodule};
            var modulecategory = RuntimeAssetCacheLookup.Get<ModuleCategory>().First(s => s.name == factorytypename);
            var concatinated   = new ItemDefinition[] {item};
            modulecategory.Modules = concatinated.ToArray();

            var nameStr = name;
            var descStr = desc;

            typeof(ProductionModule).GetField("m_factoryType", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(module, factorytype);
            typeof(ProductionModule).GetField("m_module", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(module, gridmodule);
            typeof(ProductionModule).GetField("m_categories", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(module, categories);
            typeof(ItemDefinition).GetField("m_name", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, nameStr);
            typeof(ItemDefinition).GetField("m_description", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, descStr);

            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, guid);

            var assets = new AssetReference[] {new AssetReference() {Object = item, Guid = guid, Labels = new string[0]}};
            RuntimeAssetStorage.Add(assets);

            view.melodicDict.melodicRegistry[codename] = guid;
            view.melodicLog.LogToMod($"[{view.modName} | Modules]: Module " + codename + " has been loaded");
        }
    }
}