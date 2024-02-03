using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Website.Traditional.Animation;
using Website.Traditional.Pages;

namespace Website.Traditional
{
    /// <summary>
    ///    Handles all user interactions with buttons and 3D spaces
    /// </summary>
    public class HomepageHandler : MonoBehaviour
    {
        /// <summary> All text objects that are to be set to the same fontsize </summary>
        [SerializeField] private TMP_Text[] _textContentObjects;
        /// <summary> All interactable objects within the 3D scene that are used to change pages </summary>
        [SerializeField] private InteractableObject[] _interactableObjects;

        /// <summary> The current page the website is on </summary>
        private PageState _currentPage;
        /// <summary> The ID of the object currently being hit with raycast, -1 if no collision </summary>
        private int _raycastCollisionID;
        /// <summary> A dictionary consisting of the InteractableObjects and GameObject ID as the key </summary>
        private Dictionary<int, InteractableObject> _transitionsDict;
        /// <summary> Whether or not a transition in currently happening </summary>
        private bool _transitionStateActive;

        // Start is called on activation
        void Start() {
            // Check if textobjects are properly assigned
            if (_textContentObjects == null || _textContentObjects.Length <= 0) {
                Debug.Log("There are no text objects to be sized");
            } else {
                SyncText();
            }
            // Check if interactable objects are assigned
            if (_interactableObjects == null || _interactableObjects.Length <= 0) {
                Debug.Log("There are no page turners to navigate pages");
            } else {
                // Find the AnimationController component for each object
                foreach (InteractableObject turner in _interactableObjects) {
                    turner.FindAnimationController();
                }
            }
            // Assign initial state
            _currentPage = PageState.HOME;
            _raycastCollisionID = -1;
            _transitionStateActive = false;
            // Add each InteractableObject assigned in the inspector to a dictionary 
            // using the GameObject's ID as the key
            foreach (InteractableObject io in _interactableObjects) {
                // Try adding to the transitionDict, send error message if it fails
                if (!_transitionsDict.TryAdd(io.Obj.GetInstanceID(), io)) {
                    Debug.Log($"The same object was added twice to transition dictionary. Object: {io.Obj.name}");
                }
            }
        }

        // Update is called once per frame
        void Update() {
            // If raycastCollisionID is not -1, it is colliding with an interactable object
            if (_raycastCollisionID != -1 && Input.GetMouseButtonDown(0) && !_transitionStateActive) {
                // Activate transition state and prevent the transition being activated twice
                _transitionStateActive = true;
                TransitionState(_transitionsDict[_raycastCollisionID].PageState);
            }
        }

        // FixedUpdate is called at a specified fixed rate
        void FixedUpdate() {
            // Generate raycast to detect anything the cursor interacts with
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f) && hit.transform.CompareTag("RayInteractable")) {
                _raycastCollisionID = hit.transform.GetInstanceID();
            } else {
                _raycastCollisionID = -1;
            }
        }

        private void TransitionState(PageState stateToSwap) {
            Debug.Log("Not yet implemented");
        }

        /// <summary>
        ///    Syncs all text content within the body of a page to the same fontsize by temporarily 
        ///    enabling autosizing on the lead text object and applying it to all TMP text. Does not
        ///    include headers.
        /// </summary>
        private void SyncText() {
            // Enable autosizing to determine the fontsize to apply to all text, then disable
            _textContentObjects[0].enableAutoSizing = true;
            float fontSize = _textContentObjects[0].fontSize;
            _textContentObjects[0].enableAutoSizing = false;
            // Apply new fontsize to all applicable text
            foreach (TMP_Text text in _textContentObjects) {
                text.fontSize = fontSize;
            }
        }

        /// <summary>
        ///    An object that is to be interacted with in order to change the PageState of the website,
        ///    transitioning to the respective page.
        /// </summary>
        [Serializable]
        private class InteractableObject
        {
            /// <summary> The state that the website will change to when interacted with </summary>
            private PageState _pageState;
            /// <summary> The GameObject that is clicked to change the page </summary>
            private GameObject _obj;

            /// <summary> The state that the website will change to when interacted with </summary>
            public PageState PageState {
                get { return _pageState; }
            }
            /// <summary> The GameObject that is clicked to change the page </summary>
            public GameObject Obj {
                get { return _obj; }
            }
            /// <summary> The AnimationController component of the object used to manipulate the animations </summary>
            private AnimationController _animationController;

            /// <summary>
            ///    Get the AnimationController component of the object
            /// </summary>
            public void FindAnimationController() {
                _animationController = Obj.GetComponent<AnimationController>();
            }
        }
    }
}
