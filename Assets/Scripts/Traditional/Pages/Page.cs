using System;
using UnityEngine;

namespace Website.Traditional.Pages {
    /// <summary>
    ///    A page on the website is defined by a given state it is associated with rather than 
    ///    changing scenes.
    /// </summary>
    [Serializable]
    public class Page
    {
        /// <summary> The state that this Page should be active on </summary>
        private PageState _pageState;
        /// <summary> The GameObject UI Element that contains all page contents </summary>
        private GameObject _pageGO;
        /// <summary> The location of the page content on the screen </summary>
        private PageOrientation _pageOrientation;

        /// <summary> The state that this Page should be active on </summary>
        public PageState PageState {
            get { return _pageState; }
        }
        /// <summary> The GameObject UI Element that contains all page contents </summary>
        public GameObject PageGO {
            get { return _pageGO; }
        }
        /// <summary> The location of the page content on the screen </summary>
        public PageOrientation pageOrientation {
            get { return _pageOrientation; }
        }
    }
}