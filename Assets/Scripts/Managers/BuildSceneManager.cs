using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

//Inherits singleton persistent, spawns the object on don't destroy on load
public class BuildSceneManager : SingletonPersistent<BuildSceneManager>
{
    //Scriptable Object to contain data for transition
    [SerializeField] private TransitionScriptableObject _transitionScriptableObject;
    private GameObject _transitionPanel;

    //Called to set the transition panel to be played
    public void SetTransitionPanel(GameObject transitionObject)
    {
        _transitionPanel = transitionObject;
    }

    //Restarts transition and loads the scene async
    public void LoadSceneAsync(BuildScene buildScene)
    {
        _transitionPanel.SetActive(false);
        StartCoroutine(LoadAsync(buildScene));
    }

    //Wait for the half of the transition time before loading the scene
    private IEnumerator LoadAsync(BuildScene buildScene)
    {
        _transitionPanel.SetActive(true);
        float elapsedTime = 0f;
        while (elapsedTime < _transitionScriptableObject.TransitionTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadSceneAsync((int)buildScene, LoadSceneMode.Single);
    }
}

//Enums for the index of the scenes
public enum BuildScene
{
    MainMenuScene = 0,
    GameScene = 1,
    FTUEScene = 2,
}
