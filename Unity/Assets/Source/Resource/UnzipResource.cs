using System.IO;
using UnityEngine;
using System.Collections;
using ICSharpCode.SharpZipLib.Zip;

namespace Game
{
    public partial class ResourceManager
    {



        public IEnumerator CheckUnzipData()
        {
            if (Client.UpdateMgr.HasdownloadFile)
                yield break;

            string zip = Util.StreamingPath + "data.zip";
            WWW www = new WWW(zip);
            yield return www;
            if (www.error != null)
            {
                Debug.LogError("解压data.zip失败!");
                yield break;
            }

            MemoryStream stream = new MemoryStream(www.bytes);
            using (ZipFile zfile = new ZipFile(stream))
            {
                long count = zfile.Count;
                int i = 0;

                var iter = zfile.GetEnumerator();
                while (iter.MoveNext())
                {
                    var entry = iter.Current as ZipEntry;
                    string filePath = entry.Name;
                    if (string.IsNullOrEmpty(filePath))
                        continue;

                    filePath = Util.DataPath + filePath;
                    string subDirectoryName = Path.GetDirectoryName(filePath);
                    if (!string.IsNullOrEmpty(subDirectoryName) && !Directory.Exists(subDirectoryName))
                    {
                        Directory.CreateDirectory(subDirectoryName);
                    }

                    string fileName = Path.GetFileName(filePath);
                    if (string.IsNullOrEmpty(fileName))
                    {
                        i++;
                        continue;
                    }

                    using (FileStream streamWriter = File.Create(filePath))
                    {
                        int size = 2048;
                        byte[] data = new byte[size];
                        Stream zipStream = zfile.GetInputStream(entry);
                        while (true)
                        {
                            size = zipStream.Read(data, 0, data.Length);
                            if (size > 0)
                                streamWriter.Write(data, 0, size);
                            else
                                break;
                        }
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                    Launcher.Ins.SetLaunchState(LaunchState.CheckUnzipData, ++i * 1f / count);
                }
                zfile.Close();
            }
        }

        private void ClearDataPath()
        {
            string[] dirs = Directory.GetDirectories(Util.DataPath);
            for (int i = 0; i < dirs.Length; i++)
                Directory.Delete(dirs[i], true);
            string[] fs = Directory.GetFiles(Util.DataPath);
            for (int i = 0; i < fs.Length; i++)
                File.Delete(fs[i]);
        }
    }
}
