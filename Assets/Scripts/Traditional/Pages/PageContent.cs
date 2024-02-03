using System;
using UnityEngine;

namespace Website.Traditional.Pages {
    /// <summary>
    ///    Content boxes that can contain both images and text content at once, or just one of the 
    ///    two. Will be stacked in a scroll view in a vertical layout group.
    /// </summary>
    [Serializable]
    public struct PageContent
    {
        /// <summary> The image included in the content box, defaults to the left </summary>
        [SerializeField] private Sprite _image;
        /// <summary> The text included in the content box, defaults to the right </summary>
        [SerializeField, TextArea(5, 5)] private string _textContent;
        /// <summary> Will reverse the order, putting the image on the right and text on the right </summary>
        [SerializeField] private bool _reversed;

        /// <summary> The image included in the content box, defaults to the left </summary>
        public Sprite Image {
            get {
                return _image;
            }
        }

        /// <summary> The text included in the content box, defaults to the right </summary>
        public string TextContent {
            get {
                return _textContent;
            }
        }

        /// <summary> Will reverse the order, putting the image on the right and text on the right </summary>
        public bool Reversed {
            get {
                return _reversed;
            }
        }
    }
}
