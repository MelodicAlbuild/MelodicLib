using System.IO;
using Newtonsoft.Json;

namespace MelodicLib.lib.data {
    public class ImportHandler {
        public ImportHandler(IMelodicLib view) {
            path = view.PersistentDataPath + "/data/data.json";
            view.melodicLog.LogToMod("[Import Handler]: Import Started");
            view.melodicLog.LogToMod("[Import Handler]: Import Complete");
        }

        public           Rootobject imports;
        private readonly string     path;

        public void Import() {
            var root = JsonConvert.DeserializeObject<Rootobject>(File.ReadAllText(path));
            imports = root;
        }
    }


    public class Rootobject {
        public Item[]      items      { get; set; }
        public Recipe[]    recipes    { get; set; }
        public Module[]    modules    { get; set; }
        public Station[]   stations   { get; set; }
        public Deposit[]   deposits   { get; set; }
        public Category[]  categories { get; set; }
        public Modifier[]  modifiers  { get; set; }
        public Schematic[] schematics { get; set; }

        public override string ToString() {
            return "Items: " + items + " | Recipies: " + recipes + " | Modules: " + modules + " | Stations: " + stations + " | Deposits: " + deposits + " | Categories: " + categories + " | Modifiers: " + modifiers + " | Schematics: " + schematics;
        }
    }

    public class Item {
        public string item_name   { get; set; }
        public int    stack_size  { get; set; }
        public string name        { get; set; }
        public string description { get; set; }
        public string guid        { get; set; }
        public string base_item   { get; set; }
        public string icon_path   { get; set; }

        public override string ToString() {
            return "Item Name: " + item_name + " | Stack Size: " + stack_size + " | Name: " + name + " | Description: " + description + " | GUID: " + guid + " | Base Item: " + base_item + " | Icon Path: " + icon_path;
        }
    }

    public class Recipe {
        public string   recipe_name     { get; set; }
        public int      input_amount    { get; set; }
        public Input[]  inputs          { get; set; }
        public Output[] output          { get; set; }
        public string   base_recipe     { get; set; }
        public string   itemID          { get; set; }
        public string[] required_items  { get; set; }
        public string   recipe_category { get; set; }

        public override string ToString() {
            return "Recipe Name: " + recipe_name + " | Input Amount: " + input_amount + " | Inputs: " + inputs + " | Outputs: " + output + " | Base Recipe: " + base_recipe + " | GUID: " + itemID + " | Required Items: " + required_items + " | Recipe Category: " + recipe_category;
        }
    }

    public class Input {
        public string input_name   { get; set; }
        public int    input_amount { get; set; }

        public override string ToString() {
            return "Input Name: " + input_name + " | Input Amount: " + input_amount;
        }
    }

    public class Output {
        public string output_name   { get; set; }
        public int    output_amount { get; set; }

        public override string ToString() {
            return "Output Name: " + output_name + " | Output Amount: " + output_name;
        }
    }

    public class Module {
        public string   module_name   { get; set; }
        public string   variant       { get; set; }
        public int      stack_size    { get; set; }
        public string   base_item     { get; set; }
        public string   name          { get; set; }
        public string   description   { get; set; }
        public string   guid          { get; set; }
        public string   category_name { get; set; }
        public string   factory_type  { get; set; }
        public string   icon_path     { get; set; }
        public string[] categories    { get; set; }
        public bool     first         { get; set; }

        public override string ToString() {
            return "Module Name: " + module_name + " | Variant: " + variant + " | Stack Size: " + stack_size + " | Base Item: " + base_item + " | Name: " + name + " | Description: " + description + " | GUID: " + guid + " | Category Name: " + category_name + " | Factory Type: " + factory_type + " | Icon Path: " + icon_path + " | Categories: " + categories + " | First: " + first;
        }
    }

    public class Station {
        public string   factory_type { get; set; }
        public string   station_name { get; set; }
        public int      stack_size   { get; set; }
        public string   name         { get; set; }
        public string   description  { get; set; }
        public string   guid         { get; set; }
        public string   icon_path    { get; set; }
        public string   variant      { get; set; }
        public string[] categories   { get; set; }

        public override string ToString() {
            return "Factory Type: " + factory_type + " | Station Name: " + station_name + " | Stack Size: " + stack_size + " | Name: " + name + " | Description: " + description + " | GUID: " + guid + " | Icon Path: " + icon_path + " | Variant: " + variant + " | Categories: " + categories;
        }
    }

    public class Deposit {
        public bool   underground     { get; set; }
        public int    percent_replace { get; set; }
        public string output_name     { get; set; }
        public int[]  yields          { get; set; }
        public string replaced_item   { get; set; }

        public override string ToString() {
            return "Underground: " + underground + " | Replacement Percent: " + percent_replace + " | Output Name: " + output_name + " | Yields: " + yields + " | Replaced Item: " + replaced_item;
        }
    }

    public class Category {
        public string category_type { get; set; }
        public string name          { get; set; }
        public string guid          { get; set; }

        public override string ToString() {
            return "Category Type: " + category_type + " | Name: " + name + " | GUID: " + guid;
        }
    }

    public class Modifier {
        public string   modification_type { get; set; }
        public string   target            { get; set; }
        public string[] modifiers         { get; set; }

        public override string ToString() {
            return "Modification Type: " + modification_type + " | Target: " + target + " | Modifiers: " + modifiers;
        }
    }

    public class Schematic {
        public int    id           { get; set; }
        public string name         { get; set; }
        public string image        { get; set; }
        public int    tier         { get; set; }
        public string tooltip      { get; set; }
        public int?[] requirements { get; set; }

        public override string ToString() {
            return "ID: " + id + " | Name: " + name + " | Image: " + image + " | Tier: " + tier + " | Tooltip: " + tooltip + " | Requirements: " + requirements;
        }
    }
}