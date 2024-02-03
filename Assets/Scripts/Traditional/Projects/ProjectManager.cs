using System.Collections.Generic;
using UnityEngine;

namespace Website.Traditional.Projects
{
    /// <summary>
    ///    A singleton ProjectManager which stores all references to each of the Projects in the 
    ///    Resources folder.
    /// </summary>
    public class ProjectManager
    {
        /// <summary> The static instance of ProjectManager </summary>
        private static ProjectManager _instance;
        /// <summary> The list of all Projects loaded from the Resources folder </summary>
        private Project[] _projects;

        /// <summary>
        ///    A constructor for ProjectManager that also populates the Projects array
        /// </summary>
        private ProjectManager() {
            _projects = Resources.LoadAll<Project>("Projects") as Project[];
        }

        /// <summary>
        ///    Returns the current instance of the ProjectManager and instantiates an 
        ///    instance and Project array if it does not yet exist.
        /// </summary>
        ///    <returns> A singleton instance of ProjectManager </returns>
        public ProjectManager GetInstance() {
            // If instance is null instantiate and populate Project array
            if (_instance == null) {
                _instance = new ProjectManager();
            }
            // Return current instance
            return _instance;
        }

        /// <summary>
        ///    Returns two lists of sorted Projects based on the type of project
        /// </summary>
        ///    <returns> A Dictionary containing all projects sorted by ProjectType </returns>
        public Dictionary<ProjectType, List<Project>> GetProjects() {
            // Create Dictionary of Lists divided by ProjectType
            Dictionary<ProjectType, List<Project>> projectDict = new Dictionary<ProjectType, List<Project>>();
            // Sort each project into its respective List
            foreach (Project project in _projects) {
                projectDict[project.ProjectType].Add(project);
            }
            // Return populated Dictionary
            return projectDict;
        }
    }
}
