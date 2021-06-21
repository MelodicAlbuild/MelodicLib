using MelodicLib.lib.scripts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelodicLib.lib.data
{
    class ExportHandler
    {
        public static readonly Dictionary<Item, GUID> melodicItems = new Dictionary<Item, GUID>();
        public static readonly Dictionary<Recipe, GUID> melodicRecipes = new Dictionary<Recipe, GUID>();
        public static readonly Dictionary<Modifier, string> melodicModifiers = new Dictionary<Modifier, string>();
        public static readonly Dictionary<Category, GUID> melodicCategories = new Dictionary<Category, GUID>();
        public static readonly Dictionary<Deposit, string> melodicDeposits = new Dictionary<Deposit, string>();
        public static readonly Dictionary<Module, GUID> melodicModules = new Dictionary<Module, GUID>();
        public static readonly Dictionary<Station, GUID> melodicStations = new Dictionary<Station, GUID>();
        public static readonly Dictionary<Schematic, int> melodicSchematics = new Dictionary<Schematic, int>();
        public ExportHandler()
        {
            Export();
            MelodicLog.Log("[Export Handler]: Export Complete");
        }
        private void Export()
        {
            MelodicLog.Log("[Export Handler]: Itemizing Items...");
            foreach (Item item in ImportHandler.imports.items)
            {
                melodicItems[item] = GUID.Parse(item.guid);
            }

            MelodicLog.Log("[Export Handler]: Done Itemizing Items...");
            MelodicLog.Log("[Export Handler]: Itemizing Recipes...");

            foreach (Recipe item in ImportHandler.imports.recipes)
            {
                melodicRecipes[item] = GUID.Parse(item.itemID);
            }

            MelodicLog.Log("[Export Handler]: Done Itemizing Recipes...");
            MelodicLog.Log("[Export Handler]: Itemizing Modifiers...");

            foreach (Modifier item in ImportHandler.imports.modifiers)
            {
                melodicModifiers[item] = item.modification_type;
            }

            MelodicLog.Log("[Export Handler]: Done Itemizing Modifiers...");
            MelodicLog.Log("[Export Handler]: Itemizing Categories...");

            foreach (Category item in ImportHandler.imports.categories)
            {
                melodicCategories[item] = GUID.Parse(item.guid);
            }

            MelodicLog.Log("[Export Handler]: Done Itemizing Categories...");
            MelodicLog.Log("[Export Handler]: Itemizing Deposits...");

            foreach (Deposit item in ImportHandler.imports.deposits)
            {
                melodicDeposits[item] = item.replaced_item;
            }

            MelodicLog.Log("[Export Handler]: Done Itemizing Deposits...");
            MelodicLog.Log("[Export Handler]: Itemizing Modules...");

            foreach (Module item in ImportHandler.imports.modules)
            {
                melodicModules[item] = GUID.Parse(item.guid);
            }

            MelodicLog.Log("[Export Handler]: Done Itemizing Modules...");
            MelodicLog.Log("[Export Handler]: Itemizing Stations...");

            foreach (Station item in ImportHandler.imports.stations)
            {
                melodicStations[item] = GUID.Parse(item.guid);
            }

            MelodicLog.Log("[Export Handler]: Done Itemizing Stations...");
            MelodicLog.Log("[Export Handler]: Itemizing Schematics...");

            foreach (Schematic item in ImportHandler.imports.schematics)
            {
                melodicSchematics[item] = item.id;
            }

            MelodicLog.Log("[Export Handler]: Done Itemizing Schematics...");
        }
    }
}
