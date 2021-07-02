using System;
using System.IO;
using MelodicLib.lib;
using MelodicLib.lib.data;
using MelodicLib.lib.manager;
using MelodicLib.lib.scripts;
using MelodicLib.lib.storage;

namespace MelodicLib {
    public abstract class MelodicLib : GameMod, IMelodicLib {
        public abstract string        modName            { get; }
        public abstract string        shortName          { get; }
        public          GlobalManager globalManager      { get; }
        public          GlobalStorage globalStorage      { get; }
        public          MelodicLog    melodicLog         { get; }
        public          MelodicDict   melodicDict        { get; }
        public          ImportHandler importHandler      { get; }
        public          ExportHandler exportHandler      { get; }
        public          string        PersistentDataPath => Environment.GetEnvironmentVariable("USERPROFILE") + "/appdata/locallow/volcanoid/volcanoids/MelodicMods/" + modName;
        public          string        LPSPath            => Environment.GetEnvironmentVariable("USERPROFILE") + "/appdata/locallow/volcanoid/volcanoids/MelodicMods";

        protected MelodicLib() {
            Init();

            globalManager = new GlobalManager(this);
            globalStorage = new GlobalStorage(this);
            melodicLog    = new MelodicLog(this);
            melodicDict   = new MelodicDict(this);
            importHandler = new ImportHandler(this);
            exportHandler = new ExportHandler(this);
        }

        // Avoids virtual methods calls in the constructor.
        private void Init() {
            if(!Directory.Exists(LPSPath))
            {
                Directory.CreateDirectory(LPSPath);
            }

            if (!Directory.Exists(PersistentDataPath))
            {
                // Create Main Project Directory and Log
                Directory.CreateDirectory(PersistentDataPath);
                File.Create(PersistentDataPath + "/" + shortName + ".log");

                // Create Data Directory
                Directory.CreateDirectory(PersistentDataPath + "/data");

                // Create Logs Directory
                Directory.CreateDirectory(PersistentDataPath + "/logs");

                // Create Other Log Files
                File.Create(PersistentDataPath + "/logs/Dict.log");
                File.Create(PersistentDataPath + "/logs/Manager.log");
            }

            if(!File.Exists(Path.Combine(PersistentDataPath, "data", "manager.json")) || !File.Exists(Path.Combine(PersistentDataPath, "data", "data.json")))
            {
                new InitialJSON(this);
            }
        }

        public override void Load() {
            var lastWrite = File.GetLastWriteTime(typeof(MelodicLib).Assembly.Location);
            melodicLog.LogToMod($"[{modName} | Main]: {modName} loaded, build time: {lastWrite.ToShortTimeString()}", true);

            globalManager.Load();
            globalStorage.Load();

            melodicLog.LogToMod($"[{modName} | Menu]: Managers Loaded without Error. Version Tags are now Enabled");
        }

        public override void OnInitData() {
            melodicDict.InitData();
            importHandler.Import();
            exportHandler.Export();

            // All the entries in json will fill out the data here.
            foreach (var kvp in globalStorage.managerData) {
                var managerData = kvp.Value;
                var identifier  = managerData.identifier;
                if (!managerData.enabled) continue; // Ignore if disabled.

                melodicLog.LogToMod($"---{shortName} {identifier} Begin---");

                // This gets the target class for the data. The key is the 'identifier' from the json.
                GetDataClass(identifier).Init();

                melodicLog.LogToMod($"[{modName} | Main]: {identifier} Done.");
                melodicLog.LogToMod($"---{shortName} {identifier} End---");
            }

            melodicDict.ReturnAllData();
        }

        public void CopyData(string filePath, string destinationPath) {
            File.Copy(filePath, destinationPath);
        }

        /**
         * For a given identifier, it returns an instance of the class associated to it.
         * Still Gregs Voodoo Shit, Even when explained.
         */
        public IHasData GetDataClass(string identifier) {
            return identifier switch {
                "categories" => new MelodicCategories(this),
                "deposits" => new MelodicDeposits(this),
                "items" => new MelodicItems(this),
                "modules" => new MelodicModules(this),
                "recipes" => new MelodicRecipes(this),
                "stations" => new MelodicStations(this),
                _ => throw new NotImplementedException($"No class found for {identifier}.")
            };
        }
    }

    // Totally done by Greg, Idfk how interfaces work. IT'S A MASTER INTERFACE AHHHHHHHHHHHHHHHHHHHHHHHHH
    // One interface to rule them all, and in the dark ages of code, bind them.
    public interface IMelodicLib {
        string        modName            { get; }
        string        shortName          { get; }
        GlobalManager globalManager      { get; }
        GlobalStorage globalStorage      { get; }
        MelodicLog    melodicLog         { get; }
        MelodicDict   melodicDict        { get; }
        ImportHandler importHandler      { get; }
        ExportHandler exportHandler      { get; }
        string        PersistentDataPath { get; }
    }
}