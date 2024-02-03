using UnityEngine;
using Website.Traditional.Pages;

namespace Website.Traditional.Projects
{
    /// <summary>
    ///    The attributes for a project that is to be displayed in the portfolio section
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObjects/Projects", fileName = "NewProject", order = 0)]
    public class Project : ScriptableObject
    {
        /// <summary> The type of project </summary>
        [SerializeField] private ProjectType _projectType;
        /// <summary> The title of the project </summary>
        [SerializeField] private string _title;
        /// <summary> The icon displayed on the project select menu </summary>
        [SerializeField] private Sprite _icon;
        /// <summary> The thumbnail image for the project when it is loaded up </summary>
        [SerializeField] private Sprite _thumbnail;
        /// <summary> A brief description of the project </summary>
        [SerializeField, TextArea(5, 10)] private string _briefDescription;
        /// <summary> More specific details (text and images) of the project </summary>
        [SerializeField] private PageContent[] _details;
        /// <summary> Design details (text and images) about the project </summary>
        [SerializeField] private PageContent[] _design;
        /// <summary> The challenges faced during the project </summary>
        [SerializeField] private PageContent[] _challenges;
        /// <summary> Credits to the inclusion of any other people's assets or help </summary>
        [SerializeField] private string _credits;
        /// <summary> The 3D trophy associated with the project </summary>
        [SerializeField] private GameObject _trophy;

        /// <summary> The type of project </summary>
        public ProjectType ProjectType {
            get {
                return _projectType;
            }
        }
        /// <summary> The title of the project </summary>
        public string Title {
            get {
                return _title;
            }
        }
        /// <summary> The icon displayed on the project select menu </summary>
        public Sprite Icon {
            get {
                return _icon;
            }
        }
        /// <summary> The thumbnail image for the project when it is loaded up </summary>
        public Sprite Thumbnail {
            get {
                return _thumbnail;
            }
        }
        /// <summary> A brief description of the project </summary>
        public string BriefDescription {
            get {
                return _briefDescription;
            }
        }
        /// <summary> More specific details (text and images) of the project </summary>
        public PageContent[] Details {
            get {
                return _details;
            }
        }
        /// <summary> Design details (text and images) about the project </summary>
        public PageContent[] Design {
            get {
                return _design;
            }
        }
        /// <summary> The challenges faced during the project </summary>
        public PageContent[] Challenges {
            get {
                return _challenges;
            }
        }
        /// <summary> Credits to the inclusion of any other people's assets or help </summary>
        public string Credits {
            get {
                return _credits;
            }
        }
        /// <summary> The 3D trophy associated with the project </summary>
        public GameObject Trophy {
            get {
                return _trophy;
            }
        }
    }
}
