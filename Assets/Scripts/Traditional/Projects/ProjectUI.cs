using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Website.Traditional.Projects
{
    /// <summary>
    ///    ProjectUI controls all UI elements part of the Portfolio Page
    /// </summary>
    public class ProjectUI : MonoBehaviour
    {
        /// <summary> The database of information of all Projects </summary>
        private ProjectDatabase _database;
        /// <summary> Dictionary of all projects organized by type and unique ID </summary>
        private Dictionary<ProjectType, Dictionary<string, Project>> _projectDict;

        /////////////////
        // UI Elements //
        /////////////////

        /// <summary> The textbox containing the title </summary>
        [SerializeField] private TMP_Text _titleBox;
        /// <summary> The textbox containing the brief description </summary>
        [SerializeField] private TMP_Text _descriptionBox;
        /// <summary> The scrollbox content object that holds all individual PageContent objects </summary>
        [SerializeField] private GameObject _contentScroll;
        /// <summary> The thumbnail image displayed for each project on the display panel </summary>
        [SerializeField] private Image _thumbnail;

        private enum DisplayContentTabType { DETAILS, DESIGN, CHALLENGES, CREDITS }

        // Start is called before the first frame update
        void Start()
        {
            // Create a ProjectDatabase to get information
            _database = new ProjectDatabase();
            // Fill school and project ProjectDicts
            _projectDict = _database.GetProjects();
            // If any of the content-holders are not assigned, produce warning
            if (null == _titleBox | _descriptionBox | _contentScroll | _thumbnail) {
                Debug.LogWarning("WARNING: Not all project UI elements are assigned!");
            }
        }

        /// <summary>
        ///    Loads a given Project's content into the display panel once selected using the project's
        ///    <paramref name="type"></paramref> and unique <paramref name="projectUUID"></paramref>.
        /// </summary>
        ///    <param name="type"> The type of Project that is being loaded </param>
        ///    <param name="projectUUID"> The unique ID assigned to the project being loaded </param>
        public void LoadProjectContent(ProjectType type, string projectUUID)
        {
            // Get project from dictionary
            Project project = _projectDict[type][projectUUID];
            // Assign each textbox necessar details
            _titleBox.text = project.Title;
            _descriptionBox.text = project.BriefDescription;
            _thumbnail.sprite = project.Thumbnail;
            LoadContentTab(DisplayContentTabType.DETAILS, project);
        }

        /// <summary>
        ///    Loads the content for a given project for a certain tab
        /// </summary>
        private void LoadContentTab(DisplayContentTabType tabChange, Project project) {
            // TODO: Add all details to content scrollview
        }
    }
}
