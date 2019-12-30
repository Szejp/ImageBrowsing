using System;
using System.Collections.Generic;
using System.IO;
using Script.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Script.ImageBrowsing
{
    public class ImageBrowsingComponent : MonoBehaviour
    {
        [SerializeField] string folderPath;
        [SerializeField] ImageDataView imageTemplate;
        [SerializeField] Transform content;
        [SerializeField] Button refreshButton;

        TexturesLoader texturesLoader;
        List<ImageDataView> imageDataViews = new List<ImageDataView>();

        string defaultImagesPath => Application.dataPath + Path.DirectorySeparatorChar + "Images";

        public void SetFolderPath(string folderPath)
        {
            this.folderPath = folderPath;
        }

        public void RefreshImages()
        {
            var textures = texturesLoader.GetImagesDataFromPath(folderPath);

            foreach (var t in imageDataViews)
                t.gameObject.SetActive(false);

            for (int i = 0; i < textures.Count; i++)
            {
                ImageDataView selectedView;
                if (imageDataViews.Count - 1 < i)
                {
                    selectedView = Instantiate(imageTemplate, content).GetComponent<ImageDataView>();
                    imageDataViews.Add(selectedView);
                }
                else
                    selectedView = imageDataViews[i];

                selectedView.SetImageData(textures[i]);
                selectedView.gameObject.SetActive(true);
            }
        }

        void Awake()
        {
            texturesLoader = new TexturesLoader();
            refreshButton.onClick.AddListener(RefreshImages);

            if (String.IsNullOrEmpty(folderPath))
                folderPath = defaultImagesPath;
        }

        void OnDestroy()
        {
            refreshButton.onClick.RemoveListener(RefreshImages);
        }
    }
}