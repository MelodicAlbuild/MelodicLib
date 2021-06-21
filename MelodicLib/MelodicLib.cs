using MelodicLib.lib;
using MelodicLib.lib.data;
using MelodicLib.lib.manager;
using MelodicLib.lib.scripts;
using MelodicLib.lib.storage;
using System;
using System.IO;

namespace MelodicLib
{
    public class MelodicLib : GameMod
    {
        public static string modName = "";
        public static string shortName = "";
        public static string PersistentDataPath = Environment.GetEnvironmentVariable("USERPROFILE") + "/appdata/locallow/volcanoid/volcanoids/MelodicMods/" + modName;
        public MelodicLib(string ModName, string ShortName)
        {
            modName = ModName;
            shortName = ShortName;

            if(!Directory.Exists(Environment.GetEnvironmentVariable("USERPROFILE") + "/appdata/locallow/volcanoid/volcanoids/MelodicMods/" + modName))
            {
                // Create Main Project Directory and Log
                Directory.CreateDirectory(Environment.GetEnvironmentVariable("USERPROFILE") + "/appdata/locallow/volcanoid/volcanoids/MelodicMods/" + modName);
                File.Create(Environment.GetEnvironmentVariable("USERPROFILE") + "/appdata/locallow/volcanoid/volcanoids/MelodicMods/" + modName + "/" + shortName + ".log");

                // Create Data Directory
                Directory.CreateDirectory(Environment.GetEnvironmentVariable("USERPROFILE") + "/appdata/locallow/volcanoid/volcanoids/MelodicMods/" + modName + "/data");

                // Create Logs Directory
                Directory.CreateDirectory(Environment.GetEnvironmentVariable("USERPROFILE") + "/appdata/locallow/volcanoid/volcanoids/MelodicMods/" + modName + "/logs");

                // Create Other Log Files
                File.Create(Environment.GetEnvironmentVariable("USERPROFILE") + "/appdata/locallow/volcanoid/volcanoids/MelodicMods/" + modName + "/logs/Dict.log");
                File.Create(Environment.GetEnvironmentVariable("USERPROFILE") + "/appdata/locallow/volcanoid/volcanoids/MelodicMods/" + modName + "/logs/Manager.log");
            }
        }
        public override void Load()
        {
            var lastWrite = File.GetLastWriteTime(typeof(MelodicLib).Assembly.Location);
            MelodicLog.Log($"[{modName} | Main]: {modName} loaded, build time: {lastWrite.ToShortTimeString()}", true);

            new GlobalManager();
            new GlobalStorage();

            MelodicLog.Log($"[{modName} | Menu]: Managers Loaded without Error. Version Tags are now Enabled");
        }
        public override void OnInitData()
        {
            new MelodicDict();
            new ImportHandler();
            new ExportHandler();

            if (GlobalStorage.managerData[0].enabled)
            {
                MelodicLog.Log($"---{shortName} Items Begin---");
                new MelodicItems().InitItems();
                MelodicLog.Log($"[{modName} | Main]: Items Done.");
                MelodicLog.Log($"---{shortName} Items End---");
            }

            MelodicDict.ReturnAllData();
        }

        public void CopyData(string filePath, string destinationPath)
        {
            File.Copy(filePath, destinationPath);
        }
    }
}
