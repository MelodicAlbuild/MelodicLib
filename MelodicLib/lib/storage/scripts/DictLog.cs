using System.IO;

namespace MelodicLib.lib.storage.scripts
{
    class DictLog
    {
        public static readonly string path = System.Environment.GetEnvironmentVariable("USERPROFILE") + "/appdata/locallow/volcanoid/volcanoids/MelodicMods" + MelodicLib.modName +"/logs/Dict.log";
        private static StreamWriter writer;

        public static void Log(string msg)
        {
            if (writer == null) writer = new StreamWriter(path, true);

            writer.WriteLine(msg);

            // So it's not kept in the buffer and you can see it in an external program.
            writer.Flush();

            // You don't need dispose. In this case we're leaving it open while the game is running.
            // Call `Close()` somewhere on game shutdown.
        }

        public static void Log(string msg, bool overwrite)
        {
            if (writer == null) writer = new StreamWriter(path, !overwrite);

            writer.WriteLine(msg);

            // So it's not kept in the buffer and you can see it in an external program.
            writer.Flush();

            // You don't need dispose. In this case we're leaving it open while the game is running.
            // Call `Close()` somewhere on game shutdown.
        }

        public static void Close()
        {
            writer?.Close();
        }
    }
}
