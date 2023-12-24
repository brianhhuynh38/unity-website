using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

/// <summary>
///   Handles the timelines that are played and any necessary scene loading in the Homepage scene
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>The playable directors active in the scene</summary>
    public List<PlayableDirector> directors;
    /// <summary>Asynchronous operation to load the gallery scene</summary>
    AsyncOperation _galleryAsyncLoad;

    /// <summary>
    ///   Plays the transitional cutscene to the Gallery scene while asynchronously loading the scene
    /// </summary>
    public void PlayEnterTimeline() {
        // Start asynchronously loading the gallery scene while the cutscene plays
        StartCoroutine(LoadGallery());
        directors[1].Play();
    }

    /// <summary>
    ///   A couroutine to be used while the timeline is playing in order to speed up transitions
    ///   when changing scenes.
    /// </summary>
    IEnumerator LoadGallery() {
        // Try to load scene until the cutscene allows
        yield return null;
        _galleryAsyncLoad = SceneManager.LoadSceneAsync("Gallery");
        _galleryAsyncLoad.allowSceneActivation = false;
    }

    /// <summary>
    ///   Allows the scene activation to occur for the gallery scene
    /// </summary>
    public void AllowLoadGallery() {
        _galleryAsyncLoad.allowSceneActivation = true;
    }
}
