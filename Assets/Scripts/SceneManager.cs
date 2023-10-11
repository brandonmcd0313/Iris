using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [TooltipAttribute("Set to -1 to disable")]
    [SerializeField] int _nextSceneIndex;

    [TooltipAttribute("Set to -1 to disable")]
    [SerializeField] int _previousSceneIndex;

    [Space(5)]
    [SerializeField] GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        _player.GetComponent<PlayerController>().OnPlayerExitScreenSpaceRight += LoadNextScene;
        _player.GetComponent<PlayerController>().OnPlayerExitScreenSpaceLeft += LoadPreviousScene;

    }

    void LoadNextScene()
    {
        //load the next scene
        if (_nextSceneIndex != -1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(_nextSceneIndex);
        }
    }

    void LoadPreviousScene()
    {
        //load the previous scene
        if (_previousSceneIndex != -1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(_previousSceneIndex);
        }

    }
}
