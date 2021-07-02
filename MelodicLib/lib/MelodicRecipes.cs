using System.Collections.Generic;
using System.Linq;
using MelodicLib.lib.data;
using UnityEngine;

namespace MelodicLib.lib {
    public class MelodicRecipes : IHasData {
        private readonly IMelodicLib view;

        public MelodicRecipes(IMelodicLib view) {
            this.view = view;
        }

        public void Init() {
            foreach (var dict in view.exportHandler.melodicRecipes) {
                CreateRecipe(dict.Key.recipe_name, dict.Key.inputs, dict.Key.output, dict.Key.base_recipe, dict.Key.itemID, dict.Key.required_items, dict.Key.recipe_category);
            }

            view.melodicLog.LogToMod($"[{view.modName} | Recipes]: Recipes Loaded...");
        }

        private RecipeCategory tempcategory;

        public RecipeCategory FindCategories(string categoryname) {
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

        private void CreateRecipe(string recipeName, data.Input[] inputs, Output[] outputs, string baseRecipe, string itemId, string[] requiredItems, string recipeCategory) {
            var outputItem = GameResources.Instance.Items.FirstOrDefault(s => s.name == outputs[0].output_name);
            var finalInput = new InventoryItem[inputs.Length];
            var i          = 0;
            foreach (var input in inputs) {
                var itemVar = GameResources.Instance.Items.FirstOrDefault(s => s.name == input.input_name);
                finalInput[i] = new InventoryItem {Item = itemVar, Amount = input.input_amount};
                i++;
            }

            var recipe = ScriptableObject.CreateInstance<Recipe>();
            recipe.name   = recipeName;
            recipe.Inputs = finalInput;
            recipe.Output = new InventoryItem {Item = outputItem, Amount = outputs[0].output_amount};
            if (requiredItems[0] != "") {
                var requiredFinal = new ItemDefinition[inputs.Length];
                var iReq          = 0;
                foreach (var item in requiredItems) {
                    var instanceVar = GameResources.Instance.Items.FirstOrDefault(s => s.name == item);
                    requiredFinal[iReq] = instanceVar;
                    iReq++;
                }
                recipe.RequiredUpgrades = requiredFinal;
            } else {
                var baseRecipeGuid = GUID.Parse(baseRecipe);
                var baseRecipeTag  = GameResources.Instance.Recipes.FirstOrDefault(s => s.AssetId == baseRecipeGuid);
                recipe.RequiredUpgrades = baseRecipeTag.RequiredUpgrades;
            }
            if (recipeCategory != "") {
                recipe.Categories = new RecipeCategory[] {FindCategories(recipeCategory)};
            } else {
                var baseRecipeGuid = GUID.Parse(baseRecipe);
                var baseRecipeTag  = GameResources.Instance.Recipes.FirstOrDefault(s => s.AssetId == baseRecipeGuid);
                recipe.Categories = baseRecipeTag.Categories.ToArray();
            }

            var guid = GUID.Parse(itemId);

            var assets = new AssetReference[] {new AssetReference() {Object = recipe, Guid = guid, Labels = new string[0]}};
            RuntimeAssetStorage.Add(assets);

            view.melodicDict.melodicRegistry[recipeName] = guid;
            view.melodicLog.LogToMod($"[{view.modName} | Recipes]: Recipe " + recipeName + " has been Loaded");
        }
    }
}