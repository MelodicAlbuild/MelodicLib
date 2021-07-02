using MelodicLib.lib.manager;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace MelodicLib.lib.scripts
{
    class InitialJSON
    {
        private readonly IMelodicLib view;

        public InitialJSON(IMelodicLib view)
        {
            this.view = view;
            CreateData();
        }

        private void CreateData()
        {
            downloadFile("https://raw.githubusercontent.com/MelodicAlbuild/MelodicLib/master/MelodicLib/defaults/manager.json", Path.Combine(view.PersistentDataPath, "data", "manager.json"));
            downloadFile("https://raw.githubusercontent.com/MelodicAlbuild/MelodicLib/master/MelodicLib/defaults/data.json", Path.Combine(view.PersistentDataPath, "data", "data.json"));
        }

        public static void downloadFile(string sourceURL, string destinationPath)
        {
            long fileSize = 0;
            int bufferSize = 1024;
            bufferSize *= 1000;
            long existLen = 0;

            FileStream saveFileStream;
            if (File.Exists(destinationPath))
            {
                FileInfo destinationFileInfo = new FileInfo(destinationPath);
                existLen = destinationFileInfo.Length;
            }

            if (existLen > 0)
                saveFileStream = new FileStream(destinationPath,
                                                          FileMode.Append,
                                                          FileAccess.Write,
                                                          FileShare.ReadWrite);
            else
                saveFileStream = new FileStream(destinationPath,
                                                          FileMode.Create,
                                                          FileAccess.Write,
                                                          FileShare.ReadWrite);

            System.Net.HttpWebRequest httpReq;
            System.Net.HttpWebResponse httpRes;
            httpReq = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(sourceURL);
            httpReq.AddRange((int)existLen);
            Stream resStream;
            httpRes = (System.Net.HttpWebResponse)httpReq.GetResponse();
            resStream = httpRes.GetResponseStream();

            fileSize = httpRes.ContentLength;

            int byteSize;
            byte[] downBuffer = new byte[bufferSize];

            while ((byteSize = resStream.Read(downBuffer, 0, downBuffer.Length)) > 0)
            {
                saveFileStream.Write(downBuffer, 0, byteSize);
            }
            saveFileStream.Close();
        }
    }
}
