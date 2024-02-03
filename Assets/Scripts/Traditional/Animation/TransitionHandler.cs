using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Website.Traditional.Pages;

namespace Website.Traditional.Animation {
    public class TransitionHandler : MonoBehaviour
    {
        /// <summary> All canvas element pages created within the Unity Editor </summary>
        [SerializeField] private Page[] _pages;
        /// <summary> The content panel that displays all </summary>
        [SerializeField] private RectTransform _contentPanel;
        
        // Start is called before the first frame update
        void Start()
        {
            // Check if pages are assigned
            if (_pages == null || _pages.Length <= 0) {
                Debug.Log("No pages are referenced in HomepageHandler");
            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
