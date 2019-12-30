using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Script.Helpers
{
    public class TexturesLoader
    {
        int jobId = 0;

        public List<ImageFileData> GetImagesDataFromPath(string folderPath)
        {
            jobId++;
            var filePaths = Directory.GetFiles(folderPath, "*.png", SearchOption.AllDirectories).ToList();
            var imageFileDatas = new List<ImageFileData>();

            foreach (var path in filePaths)
            {
                var imageData = new ImageFileData
                {
                    timeCreated = File.GetCreationTime(path),
                    fileName = path.Split(Path.DirectorySeparatorChar).Last(),
                    texture = new Texture2D(1, 1)
                };

                imageFileDatas.Add(imageData);
                GetLoadBytesTask(imageData.texture, path, jobId).Start();
            }

            return imageFileDatas;
        }

        Task GetLoadBytesTask(Texture2D texture, string bytesPath, int jobId)
        {
            return new Task(() =>
            {
                var bytes = File.ReadAllBytes(bytesPath);
                Debug.Log("[TexturesLoader] loaded bytes");

                TaskDispatcher.Dispatch(() =>
                {
                    if (this.jobId != jobId)
                        return;

                    texture?.LoadImage(bytes);
                    Debug.Log("[TexturesLoader] loaded texture");
                });
            });
        }
    }

    public class ImageFileData
    {
        public string fileName;
        public DateTime timeCreated;
        public Texture2D texture;
    }
}