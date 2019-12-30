using Script.Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.ImageBrowsing
{
    public class ImageDataView : MonoBehaviour
    {
        readonly string createdText = "created {0} hours ago";

        [SerializeField] RawImage image;
        [SerializeField] TextMeshProUGUI filenameText;
        [SerializeField] TextMeshProUGUI timeSinceCreatedText;

        ImageFileData imageData;

        public void SetImageData(ImageFileData imageData)
        {
            this.imageData = imageData;

            image.texture = imageData.texture;
            filenameText.text = imageData.fileName;
            timeSinceCreatedText.text =
                string.Format(createdText, DateTimeHelper.GetHoursSinceCreated(imageData.timeCreated));
        }

        void OnDestroy()
        {
            DestroyImmediate(imageData.texture);
        }
    }
}