using System.Collections.Generic;

namespace MelodicLib.lib.data {
    public class ExportHandler {
        public readonly Dictionary<Item, GUID>       melodicItems      = new Dictionary<Item, GUID>();
        public readonly Dictionary<Recipe, GUID>     melodicRecipes    = new Dictionary<Recipe, GUID>();
        public readonly Dictionary<Modifier, string> melodicModifiers  = new Dictionary<Modifier, string>();
        public readonly Dictionary<Category, GUID>   melodicCategories = new Dictionary<Category, GUID>();
        public readonly Dictionary<Deposit, string>  melodicDeposits   = new Dictionary<Deposit, string>();
        public readonly Dictionary<Module, GUID>     melodicModules    = new Dictionary<Module, GUID>();
        public readonly Dictionary<Station, GUID>    melodicStations   = new Dictionary<Station, GUID>();
        public readonly Dictionary<Schematic, int>   melodicSchematics = new Dictionary<Schematic, int>();

        private readonly IMelodicLib view;

        public ExportHandler(IMelodicLib view) {
            this.view = view;
            view.melodicLog.LogToMod("[Export Handler]: Export Complete");
        }

        public void Export() {
            view.melodicLog.LogToMod("[Export Handler]: Itemizing Items...");
            foreach (var item in view.importHandler.imports.items) {
                melodicItems[item] = GUID.Parse(item.guid);
            }

            view.melodicLog.LogToMod("[Export Handler]: Done Itemizing Items...");
            view.melodicLog.LogToMod("[Export Handler]: Itemizing Recipes...");

            foreach (var item in view.importHandler.imports.recipes) {
                melodicRecipes[item] = GUID.Parse(item.itemID);
            }

            view.melodicLog.LogToMod("[Export Handler]: Done Itemizing Recipes...");
            view.melodicLog.LogToMod("[Export Handler]: Itemizing Modifiers...");

            foreach (var item in view.importHandler.imports.modifiers) {
                melodicModifiers[item] = item.modification_type;
            }

            view.melodicLog.LogToMod("[Export Handler]: Done Itemizing Modifiers...");
            view.melodicLog.LogToMod("[Export Handler]: Itemizing Categories...");

            foreach (var item in view.importHandler.imports.categories) {
                melodicCategories[item] = GUID.Parse(item.guid);
            }

            view.melodicLog.LogToMod("[Export Handler]: Done Itemizing Categories...");
            view.melodicLog.LogToMod("[Export Handler]: Itemizing Deposits...");

            foreach (var item in view.importHandler.imports.deposits) {
                melodicDeposits[item] = item.replaced_item;
            }

            view.melodicLog.LogToMod("[Export Handler]: Done Itemizing Deposits...");
            view.melodicLog.LogToMod("[Export Handler]: Itemizing Modules...");

            foreach (var item in view.importHandler.imports.modules) {
                melodicModules[item] = GUID.Parse(item.guid);
            }

            view.melodicLog.LogToMod("[Export Handler]: Done Itemizing Modules...");
            view.melodicLog.LogToMod("[Export Handler]: Itemizing Stations...");

            foreach (var item in view.importHandler.imports.stations) {
                melodicStations[item] = GUID.Parse(item.guid);
            }

            view.melodicLog.LogToMod("[Export Handler]: Done Itemizing Stations...");
            view.melodicLog.LogToMod("[Export Handler]: Itemizing Schematics...");

            foreach (var item in view.importHandler.imports.schematics) {
                melodicSchematics[item] = item.id;
            }

            view.melodicLog.LogToMod("[Export Handler]: Done Itemizing Schematics...");
        }
    }
}