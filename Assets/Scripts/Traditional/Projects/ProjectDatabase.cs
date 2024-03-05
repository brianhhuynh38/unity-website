using System.Collections.Generic;
using UnityEngine;

namespace Website.Traditional.Projects
{
    /// <summary>
    ///    A singleton ProjectDatabase which stores all references to each of the Projects in the 
    ///    Resources folder.
    /// </summary>
    public class ProjectDatabase
    {
        /// <summary> The list of all Projects loaded from the Resources folder </summary>
        private Project[] _projects;

        /// <summary>
        ///    A constructor for ProjectManager that also populates the Projects array
        /// </summary>
        public ProjectDatabase() {
            _projects = Resources.LoadAll<Project>("Projects") as Project[];
        }

        /// <summary>
        ///    Returns two dictionaries of Projects based on the type of project
        /// </summary>
        ///    <returns> A tuple of Dictionaries containing all projects sorted by ProjectType </returns>
        public Dictionary<ProjectType, Dictionary<string, Project>> GetProjects() {
            // Create Dictionary of Dictionaries divided by ProjectType and project IDs
            Dictionary<ProjectType, Dictionary<string, Project>> projectDict = new Dictionary<ProjectType, Dictionary<string, Project>>();
            projectDict.Add(ProjectType.SCHOOL, new Dictionary<string, Project>());
            projectDict.Add(ProjectType.PERSONAL, new Dictionary<string, Project>());
            // Sort each project into its respective Dictionary
            foreach (Project project in _projects) {
                // Assign each project to its designated Dictionary
                try {
                    projectDict[project.ProjectType].TryAdd(project.UUID, project);
                } catch {
                    Debug.LogWarning($"Project \"{project.Title}\" of type {project.ProjectType} failed to add.");
                }
            }
            // Return populated Dictionary
            return projectDict;
        }
    }
}
