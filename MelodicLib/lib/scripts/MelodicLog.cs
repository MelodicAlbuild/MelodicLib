using System.IO;

namespace MelodicLib.lib.scripts {
    public class MelodicLog {
        public readonly  string       shortNameLogPath;
        private readonly string       managerLogPath;
        private readonly string       dictLogPath;
        private          StreamWriter modLogWriter;
        private          StreamWriter managerLogWriter;
        private          StreamWriter dictLogWriter;

        public MelodicLog(IMelodicLib view) {
            shortNameLogPath = $"{view.PersistentDataPath}/{view.shortName}.log";
            managerLogPath   = $"{view.PersistentDataPath}/logs/Manager.log";
            dictLogPath      = $"{view.PersistentDataPath}/logs/Dict.log";
        }

        /**
         * Log to all three logs.
         */
        public void Log(string msg, bool overwrite = true) {
            LogToMod(msg, overwrite);
            LogToManager(msg, overwrite);
            LogToDict(msg, overwrite);
        }

        /**
         * Logs only to {view.shortName}.log.
         */
        public void LogToMod(string msg, bool overwrite = true) {
            Log(ref modLogWriter, shortNameLogPath, msg, overwrite);
        }

        /**
         * Logs only to Manager.log.
         */
        public void LogToManager(string msg, bool overwrite = true) {
            Log(ref managerLogWriter, managerLogPath, msg, overwrite);
        }

        /**
         * Logs only to Dict.log.
         */
        public void LogToDict(string msg, bool overwrite = true) {
            Log(ref dictLogWriter, dictLogPath, msg, overwrite);
        }

        private void Log(ref StreamWriter writer, string path, string msg, bool overwrite) {
            if (writer == null) writer = new StreamWriter(path, !overwrite);

            writer.WriteLine(msg);

            // So it's not kept in the buffer and you can see it in an external program.
            writer.Flush();

            // You don't need dispose. In this case we're leaving it open while the game is running.
            // Call `Close()` somewhere on game shutdown.
        }

        public void Close() {
            modLogWriter?.Close();
            managerLogWriter?.Close();
            dictLogWriter?.Close();
        }
    }
}